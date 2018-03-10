
function UsersTable() {

    function createBtn(user, txt, callback) {
        var btn = document.createElement("button");
        var text = document.createTextNode(txt);
        btn.appendChild(text);
        btn.onclick = function (u) {
            return function () {
                callback(u)
            }
        }(user);

        return btn;
    }

    return {
        drawTable(users, onDelete, onEdit) {
            var table = document.getElementsByClassName("users-table")[0];
            var tbody = table.tBodies[0];

			while(tbody.hasChildNodes()) {
				tbody.removeChild(tbody.lastChild);
			}

            var len = users.length;
            for (var i = 0; i < len; i++) {
                var row = tbody.insertRow(i);

                var login = row.insertCell(0);
                var firstName = row.insertCell(1);
                var lastName = row.insertCell(2);
                var role = row.insertCell(3);
                var createdOn = row.insertCell(4);
                var updatedOn = row.insertCell(5);
                var isActive = row.insertCell(6);
                var actions = row.insertCell(7);

                login.innerHTML = users[i].login;
                firstName.innerHTML = users[i].firstName;
                lastName.innerHTML = users[i].lastName;
                role.innerHTML = users[i].role;
                createdOn.innerHTML = users[i].createdOn.toDate();
                updatedOn.innerHTML = users[i].updatedOn.toDate();
                isActive.innerHTML = users[i].isActive;


                actions.appendChild(createBtn(users[i], "Edit", onEdit));
                actions.appendChild(createBtn(users[i], "Delete", onDelete));
            }
        }
    }
}
