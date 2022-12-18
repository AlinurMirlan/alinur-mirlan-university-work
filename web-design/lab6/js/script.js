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
let slidersCount = slider.childElementCount - 1;
let minLeft = -(slidersCount * sliderWidth);
let position = 0;
sliderPreviousButton.addEventListener("click", (eventArgs) => {
    sliderWidth = sliderWindow.getBoundingClientRect().width;
    if (position < 0) {
        position += sliderWidth;
    } else {
        position = minLeft;
    }
    slider.style.left = `${position}px`;
});
sliderNextButton.addEventListener("click", (eventArgs) => {
    sliderWidth = sliderWindow.getBoundingClientRect().width;
    if (position > minLeft) {
        position -= sliderWidth;
    } else {
        position = 0;
    }
    slider.style.left = `${position}px`;
});