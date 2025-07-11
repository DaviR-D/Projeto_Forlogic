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

async function printTable() {
    document.body.classList.add("blur");
    await updateTable(html.tableOrder, 0, html.registrationsLength);
    window.print();
    updateTable(html.tableOrder);
    document.body.classList.remove("blur");
}