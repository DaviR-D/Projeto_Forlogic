document.addEventListener("DOMContentLoaded", function () {
    getSignupElements();
})

function getSignupElements() {
    html.nameInput = document.getElementById("name");

    html.nameInput.addEventListener("keydown", e => {
        if (e.key === "Enter") html.emailInput.focus();
    })
    html.passwordInput.addEventListener("keydown", e => {
        if (e.key === "Enter") trySignup();
    })
}

async function trySignup() {
    if (checkValidEmail(html.emailInput.value)) {
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
}