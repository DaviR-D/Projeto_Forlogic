let loggedUser = JSON.parse(localStorage.getItem("login"));

let html = {};

let registrations = [];

document.addEventListener("DOMContentLoaded", function () {
    if (!loggedUser) {
        alert("Faça login para continuar!")
        window.location = "../login/login.html"
    }
    loadLayout();
})

function loadLayout() {
    loadHeader();
    loadNav();
    loadTable();
    loadTableContent();
}

function loadHeader() {
    document.body.insertAdjacentHTML('beforeend',
        `
    <header>
        <input type="text" placeholder="Pesquisar..." id="search">
        <div class="login">
            <span id="userDisplay"></span>
            <a href="../login/login.html" id="exit">SAIR</a>
        </div>
    </header>
    `);

    html.exit = document.getElementById("exit");

    html.search = document.getElementById("search");

    html.userDisplay = document.getElementById("userDisplay");
    html.userDisplay.innerText = loggedUser.name;

    html.search.addEventListener("input", function () {
        loadTableContent();
        addActions?.();
    });

    html.exit.addEventListener("click", function () {
        localStorage.removeItem("login")
    })

}

function loadNav() {
    document.body.insertAdjacentHTML('beforeend',
        `
    <nav>
        <p style="text-align: center;">Operação Curiosidade</p>
        <div class="navLinks">
            <p><a href="../dashboard/dashboard.html">Home</a></p>
            <p><a href="../register/register.html">Cadastro</a></p>
            <p><a href="../report/report.html">Relatórios</a></p>
        </div>
    </nav>
    `);
}

function loadTable() {
    let content = document.getElementById("mainContent")
    content.insertAdjacentHTML('beforeend',
        `
        <article id="tableContainer">
            <div id="tableTop"></div>
            <div id="tableWraper">
                <table id="registrations"></table>
            </div>
        </article>
        `
    )
}

function loadTableContent() {
    registrations = [];
    getStorageRegistrations();

    html.registrations = document.getElementById("registrations");

    registrationList = ['<tr id="tableHeader"><th>Nome</th><th>Email</th><th>Status</th></tr>'];
    registrations.forEach(register => {
        let rowContent = `${register.name} ${register.email.split("@")[0]}`;

        if (rowContent.includes(html.search.value)) {
            registrationList.push(
                `<tr>
                    <td>${register.name}</td>
                    <td>${register.email}</td>
                    <td>${register.status}</td>
                    <td class="actions" style="display: none;">
                        <button class="editButton" onclick="editRegistration('${register.key}')">&#9998</button>
                        <button class="deleteButton" onclick="deleteRegistration('${register.key}')">X</button>
                    </td>
                </tr>`
            );
        }

    });

    html.registrations.innerHTML = registrationList.join('');
}


function getStorageRegistrations() {
    for (let index = 0; index < localStorage.length; index++) {
        let key = localStorage.key(index);
        if(key=="login") continue;
        let item = JSON.parse(localStorage.getItem(key));
        item.key = key;
        registrations.push(item);
    }
}




