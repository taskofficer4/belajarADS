function checkContainsCollapsedSidebar() {
    return $('#master-page-content').attr("class").includes("collapsed") ||
        $('#master-page-content').attr("class").includes("left-collapsed") ||
        $('#master-page-content').attr("class").includes("right-collapsed");
}

function expandCollapseLeftSidebar(id) {
    if (checkContainsCollapsedSidebar()) {
        if ($('#master-page-content').attr("class").includes("left-collapsed")) {
            $('#master-page-content').removeClass("left-collapsed");
            $(id).removeClass("swipe-right-sidebar");
            $(id).addClass("swipe-left-sidebar");
        } else if ($('#master-page-content').attr("class").includes("right-collapsed")) {
            $('#master-page-content').removeClass("right-collapsed");
            $('#master-page-content').addClass("collapsed");
            $(id).removeClass("swipe-left-sidebar");
            $(id).addClass("swipe-right-sidebar");
        } else {
            $('#master-page-content').removeClass("collapsed");
            $('#master-page-content').addClass("right-collapsed");
            $(id).removeClass("swipe-right-sidebar");
            $(id).addClass("swipe-left-sidebar");
        }
    } else {
        $('#master-page-content').addClass("left-collapsed"); 
        $(id).removeClass("swipe-left-sidebar");
        $(id).addClass("swipe-right-sidebar");
    }
}

function expandCollapseRightSidebar(id) {
    if (checkContainsCollapsedSidebar()) {
        if ($('#master-page-content').attr("class").includes("left-collapsed")) {
            $('#master-page-content').removeClass("left-collapsed");
            $('#master-page-content').addClass("collapsed");
            $(id).removeClass("swipe-right-sidebar");
            $(id).addClass("swipe-left-sidebar");
        } else if ($('#master-page-content').attr("class").includes("right-collapsed")) {
            $('#master-page-content').removeClass("right-collapsed");
            $(id).removeClass("swipe-left-sidebar");
            $(id).addClass("swipe-right-sidebar");
        } else {
            $('#master-page-content').removeClass("collapsed");
            $('#master-page-content').addClass("left-collapsed");
            $(id).removeClass("swipe-left-sidebar");
            $(id).addClass("swipe-right-sidebar");
        }
    } else {
        $('#master-page-content').addClass("right-collapsed");
        $(id).removeClass("swipe-right-sidebar");
        $(id).addClass("swipe-left-sidebar");
    }
}

function showHideMenu(child, parentIcon) {
    $('.' + child).each(function () {
        if ($(this).attr("class").includes("d-none")) {
            $('#' + parentIcon).attr("src", "../assets/angle-up.svg");
            $(this).removeClass("d-none");
        } else {
            $('#' + parentIcon).attr("src", "../assets/angle-down.svg");
            $(this).addClass("d-none");
        }
    });
}