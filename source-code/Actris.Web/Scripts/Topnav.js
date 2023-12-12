window.onclick = function (e) {
    console.log(e.target);
    if (!e.target.matches('.topnav-dropdown-children-wrapper') &&
        !e.target.matches('.topnav-dropdown-subchildren-wrapper') && 
        !e.target.matches('.topnav-dropdown-parent') &&
        !e.target.matches('.topnav-dropdown-parent-link') &&
        !e.target.matches('.topnav-dropdown-children-link') &&
        !e.target.matches('.topnav-dropdown-subchildren-link')) {

        console.log("masuk");
        closeTopnav();
    }
}

function openTopnavChildren(idName) {
    closeAllChildren(idName);
    closeAllSubChildren(idName);
    if ($('#' + idName).attr("class").includes("d-none")) {
        $('#' + idName).removeClass("d-none");
    } else {
        $('#' + idName).addClass("d-none");
    }
}

function closeAllChildren(idName) {
    $('.topnav-dropdown-children-wrapper').each(function () {
        if (!$(this).attr("class").includes("d-none") && $(this).attr("id") != idName) {
            $(this).addClass("d-none");
        }
    });
}

function openTopnavSubChildren(idName) {
    closeAllSubChildren(idName);
    if ($('#' + idName).attr("class").includes("d-none")) {
        $('#' + idName).removeClass("d-none");
    } else {
        $('#' + idName).addClass("d-none");
    }
}

function closeAllSubChildren(idName) {
    $('.topnav-dropdown-subchildren-wrapper').each(function () {
        if (!$(this).attr("class").includes("d-none") && $(this).attr("id") != idName) {
            $(this).addClass("d-none");
        }
    });
}

function closeTopnav() {
    closeAllChildren("");
    closeAllSubChildren("");
}