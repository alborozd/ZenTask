using System;

namespace Shop.Common
{
    public static class Maybe
    {
        public static TO Assert<TO>(this TO item, Func<Exception> exception)
        {
            if (!ReferenceEquals(item, null))
                return item;

            throw exception();
        }

        public static TO AssertIf<TO>(this TO item, Func<TO, bool> condition, Func<Exception> exception)
        {
            if (ReferenceEquals(item, null) || condition(item))
                throw exception();

            return item;
        }

        public static TI Do<TI>(this TI item, Action<TI> evaluator, Action failure = null)
        {
            if (ReferenceEquals(item, null))
            {
                if (failure != null)
                    failure();

                return default(TI);
            }

            evaluator(item);
            return item;
        }

        public static TO Return<TI, TO>(this TI item, Func<TI, TO> evaluator, TO failureValue)
        {
            return ReferenceEquals(item, null) ? failureValue : evaluator(item);
        }

        public static TO Return<TI, TO>(this TI item, Func<TI, TO> evaluator)
        {
            return item.Return(evaluator, default(TO));
        }
    }
}
