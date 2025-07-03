let apiUrl = "http://localhost:5204";

let loggedUser = JSON.parse(localStorage.getItem("login"));

let html = {};

let registrations = [];

let pageTheme = localStorage.getItem("theme");

document.addEventListener("DOMContentLoaded", function () {
    loadLayout();
    applyTheme(pageTheme ?? "default");
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
        loadTableContent();
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


    html.pageNumber = document.getElementById("pageNumber");
    html.tableOrder = "";
    html.arrow = {};
    html.orderReverse = false;
}

async function loadTableContent(order = "default") {
    await sortTable(order);

    renderedRegistrations = [];
    registrations.forEach(register => {
        let rowContent = `${register.name}${register.email.split("@")[0]}`.toLowerCase();

        if (rowContent.includes(html.search.value.toLowerCase())) {
            renderedRegistrations.push(
                `<tr>
                    <td>${register.name}</td>
                    <td>${register.email}</td>
                    <td><span style="border-radius:5px; padding:5px;" class=${register.status == "Ativo" ? "active" : "inactive"}>${register.status}</span></td>
                     <td>${new Date(register.date).toLocaleDateString('pt-BR')}</td>
                    <td class="actions" style="display: none;">
                        <button class="editButton material-symbols-outlined" onclick="editRegistration('${register.id}')">edit</button>
                        <button class="deleteButton material-symbols-outlined" onclick="showDeleteConfirmation('${register.id}')">delete</button>
                    </td>
                </tr>`
            );
        }
        if (search.value.length > 0) {
            searchResults.innerText = `${renderedRegistrations.length} resultados`
        } else {
            searchResults.innerText = "";
        }

    });

    loadPaging();
}

function loadPaging(start = 0, increment = 10) {
    if (start >= (renderedRegistrations.length))
        start -= increment;

    if (start < 0)
        start = 0;

    let totalPages = Math.ceil(renderedRegistrations.length / increment);
    let currentPage = Math.round(start / increment) + 1;

    html.pageNumber.innerText = `${currentPage}/${totalPages}`;

    html.registrations = document.getElementById("registrations");

    let nextButton = document.getElementById("nextButton");
    nextButton.onclick = () => loadPaging(start + increment);

    let previousButton = document.getElementById("previousButton");
    previousButton.onclick = () => loadPaging(start - increment);


    let stop = start + increment;

    let page = renderedRegistrations.slice(start, stop);
    page.unshift(`
        <tr id="tableHeader">
            <th class="column" onclick="loadTableContent('name')">Nome ${html.arrow.name ? html.arrow.name : ""}</th>
            <th class="column" onclick="loadTableContent('email')">Email ${html.arrow.email ? html.arrow.email : ""}</th>
            <th class="column" onclick="loadTableContent('status')">Status ${html.arrow.status ? html.arrow.status : ""}</th>
            <th class="column" onclick="loadTableContent('date')">Data ${html.arrow.date ? html.arrow.date : ""}</th>
        </tr>
        `);

    html.registrations.innerHTML = page.join('');
    html.addActions?.();
}

async function sortTable(order = "default") {
    let sortBy = {
        "name": (registrations) => {
            html.arrow.name = "";
            return registrations.sort((a, b) => a.name.localeCompare(b.name))
        },
        "email": (registrations) => {
            html.arrow.email = "";
            return registrations.sort((a, b) => a.email.localeCompare(b.email))
        },
        "status": (registrations) => {
            html.arrow.status = "";
            return registrations.sort((a, b) => a.status.localeCompare(b.status))
        },
        "date": (registrations) => {
            html.arrow.date = "";
            return registrations.sort((a, b) => new Date(b.date) - new Date(a.date));
        },
    }

    if (order == "default") {
        html.arrow = {};
        registrations = [];
        await getStorageRegistrations();
    } else if (order == html.tableOrder) {
        html.orderReverse = !html.orderReverse;
        registrations.reverse();
    } else {
        html.arrow = {};
        sortBy[order](registrations);
        html.orderReverse = false;
    }

    let selectedColumn = Object.keys(html.arrow)[0];
    html.arrow[selectedColumn] = html.orderReverse ? "↓" : "↑";

    html.tableOrder = order;
}

async function getStorageRegistrations() {
    await fetch(`${apiUrl}/register`)
        .then(response => { return response.json() })
        .then(data => {
            registrations = Object.values(data);
        });
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




