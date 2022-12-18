const burgerMenu = document.querySelector(".burger-menu");
const headerNav = document.querySelector(".page-header-nav");
burgerMenu.addEventListener("click", () => {
    burgerMenu.classList.toggle("_open");
    headerNav.classList.toggle("_open");

});

function flipCard(flashcardButton) {
    let flashcard = null;
    do {
        flashcard = flashcardButton.parentElement;
        flashcardButton = flashcard;
    } while(!flashcard.classList.contains("flashcard"))

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
    console.log(moreInterface);
}