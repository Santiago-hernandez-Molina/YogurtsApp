let menuBtn = document.getElementById("btn-menu");
let menu = document.getElementById("menu");
let menu2 = document.getElementById("menu-2");


const changeMenuStatus = () =>
{
    if (menu.className == "nav-section") {
        menu.className += " nav-section-disable"
        menu2.className += " nav-section-disable"
    }else{
        menu.className = "nav-section"
        menu2.className = "nav-section"
    }
}
menuBtn.addEventListener('click',changeMenuStatus)
