let logs = []

document.addEventListener("DOMContentLoaded", async function () {
    html.logs = {};
    getLogsElements();
    insertLogsData();
    await getLogs();
    renderLogs();
});

function getLogsElements() {
    html.logs.tableTop = document.getElementById("tableTop");
    html.logs.table = document.getElementById("clients");
}

function insertLogsData() {
    html.logs.tableTop.insertAdjacentHTML('beforeend',
        `
        <h1><strong>Logs</strong></h1>
        `
    )

    navLink = document.getElementById("logsNav")
    navLink.style.backgroundColor = "var(--highlight-color)";
}

async function getLogs() {
    await fetch(`${apiUrl}/api/log/`, {
        method: "GET",
        headers: {
            "Authorization": `Bearer ${loggedUser.token}`,
            "Content-Type": "application/json"
        }
    })
        .then(response => { return response.json() })
        .then(data => logs = data.logs)
}

function renderLogs() {
    let renderedLogs =
        [`
        <tr id="tableHeader">
            <th class="column">Usuário</th>
            <th class="column">Cliente</th>
            <th class="column">Ação</th>
            <th class="column">TimeStamp</th>
        </tr>`
        ];

    logs.forEach(log => {
        renderedLogs.push(
            `
            <tr>
                <td>${log.userEmail}</td>
                <td>${log.clientEmail}</td>
                <td>${log.action}</td>
                <td>${new Date(log.timeStamp).toLocaleDateString('pt-BR', {
                year: 'numeric',
                month: '2-digit',
                day: '2-digit',
                hour: '2-digit',
                minute: '2-digit',
                second: '2-digit',
                hour12: false
            })}</td>
            </tr>
            `
        );
    });

    html.logs.table.innerHTML = renderedLogs.join('');
}