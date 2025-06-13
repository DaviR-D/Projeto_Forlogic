document.addEventListener("DOMContentLoaded", function () {
    let content = document.getElementById("mainContent")
    content.insertAdjacentHTML('afterbegin',
        `
        <div class="statsContainersWraper">
            <article class="statsContainer">
                <p id="total" class="stats" style="color: rgb(79, 79, 255);"></p>
                <span class="statsTitle">Total de cadastros</span>
            </article>
            <article class="statsContainer">
                <p id="lastMonth" class="stats" style="color: rgb(52, 255, 52);"></p>
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


function insertDashboardData() {
    const [totalRegistrations, pendingRegistrations, lastMonthRegistrations] = calculateStats();

    html.dashboard.tableTop.insertAdjacentHTML('beforeend',
        `
        <h1><strong>Últimos cadastros</strong></h1>
        `
    )

    html.dashboard.total.innerText = totalRegistrations;
    html.dashboard.pending.innerText = pendingRegistrations;
    html.dashboard.lastMonth.innerText = lastMonthRegistrations;

    navLink = document.getElementById("dashboardNav")
    navLink.style.borderBottomStyle = "solid";

    loadTableContent("date");

}

function calculateStats() {
    let totalRegistrations = registrations.length;
    let pendingRegistrations = registrations.filter(registration => registration.pending === true).length;
    let lastMonthRegistrations = registrations.filter(registration => checkLastMonth(registration.date)).length;

    return [totalRegistrations, pendingRegistrations, lastMonthRegistrations];
}


function checkLastMonth(registrationDate) {
    let today = new Date();
    let registrationDay = new Date(registrationDate);

    let dateDiference = today - registrationDay;
    let days = dateDiference / (1000 * 60 * 60 * 24);

    return days <= 30;
}
