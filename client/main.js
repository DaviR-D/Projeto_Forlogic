let apiUrl = "http://localhost:5204";

let loggedUser = JSON.parse(localStorage.getItem("login"));

let html = {};

html.endpoint = "page"
html.params = ""

let clients = [];
let clientsCache = {};

let pageTheme = localStorage.getItem("theme");

document.addEventListener("DOMContentLoaded", function () {
    loadLayout();
    applyTheme(pageTheme ?? "default");
})

function loadLayout() {
    loadHeader();
    loadNav();
    loadTable();
    updateTable();
}

function loadHeader() {
    document.body.insertAdjacentHTML('beforeend',
        `
    <header>
        <input type="text" placeholder="Pesquisar..." id="search">
        <span id="searchResults"></span>
        <div class="login">
            <span id="themeIcon" class="material-symbols-outlined"></span>
            <span id="userDisplay"></span>
            <a href="../authentication/login/login.html" id="exit">SAIR</a>
        </div>
    </header>
    `);

    html.exit = document.getElementById("exit");

    html.search = document.getElementById("search");

    html.searchResults = document.getElementById("searchResults");

    html.userDisplay = document.getElementById("userDisplay");
    html.userDisplay.innerText = loggedUser.name;

    html.search.addEventListener("input", function () {
        clientsCache = {}
        html.arrow = {};
        html.tableOrder = "default"
        html.endpoint = html.search.value.length > 0 ? `page/search` : "page";
        html.params = html.search.value.length > 0 ? `query=${html.search.value.toLowerCase()}` : "";
        updateTable();
    });

    html.exit.addEventListener("click", function () {
        localStorage.removeItem("login");
    })

}

function loadNav() {
    document.body.insertAdjacentHTML('beforeend',
        `
    <nav>
        <p id="navIcon">OC</p>
        <p style="text-align: center;">Operação<br>Curiosidade</p>
        <div class="navLinks">
            <a id="dashboardNav" href="../dashboard/dashboard.html"><span class="material-symbols-outlined">home</span>Home</a>
            <a id="registerNav" href="../register/register.html"><span class="material-symbols-outlined">group_add</span>Cadastro</a>
            <a id="reportNav" href="../report/report.html"><span class="material-symbols-outlined">analytics</span>Relatórios</a>
        </div>
    </nav>
    `);

    html.themeIcon = document.getElementById("themeIcon");
    html.themeIcon.addEventListener("click", function () {
        if (pageTheme == "default") {
            applyTheme("dark")
        }
        else {
            applyTheme("default")
        }
    })
}

function loadTable() {
    let content = document.getElementById("mainContent")
    content.insertAdjacentHTML('beforeend',
        `
        <article id="tableContainer">
            <div id="tableTop"></div>
            <div id="tableWrapper">
                <table id="clients"></table>
            </div>
            <div class="tablePaging">
                <button id="previousButton" class="material-symbols-outlined">arrow_back</button>
                <span id="pageNumber"></span>
                <button id="nextButton" class="material-symbols-outlined">arrow_forward</button>
            </div>
        </article>
        `
    )
    html.clients = document.getElementById("clients");
    html.pageNumber = document.getElementById("pageNumber");
    html.nextButton = document.getElementById("nextButton");
    html.previousButton = document.getElementById("previousButton");
    html.tableOrder = "default";
    html.arrow = {};
    html.orderReverse = false;
}

async function loadTableContent() {
    renderedClients =
        [`
        <tr id="tableHeader">
            <th class="column" onclick="sortTable('name')">Nome ${html.arrow.name ? html.arrow.name : ""}</th>
            <th class="column" onclick="sortTable('email')">Email ${html.arrow.email ? html.arrow.email : ""}</th>
            <th class="column" onclick="sortTable('status')">Status ${html.arrow.status ? html.arrow.status : ""}</th>
            <th class="column" onclick="sortTable('date')">Data ${html.arrow.date ? html.arrow.date : ""}</th>
        </tr>`
        ];
    clients.forEach(register => {
        renderedClients.push(
            `
            <tr>
                <td>${register.name}</td>
                <td>${register.email}</td>
                <td><span style="border-radius:5px; padding:5px;" class=${register.status == "Ativo" ? "active" : "inactive"}>${register.status}</span></td>
                <td>${new Date(register.date).toLocaleDateString('pt-BR')}</td>
                <td class="actions" style="display: none;">
                    <button class="editButton material-symbols-outlined" onclick="editClient('${register.id}')">edit</button>
                    <button class="deleteButton material-symbols-outlined" onclick="showDeleteConfirmation('${register.id}')">delete</button>
                </td>
            </tr>
            `
        );
    });

    if (search.value.length > 0) {
        searchResults.innerText = `${renderedClients.length - 1} resultados`
    } else {
        searchResults.innerText = "";
    }

    html.clients.innerHTML = renderedClients.join('');
    html.addActions?.();
}

async function loadPaging(start = 0, increment = 10) {
    let length = html.clientsLength;

    let totalPages = Math.ceil(length / increment);
    let currentPage = Math.round(start / increment) + 1;

    html.pageNumber.innerText = `${currentPage}/${totalPages}`;

    clientsCache[currentPage] = clients;

    let nextPageStart = currentPage == totalPages ? start : (start + increment);
    let previousPageStart = currentPage == 1 ? 0 : (start - increment);

    html.nextButton.onclick = () => {
        if (clientsCache[currentPage + 1] != undefined) {
            clients = clientsCache[currentPage + 1];
            loadTableContent();
            loadPaging(nextPageStart);
        }
        else if (currentPage < totalPages) return updateTable(nextPageStart);
        else return () => { };
    };
    html.previousButton.onclick = () => {
        if (currentPage == 1) return () => { };
        else if (clientsCache[currentPage - 1] != undefined) {
            clients = clientsCache[currentPage - 1];
            loadTableContent();
            loadPaging(previousPageStart);
        }
        else if (currentPage > 1) return updateTable(previousPageStart);
    };
}

function sortTable(order = "default") {
    clientsCache = {};
    html.arrow = {};
    html.arrow[order] = "";

    if (order == html.tableOrder) html.orderReverse = !html.orderReverse;

    html.arrow[order] = html.orderReverse ? "↓" : "↑";

    html.tableOrder = order;
    html.endpoint = "page/sorted";
    html.params = `sortKey=${order}&descending=${html.orderReverse}`;
    updateTable();
}

async function getClients(start = 0, increment = 10) {
    await fetch(`${apiUrl}/api/client/${html.endpoint}?start=${start}&increment=${increment}&${html.params}`, {
        headers: {
            "Authorization": `Bearer ${loggedUser.token}`,
            "Content-Type": "application/json"
        },
    })
        .then(response => {
            if (response.status == 401) {
                localStorage.removeItem("login");
                window.location = "../authentication/login/login.html";
            }
            return response.json()
        })
        .then(data => {
            clients = data;
        });

    await fetch(`${apiUrl}/api/client/stats`, {
        headers: {
            "Authorization": `Bearer ${loggedUser.token}`,
            "Content-Type": "application/json"
        },
    })
        .then(response => {
            if (response.status == 401) {
                localStorage.removeItem("login");
                window.location = "../authentication/login/login.html";
            }
            return response.json()
        })
        .then(data => {
            html.clientsLength = data.clientsLength;
            html.lastMonthClients = data.lastMonthClients;
            html.pendingClients = data.pendingClients;
        });
}

async function updateTable(start = 0, increment = 10) {
    await getClients(start, increment);
    loadPaging(start, increment);
    loadTableContent();
}