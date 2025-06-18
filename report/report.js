document.addEventListener("DOMContentLoaded", function () {
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

function printTable() {
    loadPaging(0, registrations.length);
    window.print()
    loadPaging();
}