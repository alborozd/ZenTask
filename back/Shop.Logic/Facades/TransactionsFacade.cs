using Shop.Common;
using Shop.Common.Contracts;
using Shop.Common.Services.Contracts;
using Shop.Contracts.Dal;
using Shop.Domain;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using Shop.Logic.Exceptions;

namespace Shop.Logic.Facades
{
    public class TransactionsFacade
    {
        private readonly IUsersRepository usersRepository;
        private readonly IProductsRepository productsRepository;
        private readonly IDiscountsRepository discountRepository;
        private readonly IEnvironment environment;
        private readonly ILockService lockService;
        private readonly ITransactionErrorsRepository errorsRepository;
        private readonly ITransactionsRepository transactionsRepository;

        public TransactionsFacade(
            IUsersRepository usersRepository,
            IProductsRepository productsRepository,
            IDiscountsRepository discountRepository,
            IEnvironment environment,
            ILockService lockService,
            ITransactionErrorsRepository errorsRepository,
            ITransactionsRepository transactionsRepository
        )
        {
            this.usersRepository = usersRepository;
            this.productsRepository = productsRepository;
            this.discountRepository = discountRepository;
            this.environment = environment;
            this.lockService = lockService;
            this.errorsRepository = errorsRepository;
            this.transactionsRepository = transactionsRepository;
        }

        private float GetDiscount(int productId)
        {
            return discountRepository
                .GetDiscountsByProduct(productId)
                .OrderByDescending(t => t.StartDate)
                .FirstOrDefault()?.Percents ?? 0f;
        }

        private Tuple<User, Product> GetAndCheckEntities(string userName, int productId)
        {
            User user = usersRepository
                .GetByName(userName)
                .Assert(() => new InvalidOperationException("User not found"));

            Product product = productsRepository
                .GetById(productId)
                .Assert(() => new InvalidOperationException("Product not found"));

            return new Tuple<User, Product>(user, product);
        }

        public void Buy(string userName, int productId, int quantity)
        {
            if (quantity <= 0)
                throw new InvalidOperationException("Quantity can't be less or equal 0");

            lockService.Execute(() =>
            {
                try
                {
                    var entities = GetAndCheckEntities(userName, productId);
                    User user = entities.Item1;
                    Product product = entities.Item2;

                    if (product.Quantity < quantity)
                        throw new ValidationException("Not enough product to buy");

                    float userDiscount = GetDiscount(productId);
                    decimal discountPrice = userDiscount != 0
                        ? product.Cost - ((product.Cost / 100) * (decimal)userDiscount)
                        : product.Cost;

                    decimal amount = discountPrice * quantity;

                    if (user.Amount < amount)
                        throw new ValidationException("Not enough money to buy the product");

                    transactionsRepository.Add(new Transaction()
                    {
                        Username = user.Name,
                        ProductId = product.Id,
                        ProductName = product.Name,
                        Quantity = quantity,
                        Cost = product.Cost,
                        Discount = userDiscount,
                        SellCost = discountPrice,
                        Amount = amount
                    });

                    productsRepository.ChangeQuantity(productId, product.Quantity - quantity);
                    usersRepository.ChangeBalance(user.Name, user.Amount - amount);
                }
                catch (Exception ex) when (!(ex is ValidationException))
                {
                    errorsRepository.Add(new TransactionError()
                    {
                        Date = environment.GetUtcNow(),
                        Description = ex.Message
                    });

                    throw;   
                }
            });
        }
    }
}
