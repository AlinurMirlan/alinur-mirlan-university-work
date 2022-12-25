const pageTitle = document.getElementsByTagName("title")[0].innerText;
const pageHeader = document.querySelector(".page-header-nav-left");
const pageHeaderTitles = pageHeader.children;
for (let i = 0; i < pageHeaderTitles.length; i++) {
    let pageHeaderTitle = pageHeaderTitles[i].innerText.toLowerCase();
    if (pageTitle.toLowerCase().includes(pageHeaderTitle)) {
        pageHeaderTitles[i].classList.add("page-chosen");
    } else {
        pageHeaderTitles[i].classList.remove("page-chosen");
    }
}