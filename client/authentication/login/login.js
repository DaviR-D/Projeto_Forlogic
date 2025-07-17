document.addEventListener("DOMContentLoaded", function () {
  getAuthElements();
  applyTheme(pageTheme ?? "default");
  html.passwordInput.addEventListener("keydown", e => {
    if (e.key === "Enter") tryLogin();
  })
})

function parseToken(token) {
  const payload = token.split('.')[1];
  const decodedPayload = atob(payload.replace(/-/g, '+').replace(/_/g, ';'));
  return JSON.parse(decodedPayload);
}

