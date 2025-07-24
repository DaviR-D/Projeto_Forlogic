document.addEventListener("DOMContentLoaded", async function () {
    await updateTable();
    html.report = {};
    getReportElements();
    insertReportData();
});

function getReportElements() {
    html.report.tableTop = document.getElementById("tableTop");
}

function insertReportData() {
    html.report.tableTop.insertAdjacentHTML('beforeend',
        `
        <h1><strong>Lista de usuários</strong></h1>
        <button onclick="printTable()">IMPRIMIR</button>
        `
    )

    navLink = document.getElementById("reportNav")
    navLink.style.backgroundColor = "var(--highlight-color)";
}

async function printTable() {
    document.body.classList.add("blur");
    await updateTable(0, html.clientsLength);
    window.print();
    clientsCache = {};
    updateTable();
    document.body.classList.remove("blur");
}