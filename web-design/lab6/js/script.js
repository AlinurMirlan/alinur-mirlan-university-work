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
let sliders = document.querySelector(".quotes").children;
let sliderWindow = document.querySelector(".quotes-section-content");
let sliderWidth = sliderWindow.getBoundingClientRect().width;
let slidersCount = slider.childElementCount - 1;
let minLeft = -(slidersCount * sliderWidth);
let currentSlider = 0;
sliders[currentSlider].style.display = "inline-flex";
let slidersLeft = 0;
let position = 0;
sliderPreviousButton.addEventListener("click", (eventArgs) => {

});
sliderNextButton.addEventListener("click", (eventArgs) => {
    currentSlider += 1;
    let sliderWidth = sliderWindow.getBoundingClientRect().width;
    position -= sliderWidth;
    sliders[currentSlider].style.display = "inline-flex";
    slider.style.left = `${position}px`;
    setTimeout(() => {
        slider[currentSlider - 1].style.display = "none";
    }, 300);
});