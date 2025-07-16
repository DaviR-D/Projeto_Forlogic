let apiUrl = "http://localhost:5204";

let pageTheme = localStorage.getItem("theme");

let html = {};

document.addEventListener("DOMContentLoaded", function () {
    getLoginElements();
    applyTheme(pageTheme ?? "default");
})

function getLoginElements() {
    html.loginButton = document.getElementById("login");
    html.nameInput = document.getElementById("name");
    html.emailInput = document.getElementById("email");
    html.passwordInput = document.getElementById("password");
    html.errorMessage = document.getElementById("errorMessage");

    html.nameInput.addEventListener("keydown", e => {
        if (e.key === "Enter") html.emailInput.focus();
    })
    html.emailInput.addEventListener("keydown", e => {
        if (e.key === "Enter") html.passwordInput.focus();
    })
    html.passwordInput.addEventListener("keydown", e => {
        if (e.key === "Enter") tryRegister();
    })
}

async function tryRegister() {
    let newUser = { name: html.nameInput.value, email: html.emailInput.value, password: html.passwordInput.value }
    await fetch(`${apiUrl}/api/authentication/signup`, {
        method: "POST",
        headers: {
            "Content-Type": "application/json"
        },
        body: JSON.stringify(newUser)
    })
    window.location = "../login/login.html";
}

function checkValidEmail(email) {
    let regex = /^[\w-\.]+@([\w-]+\.)+[\w-]{2,4}$/;

    let validEmail = regex.test(email);

    if (!validEmail) html.errorMessage.innerText = "Insira um email válido!";;

    return validEmail;
}