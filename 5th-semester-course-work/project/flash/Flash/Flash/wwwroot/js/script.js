const burgerMenu = document.querySelector(".burger-menu");
const headerNav = document.querySelector(".page-header-nav");
burgerMenu.addEventListener("click", () => {
    burgerMenu.classList.toggle("_open");
    headerNav.classList.toggle("_open");

});

const deletionForms = document.getElementsByClassName("deletion-form");
for (const element of deletionForms) {
    element.addEventListener(
        "submit",
        function (event) {
            let confirmed = window.confirm("Confirm the deletion.");
            if (!confirmed) {
                event.preventDefault();
            }
        }, false);
}

function flipCard(flashcardButton) {
    let flashcard = null;
    do {
        flashcard = flashcardButton.parentElement;
        flashcardButton = flashcard;
    } while (!flashcard.classList.contains("flashcard"))

    flashcard.classList.toggle("_flashcard-flip");
}

function showAdditionalFunctionality(moreButton) {
    const moreButtonParent = moreButton.parentElement;
    const moreButtonParentChildren = moreButtonParent.children;
    let moreInterface = null;
    for (const child of moreButtonParentChildren) {
        if (child.classList.contains("more-interface")) {
            moreInterface = child;
            break;
        }
    }

    moreInterface.classList.toggle("_hidden");
}

function showProfileInterface() {
    const profileInterface = document.querySelector(".profile-interface");
    profileInterface.classList.toggle("_hidden");
}

const pageTitle = document.getElementsByTagName("title")[0].innerText;
const pageHeader = document.querySelector(".page-header-nav-left");
const pageHeaderTitles = pageHeader.children;
for (const element of pageHeaderTitles) {
    let pageHeaderTitle = element.innerText.toLowerCase();
    if (pageTitle.toLowerCase().includes(pageHeaderTitle)) {
        element.classList.add("page-chosen");
    } else {
        element.classList.remove("page-chosen");
    }
}