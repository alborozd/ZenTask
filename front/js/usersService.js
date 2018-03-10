

function UsersService() {
    var users = [
        { login: "billgates", firstName: "Bill", lastName: "Gates", role: "CEO", createdOn: new Date(), updatedOn: new Date(), isActive: true },
    ];

    return {
        getUsers() {
            return Promise.resolve({ users })
        },

        getUserByLogin(login) {
            var items = users.filter(function(user) {
                return user.login === login;
            });

            return items.length && items.length > 0 
                ? items[0]
                : null;
        },

        addUser(user) {
            users.push(user);
        }
    }
}


