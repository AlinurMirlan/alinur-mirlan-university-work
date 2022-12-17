let burgerMenu = document.querySelector(".burger-menu");
let mobileNav = document.querySelector(".main-header-mobile-nav");
let mainHeader = document.querySelector(".main-header");
burgerMenu.addEventListener("click", (evetArgs) => {
    burgerMenu.classList.toggle("_open");
    mobileNav.classList.toggle("_open");
    mainHeader.classList.toggle("_open");
})

let sliderNextButton = document.querySelector(".slider-next");
let sliderPreviousButton = document.querySelector(".slider-previous");
let slider = document.querySelector(".quotes");
let sliderWindow = document.querySelector(".quotes-section-content");
let sliderWidth = sliderWindow.getBoundingClientRect().width;
let quotesSection = document.querySelector(".quotes-section");
let quotesSectionStyles = getComputedStyle(quotesSection);
let rightPadding = (quotesSectionStyles.paddingRight).substring(0, );
let paddingValue = parseInt(rightPadding.substring(0, rightPadding.length - 2));
let slidersCount = slider.childElementCount - 1;
let minLeft = -(slidersCount * sliderWidth);
let slidersOnTheLeft = 0;
let position = 0;
sliderPreviousButton.addEventListener("click", (eventArgs) => {
    sliderWidth = sliderWindow.getBoundingClientRect().width;
    if (position < 0) {
        position += sliderWidth;
        slidersOnTheLeft -= 1;
    } else {
        slidersOnTheLeft = slidersCount;
        position = minLeft;
    }
    slider.style.left = `${position}px`;
});
sliderNextButton.addEventListener("click", (eventArgs) => {
    sliderWidth = sliderWindow.getBoundingClientRect().width;
    if (position > minLeft) {
        slidersOnTheLeft += 1;
        position -= sliderWidth;
    } else {
        position = 0;
        slidersOnTheLeft = 0;
    }
    slider.style.left = `${position}px`;
});
window.onresize = () => {
    let breakPoint = sliderWidth + (paddingValue * 2);
    let windowWidth = window.innerWidth;
    if (windowWidth < breakPoint) {
        let offset = breakPoint - windowWidth;
        offset = (offset * slidersOnTheLeft);
        console.log("before" + position);
        if (position < 0) {
            position += offset;
            console.log("after" + position);
            slider.style.left = `${position}px`;
        }

    }
};