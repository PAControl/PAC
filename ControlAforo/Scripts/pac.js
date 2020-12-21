//Change the background color when OnClick the buttons
function ChangeGreen() {
    document.body.style.backgroundColor = "#A5FAB3";
}

function ChangeRed() {
    document.body.style.backgroundColor = "#F0A08B";
}

//Activates/Desactivates the Password PopUp 
function PopUp() {

    var abrirpopup = document.getElementById("abrirpopup"),
        overlay = document.getElementById("overlay")
        popup = document.getElementById("popup"),
        cerrarpopup = document.getElementById("cerrarpopup");
    
        overlay.classList.add("active");
        popup.classList.add("active");
    
}

function ClosePopUp(){
    var abrirpopup = document.getElementById("abrirpopup"),
        overlay = document.getElementById("overlay")
        popup = document.getElementById("popup"),
        cerrarpopup = document.getElementById("cerrarpopup");


        overlay.classList.remove("active");
        popup.classList.remove("active");

}

//Activates/Desactivates the Register PopUp 
function PopUpReg() {

    var abrirpopupReg = document.getElementById("abrirpopupReg"),
        overlayReg = document.getElementById("overlayReg")
    popupReg = document.getElementById("popupReg"),
        cerrarpopupReg = document.getElementById("cerrarpopupReg");


        overlayReg.classList.add("active");
        popupReg.classList.add("active");

}

function ClosePopUpReg() {

    var abrirpopupReg = document.getElementById("abrirpopupReg"),
        overlayReg = document.getElementById("overlayReg")
    popupReg = document.getElementById("popupReg"),
        cerrarpopupReg = document.getElementById("cerrarpopupReg");

 
        overlayReg.classList.remove("active");
        popupReg.classList.remove("active");

}

$(document).ready(function () {
    $.ajax({
        type: "POST",
        url: "~/AforoController/VistaUsuarioUnBotonSalida", // the URL of the controller action method
        success: function () {
            setTimeout(function () { document.getElementById("refresh").reload(true); }, 5000);
        }
    });
})