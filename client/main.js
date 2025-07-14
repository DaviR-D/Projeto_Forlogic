let apiUrl = "http://localhost:5204";

let loggedUser = JSON.parse(localStorage.getItem("login"));

let html = {};

html.endpoint = "page"
html.params = ""

let registrations = [];
let registrationsCache = {};

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
            <a href="../login/login.html" id="exit">SAIR</a>
        </div>
    </header>
    `);

    html.exit = document.getElementById("exit");

    html.search = document.getElementById("search");

    html.searchResults = document.getElementById("searchResults");

    html.userDisplay = document.getElementById("userDisplay");
    html.userDisplay.innerText = loggedUser.name;

    html.search.addEventListener("input", function () {
        registrationsCache = {}
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
                <table id="registrations"></table>
            </div>
            <div class="tablePaging">
                <button id="previousButton" class="material-symbols-outlined">arrow_back</button>
                <span id="pageNumber"></span>
                <button id="nextButton" class="material-symbols-outlined">arrow_forward</button>
            </div>
        </article>
        `
    )
    html.registrations = document.getElementById("registrations");
    html.pageNumber = document.getElementById("pageNumber");
    html.nextButton = document.getElementById("nextButton");
    html.previousButton = document.getElementById("previousButton");
    html.tableOrder = "default";
    html.arrow = {};
    html.orderReverse = false;
}

async function loadTableContent() {
    renderedRegistrations =
        [`
        <tr id="tableHeader">
            <th class="column" onclick="sortTable('name')">Nome ${html.arrow.name ? html.arrow.name : ""}</th>
            <th class="column" onclick="sortTable('email')">Email ${html.arrow.email ? html.arrow.email : ""}</th>
            <th class="column" onclick="sortTable('status')">Status ${html.arrow.status ? html.arrow.status : ""}</th>
            <th class="column" onclick="sortTable('date')">Data ${html.arrow.date ? html.arrow.date : ""}</th>
        </tr>`
        ];
    registrations.forEach(register => {
        renderedRegistrations.push(
            `
            <tr>
                <td>${register.name}</td>
                <td>${register.email}</td>
                <td><span style="border-radius:5px; padding:5px;" class=${register.status == "Ativo" ? "active" : "inactive"}>${register.status}</span></td>
                <td>${new Date(register.date).toLocaleDateString('pt-BR')}</td>
                <td class="actions" style="display: none;">
                    <button class="editButton material-symbols-outlined" onclick="editRegistration('${register.id}')">edit</button>
                    <button class="deleteButton material-symbols-outlined" onclick="showDeleteConfirmation('${register.id}')">delete</button>
                </td>
            </tr>
            `
        );
    });

    if (search.value.length > 0) {
        searchResults.innerText = `${renderedRegistrations.length - 1} resultados`
    } else {
        searchResults.innerText = "";
    }

    html.registrations.innerHTML = renderedRegistrations.join('');
    html.addActions?.();
}

async function loadPaging(start = 0, increment = 10) {
    let length = html.registrationsLength;

    let totalPages = Math.ceil(length / increment);
    let currentPage = Math.round(start / increment) + 1;

    html.pageNumber.innerText = `${currentPage}/${totalPages}`;

    registrationsCache[currentPage] = registrations;

    let nextPageStart = currentPage == totalPages ? start : (start + increment);
    let previousPageStart = currentPage == 1 ? 0 : (start - increment);

    html.nextButton.onclick = () => {
        if (registrationsCache[currentPage + 1] != undefined) {
            registrations = registrationsCache[currentPage + 1];
            loadTableContent();
            loadPaging(nextPageStart);
        }
        else if (currentPage < totalPages) return updateTable(nextPageStart);
        else return () => { };
    };
    html.previousButton.onclick = () => {
        if (currentPage == 1) return () => { };
        else if (registrationsCache[currentPage - 1] != undefined) {
            registrations = registrationsCache[currentPage - 1];
            loadTableContent();
            loadPaging(previousPageStart);
        }
        else if (currentPage > 1) return updateTable(previousPageStart);
    };
}

function sortTable(order = "default") {
    registrationsCache = {};
    html.arrow = {};
    html.arrow[order] = "";

    if (order == html.tableOrder) html.orderReverse = !html.orderReverse;

    html.arrow[order] = html.orderReverse ? "↓" : "↑";

    html.tableOrder = order;
    html.endpoint = "page/sorted";
    html.params = `sortKey=${order}&descending=${html.orderReverse}`;
    updateTable();
}

async function getRegistrations(start = 0, increment = 10) {
    await fetch(`${apiUrl}/api/registration/${html.endpoint}?start=${start}&increment=${increment}&${html.params}`)
        .then(response => { return response.json() })
        .then(data => {
            registrations = data.registrations;
            html.registrationsLength = data.registrationsLength;
            html.lastMonthRegistrations = data.lastMonthRegistrations;
            html.pendingRegistrations = data.pendingRegistrations;
        });
}

async function updateTable(start = 0, increment = 10) {
    await getRegistrations(start, increment);
    loadPaging(start, increment);
    loadTableContent();
}

function applyTheme(theme = "default") {
    let themeColors = {
        default: {
            '--main-color': "white",
            '--second-color': "rgb(225, 225, 225)",
            '--font-color': "rgb(74, 74, 74)",
            '--highlight-color': "rgb(195, 195, 195)",
            '--border-color': "rgb(203, 203, 203)",
            "--activeBackground": "rgb(204, 255, 204)",
            "--activeFontColor": "rgb(0, 100, 0)",
            "--inactiveBackground": "rgb(255, 215, 204)",
            "--inactiveFontColor": "rgb(150, 0, 0)",
        },
        dark: {
            '--main-color': "rgb(24, 26, 27)",
            '--second-color': "rgb(44, 47, 49)",
            '--font-color': "rgb(210, 210, 210)",
            '--highlight-color': "rgb(70, 75, 78)",
            '--border-color': "rgb(60, 64, 66)",
            "--activeBackground": "rgb(154, 205, 154)",
            "--activeFontColor": "rgb(0, 100, 0)",
            "--inactiveBackground": "rgb(205, 165, 154)",
            "--inactiveFontColor": "rgb(150, 0, 0)",
        }
    };

    let themeVariables = [
        "--main-color",
        "--second-color",
        "--font-color",
        "--highlight-color",
        "--border-color",
        "--activeBackground",
        "--inactiveBackground"
    ]

    themeVariables.forEach(variable => {
        document.documentElement.style.setProperty(variable, themeColors[theme][variable])
    });

    localStorage.setItem("theme", theme);
    pageTheme = theme;

    html.themeIcon.innerText = theme == "dark" ? `light_mode` : `dark_mode`;
}