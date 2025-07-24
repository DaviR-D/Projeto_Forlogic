document.addEventListener("DOMContentLoaded", async function () {
    await updateTable();
    html.register = {};
    getRegisterElements();
    insertRegisterData();
    try { html.addActions(); }
    catch (error) { if (!(error instanceof ReferenceError)) throw error; }
})

function getRegisterElements() {
    html.register.tableTop = document.getElementById("tableTop");

    html.register.registerModal = document.getElementById("registerModal");
    html.register.alertModal = document.getElementById("alertModal");

    html.register.statusCheck = document.getElementById("status");
    html.register.nameInput = document.getElementById("name");
    html.register.ageInput = document.getElementById("age");
    html.register.emailInput = document.getElementById("email");
    html.register.addressInput = document.getElementById("address");
    html.register.otherInput = document.getElementById("other");
    html.register.interestsInput = document.getElementById("interests");
    html.register.feelingsInput = document.getElementById("feelings");
    html.register.valuesInput = document.getElementById("values");

    html.register.alertTitle = document.getElementById("alertTitle");
    html.register.alertDeleteButton = document.getElementById("alertDeleteButton");
    html.register.cancelDeleteButton = document.getElementById("cancelDeleteButton");

    html.register.alertModal.addEventListener("close", function () {
        document.body.classList.remove("blur");
    });

    html.register.registerModal.addEventListener("close", function () {
        document.body.classList.remove("blur");
        clearFields();
        resetFieldsStyle();
    });

    addInputEvents();
}

function insertRegisterData() {
    html.register.tableTop.insertAdjacentHTML('beforeend',
        `
            <h1><strong>Cadastros</strong></h1>
            <button onclick="showRegisterModal()"> + NOVO CADASTRO</button>
        `
    )

    navLink = document.getElementById("registerNav")
    navLink.style.backgroundColor = "var(--highlight-color)";

    html.addActions = () => {
        tableHeader.insertAdjacentHTML("beforeend", "<th style='cursor: default;'>Ações</th>");
        document.querySelectorAll(".actions").forEach(row => {
            row.style.display = "table-cell";
        })
    }
}

async function saveClient(event, id = null) {
    event.preventDefault();
    resetFieldsStyle();

    if (registerForm.checkValidity()) {
        let newRegister = {
            id: id,
            name: html.register.nameInput.value,
            email: html.register.emailInput.value,
            status: html.register.statusCheck.checked ? "Ativo" : "Inativo",
            pending: true,
            date: id ? clients.filter((register) => register.id == id)[0].date : new Date(),
            age: html.register.ageInput.value,
            address: html.register.addressInput.value,
            other: html.register.otherInput.value,
            interests: html.register.interestsInput.value,
            feelings: html.register.feelingsInput.value,
            values: html.register.valuesInput.value,
        };

        if (await checkFieldsValidity(id, newRegister)) {
            httpMethod = id == null ? "POST" : "PUT";

            fetch(`${apiUrl}/api/client/`, {
                method: httpMethod,
                headers: {
                    "Authorization": `Bearer ${loggedUser.token}`,
                    "Content-Type": "application/json"
                },
                body: JSON.stringify(newRegister)
            })
            registerForm.submit();
            if(httpMethod == "PUT")
                registerLog("Edit", id);
        }
    }
    else {
        highlightBlankFields();
    }
}

async function deleteClient(id) {
    await fetch(`${apiUrl}/api/client/${id}`, {
        method: 'DELETE',
        headers: {
            "Authorization": `Bearer ${loggedUser.token}`
        }
    })
    clientsCache = {};
    updateTable();
    hideDeleteConfirmation();
    registerLog("Delete", id);
}

async function editClient(id) {
    let editItem;

    await fetch(`${apiUrl}/api/client/${id}`, {
        headers: {
            "Authorization": `Bearer ${loggedUser.token}`,
        }
    })
        .then(response => { return response.json() })
        .then(data => {
            editItem = data.client;
        });

    registerModal.dataset.userId = id;
    showRegisterModal();

    html.register.nameInput.value = editItem.name;
    html.register.emailInput.value = editItem.email;
    html.register.ageInput.value = editItem.age;
    html.register.addressInput.value = editItem.address;
    html.register.otherInput.value = editItem.other;
    html.register.interestsInput.value = editItem.interests;
    html.register.feelingsInput.value = editItem.feelings;
    html.register.valuesInput.value = editItem.values;
    html.register.statusCheck.checked = editItem.status == "Ativo" ? true : false;
    registerLog("Read", id)
}

function showRegisterModal() {
    document.body.classList.add("blur");
    registerModal.showModal();
}

