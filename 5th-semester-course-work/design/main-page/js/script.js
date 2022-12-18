function flipCard(flashcardButton) {
    let flashcard = null;
    do {
        flashcard = flashcardButton.parentElement;
        flashcardButton = flashcard;
    } while(!flashcard.classList.contains("flashcard"))

    flashcard.classList.toggle("flashcard-flip");
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

    moreInterface.classList.toggle("hidden");
    console.log(moreInterface);
}