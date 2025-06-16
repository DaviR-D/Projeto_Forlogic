let pageTheme = localStorage.getItem("theme");

let html = {}

document.addEventListener("DOMContentLoaded", function () {
  getLoginElements();
  applyTheme(pageTheme ?? "default");

})


function getLoginElements() {
  html.users = { name: "Davi Rodrigues", email: "davi@gmail.com", password: "senha123" };
  html.loginButton = document.getElementById("login");
  html.emailInput = document.getElementById("email");
  html.passwordInput = document.getElementById("password");

  html.emailInput.addEventListener("keydown", e => {
    if (e.key === "Enter") html.passwordInput.focus();
  })
  html.passwordInput.addEventListener("keydown", e => {
    if (e.key === "Enter") tryLogin();
  })
}

function tryLogin() {
  let login = { email: html.emailInput.value, password: html.passwordInput.value }

  let inputLogin = JSON.stringify([login.email, login.password]);
  let storedLogin = JSON.stringify([html.users.email, html.users.password]);

  if (checkValidEmail(login.email)) {
    if (inputLogin == storedLogin) {
      localStorage.setItem("login", JSON.stringify(html.users))
      window.location = "../dashboard/dashboard.html";
    }
    else {
      alert("Login incorreto!");
    }
  }
}

function checkValidEmail(email) {
  let regex = /^[\w-\.]+@([\w-]+\.)+[\w-]{2,4}$/;

  let validEmail = regex.test(email);

  if (!validEmail) alert("Insira um email válido!");

  return validEmail;
}

function applyTheme(theme = "default") {
  let themeColors = {
    default: {
      '--main-color': "white",
      '--second-color': "rgb(225, 225, 225)",
      '--font-color': "rgb(74, 74, 74)",
      '--hover-color': "rgb(195, 195, 195)",
      '--border-color': "rgb(203, 203, 203)"
    },
    dark: {
      '--main-color': "rgb(24, 26, 27)",
      '--second-color': "rgb(44, 47, 49)",
      '--font-color': "rgb(210, 210, 210)",
      '--hover-color': "rgb(70, 75, 78)",
      '--border-color': "rgb(60, 64, 66)"
    }
  };

  let themeVariables = [
    "--main-color",
    "--second-color",
    "--font-color",
    "--hover-color",
    "--border-color"
  ]

  themeVariables.forEach(variable => {
    document.documentElement.style.setProperty(variable, themeColors[theme][variable]);
  });

  localStorage.setItem("theme", theme);
}

