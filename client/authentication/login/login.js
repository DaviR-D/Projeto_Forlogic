let apiUrl = "http://localhost:5204";

let pageTheme = localStorage.getItem("theme");

let html = {}

document.addEventListener("DOMContentLoaded", function () {
  getLoginElements();
  applyTheme(pageTheme ?? "default");

})


function getLoginElements() {
  html.loginButton = document.getElementById("login");
  html.emailInput = document.getElementById("email");
  html.passwordInput = document.getElementById("password");
  html.errorMessage = document.getElementById("errorMessage");

  html.emailInput.addEventListener("keydown", e => {
    if (e.key === "Enter") html.passwordInput.focus();
  })
  html.passwordInput.addEventListener("keydown", e => {
    if (e.key === "Enter") tryLogin();
  })
}

async function tryLogin() {
  let login = { email: html.emailInput.value, password: html.passwordInput.value }


  if (checkValidEmail(login.email)) {
    let token;
    await fetch(`${apiUrl}/api/authentication/`, {
      method: "POST",
      headers: {
        "Content-Type": "application/json"
      },
      body: JSON.stringify(login)
    }).then(response => { return response.json() })
      .then(data => {
        token = data.token;
      });

    if (token) {
      tokenData = parseToken(token);
      localStorage.setItem("login", JSON.stringify({ "name": tokenData.unique_name, "token": token }))
      window.location = "../../dashboard/dashboard.html";
    }
    else {
      html.errorMessage.innerText = "Login incorreto!";
    }
  }
}

function checkValidEmail(email) {
  let regex = /^[\w-\.]+@([\w-]+\.)+[\w-]{2,4}$/;

  let validEmail = regex.test(email);

  if (!validEmail) html.errorMessage.innerText = "Insira um email válido!";

  return validEmail;
}

function parseToken(token){
  const payload = token.split('.')[1];
  const decodedPayload = atob(payload.replace(/-/g, '+').replace(/_/g, ';'));
  return JSON.parse(decodedPayload);
}

