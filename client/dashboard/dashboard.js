document.addEventListener("DOMContentLoaded", function () {
    let content = document.getElementById("mainContent")
    content.insertAdjacentHTML('afterbegin',
        `
        <div class="statsContainersWrapper">
            <article class="statsContainer">
                <p id="total" class="stats" style="color: rgb(79, 79, 255);"></p>
                <span class="statsTitle">Total de cadastros</span>
            </article>
            <article class="statsContainer" id="centerCard">
                <p id="lastMonth" class="stats" style="color: rgb(0, 181, 0);"></p>
                <span class="statsTitle">Cadastros no último mês</span>
            </article>
            <article class="statsContainer">
                <p id="pending" class="stats" style="color: rgb(255, 39, 39);"></p>
                <span class="statsTitle">Cadastros com pendência de revisão</span>
            </article>
        </div>
        `
    )
    html.dashboard = {};
    getDashboardElements();
    insertDashboardData();
})

function getDashboardElements() {
    html.dashboard.tableTop = document.getElementById("tableTop");
    html.dashboard.total = document.getElementById("total");
    html.dashboard.pending = document.getElementById("pending");
    html.dashboard.lastMonth = document.getElementById("lastMonth");
}

async function insertDashboardData() {
    await updateTable();
    if (html.clientsLength == 0) createDB(loggedUser.token);
    html.orderReverse = true;
    await sortTable("date");


    html.dashboard.tableTop.insertAdjacentHTML('beforeend',
        `
        <h1><strong>Últimos cadastros</strong></h1>
        `
    )

    html.dashboard.total.innerText = html.clientsLength;
    html.dashboard.pending.innerText = html.pendingClients;
    html.dashboard.lastMonth.innerText = html.lastMonthClients;

    navLink = document.getElementById("dashboardNav")
    navLink.style.backgroundColor = "var(--highlight-color)";
}
