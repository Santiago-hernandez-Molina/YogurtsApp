let menuBtn = document.getElementById("btn-menu");
let menu = document.getElementById("menu");
let menu2 = document.getElementById("menu-2");
let logo = document.getElementById("logo");


const changeMenuStatus = () =>
{
    if (menu.className == "nav-section") {
        menu.className += " nav-section-disable"
        menu2.className += " nav-section-disable"
        logo.className = "logo-custom"
    }else{
        menu.className = "nav-section"
        menu2.className = "nav-section"
        logo.className += " logo-disable"
    }
}
menuBtn.addEventListener('click',changeMenuStatus)
