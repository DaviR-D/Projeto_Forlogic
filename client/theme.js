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

    if(html.themeIcon) html.themeIcon.innerText = theme == "dark" ? `light_mode` : `dark_mode`;
}