if (window.location.pathname.split("/").pop() == "login.html") {
    if (localStorage.getItem("login")) {
        window.location = "../../dashboard/dashboard.html";
    }
}

else if (!localStorage.getItem("login")) {
    window.location = "../authentication/login/login.html";
}

