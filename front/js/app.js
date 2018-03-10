
function ge(el) {
    return document.getElementById(el);
}

function setModalDisplay(value) {
    ge("modal-container").style.display = value;
    ge("modal-form-container").style.display = value;
}

function onAddUserClick() {
    setModalDisplay("block");
}

function closeModal() {
    setModalDisplay("none");
}

function formSave(isEdit) {
    var login = ge("login").value;
    if (!login) {
        alert("Login field required");
        return;
    }

    if (service.getUserByLogin(login)) {
        alert("Login already exists");
        return;
    }

    var firstName = ge("firstName").value;
    var lastName = ge("lastName").value;
    var role = ge("role").value;
    var isActive = ge("isActive").checked;
    var createdOn = new Date();
    var updatedOn = new Date();

    var user = {
        login,
        firstName,
        lastName,
        createdOn,
        updatedOn,
        role,
        isActive
    }

    service.addUser(user);
    drawUsers();
    closeModal();
}

var service = new UsersService();
var usersTable = new UsersTable();

function drawUsers() {
    service.getUsers()
        .then(function (response) {
            usersTable
                .drawTable(response.users, (l) => alert(l), (l) => alert(l));
        }).catch(function () {
            alert("Can't get users")
        });
}

window.onload = function () {
    drawUsers();
}
