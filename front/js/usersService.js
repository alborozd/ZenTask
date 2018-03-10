

function UsersService() {
	var users = [
		{ id: 1, login: "billgates", firstName: "Bill", lastName: "Gates", role: "CEO", createdOn: new Date(), updatedOn: new Date(), isActive: true },
		{ id: 2, login: "user", firstName: "User", lastName: "User", role: "user", createdOn: new Date(), updatedOn: new Date(), isActive: true },
		{ id: 3, login: "admin", firstName: "Admin", lastName: "Admin", role: "admin", createdOn: new Date(), updatedOn: new Date(), isActive: true },
		{ id: 4, login: "jlo", firstName: "Jennifer", lastName: "Lopez", role: "actress", createdOn: new Date(), updatedOn: new Date(), isActive: true },
		{ id: 5, login: "jchan", firstName: "Jackie", lastName: "Chan", role: "fighter", createdOn: new Date(), updatedOn: new Date(), isActive: true },
	];

	return {
		getUsers() {
			return Promise.resolve({ users })
		},

		getUserBy(prop, value) {
			var items = users.filter(function (user) {
				return user[prop] == value;
			});

			return items.length && items.length > 0
				? items[0]
				: null;
		},

        getUserByLogin(login) {
			return this.getUserBy("login", login);
		},

		getUserById(id) {
			return this.getUserBy("id", id);
		},

		removeUserById(userId) {
			var len = users.length;
			for(var i = 0; i < len; i++) {
				if (users[i].id == userId) {
					users.splice(i, 1);
					break;
				}
			}
		},

		updateUser(user) {
			var len = users.length;
			for(var i = 0; i < len; i++) {
				if (users[i].id == user.id) {
					users[i].login = user.login;
					users[i].firstName = user.firstName;
					users[i].lastName = user.lastName;
					users[i].role = user.role;
					users[i].updatedOn = new Date();
					users[i].isActive = user.isActive;	
					
					break;
				}
			}
		},

        addUser(user) {			
			user.createdOn = new Date();
			user.updatedOn = new Date();
			user.id = Date.now();
			users.push(user);
		}
	}
}


