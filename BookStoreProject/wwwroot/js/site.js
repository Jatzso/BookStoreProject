// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

function alertaSuscripcion(Email) {
    if (validarEmail()) {
        return true
    } else {
        return false
    }
    
}

function validarEmail() {
	var email = document.getElementById("Email");
	if (email.value == "") {
		alert("Debe ingresar un Email");
		return false
	} else {
		alert("¡Felicidades! Se ha suscripto a la libería")
		return true
	}

}

