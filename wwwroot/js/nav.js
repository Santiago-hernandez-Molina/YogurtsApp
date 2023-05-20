let menuBtn = document.getElementById("btn-menu");
let menu = document.getElementById("menu");
let logo = document.getElementById("logo");
let logoImg = document.getElementById("logo-img");
let navbar = document.getElementById("navbar");
let title = document.title;
try {
    let navActiveItem = document.getElementById(title);
    navActiveItem.style.color = 'var(--green)';
} catch (error) {
}



const changeMenuStatus = () =>
{
    if (menu.className == "nav-section") {
        menu.className += " nav-section-enable"
        logo.className += " logo-disable"
        logoImg.className += " logo-disable"
        navbar.className += " navbar-custom-active"
    }else{
        menu.className = "nav-section"
        logo.className = "logo-custom"
        logoImg.className = "logo-img"
        navbar.className = "navbar-custom"
    }
}
menuBtn.addEventListener('click',changeMenuStatus)
