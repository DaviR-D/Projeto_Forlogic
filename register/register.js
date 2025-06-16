document.addEventListener("DOMContentLoaded", function () {
    html.register = {};
    getRegisterElements();
    insertRegisterData();
    html.addActions();
})

function getRegisterElements() {
    html.register.tableTop = document.getElementById("tableTop");

    html.register.registerModal = document.getElementById("registerModal")

    html.register.statusCheck = document.getElementById("status");
    html.register.nameInput = document.getElementById("name");
    html.register.ageInput = document.getElementById("age");
    html.register.emailInput = document.getElementById("email");
    html.register.adressInput = document.getElementById("adress");
    html.register.otherInput = document.getElementById("other");
    html.register.interestsInput = document.getElementById("interests");
    html.register.feelingsInput = document.getElementById("feelings");
    html.register.valuesInput = document.getElementById("values");

    html.register.registerModal.addEventListener("close", function () {
        clearFields();
    });

}

function insertRegisterData() {
    html.register.tableTop.insertAdjacentHTML('beforeend',
        `
            <h1><strong>Cadastros</strong></h1>
            <button onclick="showRegisterModal()"> + NOVO CADASTRO</button>
        `
    )

    navLink = document.getElementById("registerNav")
    navLink.style.borderBottomStyle = "solid";

    html.addActions = () => {
        tableHeader.insertAdjacentHTML("beforeend","<th>Ações</th>");
        document.querySelectorAll(".actions").forEach(row => {
            row.style.display = "table-cell";
        })
    }
}

function saveRegistration(event, key = crypto.randomUUID()) {
    event.preventDefault();

    resetFieldsColor();

    if (registerForm.checkValidity()) {
        let existingRegister = JSON.parse(localStorage.getItem(key));
        let newRegister = {
            name: html.register.nameInput.value,
            email: html.register.emailInput.value,
            status: html.register.statusCheck.checked ? "Ativo" : "Inativo",
            pending: true,
            date: existingRegister?.date ? existingRegister?.date : new Date(),
            age: html.register.ageInput.value,
            adress: html.register.adressInput.value,
            other: html.register.otherInput.value,
            interests: html.register.interestsInput.value,
            feelings: html.register.feelingsInput.value,
            values: html.register.valuesInput.value,
        };

        if (checkFieldsValidity(key, newRegister)) {
            localStorage.setItem(key, JSON.stringify(newRegister));
            registerForm.submit();
        }
    }
    else {
        alert("Preencha todos os campos!");
        highlightBlankFields();
    }
}

function deleteRegistration(key) {
    let deletedUser = JSON.parse(localStorage.getItem(key)).name
    let response = confirm(`Deseja mesmo deletar ${deletedUser}?`)
    if (response) {
        localStorage.removeItem(key);
        loadTableContent();
    }
}

function editRegistration(key) {
    registerModal.dataset.userKey = key;
    resetFieldsColor();
    registerModal.showModal();
    let editItem = JSON.parse(localStorage.getItem(key));

    html.register.nameInput.value = editItem.name;
    html.register.emailInput.value = editItem.email;
    html.register.ageInput.value = editItem.age;
    html.register.adressInput.value = editItem.adress;
    html.register.otherInput.value = editItem.other;
    html.register.interestsInput.value = editItem.interests;
    html.register.feelingsInput.value = editItem.feelings;
    html.register.valuesInput.value = editItem.values;
    html.register.statusCheck.checked = editItem.status == "Ativo" ? true : false;
}

function showRegisterModal() {
    registerModal.showModal();
}

function hideRegisterModal() {
    registerModal.close();
}

function clearFields() {
    registerModal.dataset.userKey = undefined;
    html.register.nameInput.value = "";
    html.register.emailInput.value = "";
    html.register.ageInput.value = "";
    html.register.adressInput.value = "";
    html.register.otherInput.value = "";
    html.register.interestsInput.value = "";
    html.register.feelingsInput.value = "";
    html.register.valuesInput.value = "";
}

function resetFieldsColor() {
    [...registerForm.elements].forEach(field => {
        field.style.borderColor = "black";
    });
}

function highlightBlankFields() {
    let emptyFields = [...registerForm.elements].filter(field => !field.checkValidity());
    emptyFields.forEach(field => {
        field.style.borderColor = "red";
    });
}

function checkFieldsValidity(key, newRegister) {
    if (checkExistingEmail(key, newRegister.email)) {
        alert("Email já cadastrado!");
        return false;
    } else if (!checkValidEmail(newRegister)) {
        alert("Insira um email válido!");
        return false;
    } else if (!checkValidName(newRegister)) {
        alert("Insira um nome válido!");
        return false;
    }
    return true;
}

function checkExistingEmail(key, email) {
    let emailExists = registrations.map(registration => registration.email).includes(email);
    let sameKey = JSON.parse(localStorage.getItem(key))?.email == email;

    return (emailExists && !sameKey);
}

function checkValidEmail(newRegister) {
    regex = /^[\w-\.]+@([\w-]+\.)+[\w-]{2,4}$/;
    return regex.test(newRegister.email);
}

function checkValidName(newRegister) {
    regex = /^[^0-9!@#$%*+={}?<>()]*$/
    return regex.test(newRegister.name);
}