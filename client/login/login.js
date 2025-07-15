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
      localStorage.setItem("login", JSON.stringify({ "name": "Davi", "token": token }))
      window.location = "../dashboard/dashboard.html";
    }
    else {
      html.errorMessage.innerText = "Login incorreto!";
    }
  }
}

function checkValidEmail(email) {
  let regex = /^[\w-\.]+@([\w-]+\.)+[\w-]{2,4}$/;

  let validEmail = regex.test(email);

  if (!validEmail) html.errorMessage.innerText = "Insira um email válido!";;

  return validEmail;
}

function applyTheme(theme = "default") {
  let themeColors = {
    default: {
      '--main-color': "white",
      '--second-color': "rgb(225, 225, 225)",
      '--font-color': "rgb(74, 74, 74)",
      '--highlight-color': "rgb(195, 195, 195)",
      '--border-color': "rgb(203, 203, 203)"
    },
    dark: {
      '--main-color': "rgb(24, 26, 27)",
      '--second-color': "rgb(44, 47, 49)",
      '--font-color': "rgb(210, 210, 210)",
      '--highlight-color': "rgb(70, 75, 78)",
      '--border-color': "rgb(60, 64, 66)"
    }
  };

  let themeVariables = [
    "--main-color",
    "--second-color",
    "--font-color",
    "--highlight-color",
    "--border-color"
  ]

  themeVariables.forEach(variable => {
    document.documentElement.style.setProperty(variable, themeColors[theme][variable]);
  });

  localStorage.setItem("theme", theme);
}