function hideRegisterModal() {
    registerModal.close();
}

function showDeleteConfirmation(id) {
    let deletedUser = clients.filter((register) => register.id == id)[0].name
    html.register.alertTitle.innerText = `Você tem certeza que deseja deletar ${deletedUser}?`;
    document.body.classList.add("blur");
    html.register.alertModal.showModal();
    html.register.alertDeleteButton.onclick = () => deleteClient(id);
}

function hideDeleteConfirmation() {
    html.register.alertModal.close();
}

function clearFields() {
    registerModal.dataset.userId = null;
    html.register.nameInput.value = "";
    html.register.emailInput.value = "";
    html.register.ageInput.value = "";
    html.register.addressInput.value = "";
    html.register.otherInput.value = "";
    html.register.interestsInput.value = "";
    html.register.feelingsInput.value = "";
    html.register.valuesInput.value = "";
    html.register.statusCheck.checked = false;
}

function resetFieldStyle(field) {
    let errorMessage;
    field.style.borderColor = "black";
    errorMessage = document.getElementById(`${field.id}Error`) ?? {};
    errorMessage.innerText = "";
}

function resetFieldsStyle() {
    let errorMessage;
    [...registerForm.elements].forEach(field => {
        resetFieldStyle(field);
    });
}

function highlightInvalidField(field, message) {
    let errorMessage;
    field.style.borderColor = "red";
    errorMessage = document.getElementById(`${field.id}Error`) ?? {};
    errorMessage.innerText = message;
}

function highlightBlankFields() {
    let emptyFields = [...registerForm.elements].filter(field => !field.checkValidity());
    emptyFields.forEach(field => {
        highlightInvalidField(field, "Campo obrigatório")
    });

    emptyFields[0].scrollIntoView({ behavior: "smooth", block: "center" });
}

async function checkFieldsValidity(id, newRegister) {
    html.invalidFields = [];
    checkValidName(newRegister.name);
    checkValidEmail(newRegister.email);
    await checkExistingEmail(id, newRegister.email);

    if (html.invalidFields.length) {
        html.invalidFields[0].scrollIntoView({ behavior: "smooth", block: "center" });
        return false;
    }
    return true;
}

async function checkExistingEmail(id = null, email) {
    let availableEmail;
    let idParam = String(id) == "null" ? "" : `id=${id}&`;

    await fetch(`${apiUrl}/api/client/checkEmail?${idParam}email=${email}`, {
        headers: {
            "Authorization": `Bearer ${loggedUser.token}`,
        }
    })
        .then(response => { return response.json() })
        .then(data => { availableEmail = data.isAvailable; });

    if (!availableEmail) {
        highlightInvalidField(html.register.emailInput, "Email já cadastrado");
        html.invalidFields.push(html.register.emailInput);
    };
    return availableEmail;
}

function checkValidEmail(email) {
    regex = /^[\w-\.]+@([\w-]+\.)+[\w-]{2,4}$/;
    let valid = regex.test(email)
    if (!valid) {
        highlightInvalidField(html.register.emailInput, "Insira um email válido");
        html.invalidFields.push(html.register.emailInput);
    };
    return valid;
}

function checkValidName(name) {
    regex = /^[^0-9!@#$%*+={}?<>()]*$/
    let valid = regex.test(name)
    if (!valid) {
        highlightInvalidField(html.register.nameInput, "Insira um nome válido")
        html.invalidFields.push(html.register.nameInput);
    };
    return valid;
}

async function registerLog(userAction, id) {
    fetch(`${apiUrl}/api/log/`, {
        method: "POST",
        headers: {
            "Authorization": `Bearer ${loggedUser.token}`,
            "Content-Type": "application/json"
        },
        body: JSON.stringify({ clientId: id, action: userAction })
    })
}

function addInputEvents() {
    html.invalidFields = [];
    [...registerForm.elements].forEach(field => {
        let errorMessage;
        field.addEventListener("input", function () {
            errorMessage = document.getElementById(`${field.id}Error`) ?? {};
            if (errorMessage.innerText == "Campo obrigatório") resetFieldStyle(field);
        })
    });

    html.register.nameInput.addEventListener("input", function () {
        resetFieldStyle(html.register.nameInput)
        checkValidName(html.register.nameInput.value);
    })
    html.register.emailInput.addEventListener("input", function () {
        resetFieldStyle(html.register.emailInput);
        checkValidEmail(html.register.emailInput.value);
    })
    html.register.emailInput.addEventListener("blur", function () {
        if (html.register.emailInput.value.length > 0)
            checkExistingEmail(registerModal.dataset.userId, html.register.emailInput.value);
    })
}