let form = document.querySelector('form');
let baseUrl = "";
let loggedInUrl = "";
form.addEventListener('submit', async (e) => {
    e.preventDefault();
    console.log(e);

    await login();
});

async function login() {
    let el = document.getElementById("input-login-username");
    let url = (new URL(currentUrl + "/loginform" + '?userName=' + el.value, document.location)).href;
    console.log("newurl", url);
    document.querySelector('button').innerText = "LOADING...";
    document.getElementsByTagName("button")
    var json = await fetch(url).then((data) => data.json());
    document.querySelector('button').innerText = "Login";

  
    if (json.Success) {
        document.querySelector('button').innerText = "Login Success. Please wait...";
        location.href = loggedInUrl;
    } else {
        alert(json.Message);
    }
}