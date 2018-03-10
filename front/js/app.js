
function ge(el) {
    return document.getElementById(el);
}

function setModalDisplay(value) {
    ge("modal-container").style.display = value;
    ge("modal-form-container").style.display = value;
}

function onAddUserClick() {
	ge("login").value = "";
	ge("firstName").value = "";
    ge("lastName").value = "";
    ge("role").value = "";
	ge("isActive").checked = false;
	
    setModalDisplay("block");
}

function onDelete(user) {
	if (confirm("Are you sure ?")) {
		service.removeUserById(user.id);
		drawUsers();
	}		
}

function onEditClick(user) {
	ge("login").value = user.login;
	ge("firstName").value = user.firstName;
    ge("lastName").value = user.lastName;
    ge("role").value = user.role;
	ge("isActive").checked = user.isActive;

	ge("userId").value = user.id;
	setModalDisplay("block");
}

function closeModal() {
    setModalDisplay("none");
}

function formSave() {
	var userId = ge("userId").value;
	var isEdit = !!userId;

    var login = ge("login").value;
    if (!login) {
        alert("Login field required");
        return;
    }

	var existing = service.getUserByLogin(login);
    if ((!isEdit && existing) || (isEdit && existing && existing.id && existing.id != userId)) {
        alert("Login already exists");
        return;
    }	

    var firstName = ge("firstName").value;
    var lastName = ge("lastName").value;
    var role = ge("role").value;
	var isActive = ge("isActive").checked;
	    
    var user = {
		id: userId,
        login,
        firstName,
        lastName,        
        role,
        isActive
	};
	
	if (isEdit)
		service.updateUser(user)
	else
		service.addUser(user);
	
	ge("userId").value = "";
	drawUsers();	
    closeModal();
}

var service = new UsersService();
var usersTable = new UsersTable();

function drawUsers() {



    service.getUsers()
        .then(function (response) {
            usersTable
                .drawTable(response.users, function(user) {
					onDelete(user);
				}, function(user) {
					onEditClick(user);
				});
        }.bind(this)).catch(function () {
            alert("Can't get users")
        });
}

window.onload = function () {
    drawUsers();
}
