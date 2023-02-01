function filtro(titulo, mensajeN) {
    var modal = document.createElement("div");
    modal.id = "filtro";
    modal.style.display = "block";
    modal.className = "modal-completo";
    var title = document.createElement("p");
    title.className = "h4 mt-2";
    var mensaje = document.createElement("p");
    mensaje.className = "card-text";
    var modalContent = document.createElement("div");
    modalContent.className = "m-content text-light text-center";
    var cardBody = document.createElement("div");
    cardBody.className = "card-body center-col";
    var rowTitulo = document.createElement("div");
    rowTitulo.className = "row mb-5";
    var rowMensaje = document.createElement("div");
    rowMensaje.className = "row";
    var footer = document.createElement("div");
    footer.className = "card-footer center-row";
    var boton = document.createElement("button");
    boton.className = "btn btn-outline-light btn-primary w-120 mb-3 mt-4";
    boton.innerHTML = "Aceptar";
    footer.appendChild(boton);
    var iconoInfo = document.createElement("div")
    iconoInfo.className = "swal-icon swal-icon--warning";
    var icono = document.createElement("span");
    icono.className = "swal-icon--warning__body";
    var iconoPunto = document.createElement("span");
    iconoPunto.className = "swal-icon--warning__dot";
    icono.appendChild(iconoPunto)
    iconoInfo.appendChild(icono)
    var closeBtn = document.createElement("span");
    closeBtn.className = "close";
    closeBtn.innerHTML = "&times;";
    closeBtn.onclick = function () {
        modal.style.display = "none";
    };
    boton.onclick = function () {
        modal.style.display = "none";
    };
    title.innerHTML = titulo;
    mensaje.innerText = mensajeN;
    cardBody.appendChild(iconoInfo);
    rowTitulo.appendChild(title);
    rowMensaje.appendChild(mensaje);
    cardBody.appendChild(rowTitulo);
    cardBody.appendChild(rowMensaje);
    cardBody.appendChild(footer);
    modalContent.appendChild(cardBody);
    modal.appendChild(modalContent);
    document.body.appendChild(modal);
}

//Alertas de informacion
function crearAlerta(status, titulo, mensajeN) {
    var modal = document.createElement("div");
    modal.id = "notificacion";
    modal.style.display = "block";
    modal.className = "modal-completo";
    var title = document.createElement("p");
    title.className = "h4 mt-2";
    var mensaje = document.createElement("p");
    mensaje.className = "card-text";
    var modalContent = document.createElement("div");
    modalContent.className = "m-content text-light text-center";
    var cardBody = document.createElement("div");
    cardBody.className = "card-body center-col";
    var rowTitulo = document.createElement("div");
    rowTitulo.className = "row mb-5";
    var rowMensaje = document.createElement("div");
    rowMensaje.className = "row";
    var footer = document.createElement("div");
    footer.className = "card-footer center-row";
    var boton = document.createElement("button");
    boton.className = "btn btn-outline-light btn-primary w-120 mb-3 mt-4";
    boton.innerHTML = "Aceptar";
    footer.appendChild(boton);
    if (status) {
        var icono = document.createElement("div");
        var iconoRing = document.createElement("div");
        var iconoHideCorners = document.createElement("div");
        var iconoLong = document.createElement("span");
        var iconoTip = document.createElement("span");
        icono.className = "swal-icon swal-icon--success";
        iconoRing.className = "swal-icon--success__ring";
        iconoHideCorners.className = "swal-icon--success__hide-corners";
        iconoLong.className = "swal-icon--success__line swal-icon--success__line--long";
        iconoTip.className = "swal-icon--success__line swal-icon--success__line--tip";
        icono.appendChild(iconoLong);
        icono.appendChild(iconoTip);
        icono.appendChild(iconoRing);
        icono.appendChild(iconoHideCorners);
        modalContent.appendChild(icono);
    }
    else {
        var iconoError = document.createElement("div");
        var iconoX = document.createElement("div");
        var iconoLeft = document.createElement("span");
        var iconoRight = document.createElement("span");
        iconoError.className = "swal-icon swal-icon--error";
        iconoX.className = "swal-icon--error__x-mark";
        iconoLeft.className = "swal-icon--error__line swal-icon--error__line--left";
        iconoRight.className = "swal-icon--error__line swal-icon--error__line--right";
        iconoError.appendChild(iconoX);
        iconoX.appendChild(iconoLeft);
        iconoX.appendChild(iconoRight);
        modalContent.appendChild(iconoError);
    }
    var closeBtn = document.createElement("span");
    closeBtn.className = "close";
    closeBtn.innerHTML = "&times;";
    closeBtn.onclick = function () {
        modal.style.display = "none";
    };
    boton.onclick = function () {
        modal.style.display = "none";
    };

    if (mensajeN === "Debe ingresar su email, si el email ingresado es suyo, puede intentar iniciar sesión con ese email haciendo click en Iniciar Sesión, si no tiene un email registrado, haga click en Registrarse.") {
        var login = document.createElement("button");
        login.className = "btn btn-outline-light btn-primary w-120 mb-3 mt-4 ms-3";
        login.innerHTML = "Iniciar Sesión";
        footer.appendChild(login)
        var registrarse = document.createElement("button");
        registrarse.className = "btn btn-outline-light btn-primary w-120 mb-3 mt-4 ms-3";
        registrarse.innerHTML = "Registrarse";
        footer.appendChild(registrarse)
        login.onclick = function () {
            modal.style.display = "none";
            var xhr = new XMLHttpRequest();
            xhr.open("POST", "Support.aspx", true);
            xhr.setRequestHeader("X-Requested-With", "XMLHttpRequest");
            xhr.send();
            window.location.href = "Login.aspx";
        };
        registrarse.onclick = function () {
            modal.style.display = "none";
            var xhr = new XMLHttpRequest();
            xhr.open("POST", "Support.aspx", true);
            xhr.setRequestHeader("X-Requested-With", "XMLHttpRequest");
            xhr.send();
            window.location.href = "Register.aspx";
        };
    }
    if (titulo === "Email no registrado") {
        var registrarse = document.createElement("button");
        registrarse.className = "btn btn-outline-light btn-primary w-120 mb-3 mt-4 ms-3";
        registrarse.innerHTML = "Registrarse";
        footer.appendChild(registrarse)
        registrarse.onclick = function () {
            modal.style.display = "none";
            var xhr = new XMLHttpRequest();
            xhr.open("POST", "Support.aspx", true);
            xhr.setRequestHeader("X-Requested-With", "XMLHttpRequest");
            xhr.send();
            window.location.href = "Register.aspx";
        };
    }
    if (titulo === "Contraseña existente") {
        var login = document.createElement("button");
        login.className = "btn btn-outline-light btn-primary w-120 mb-3 mt-4 ms-3";
        login.innerHTML = "Iniciar Sesión";
        footer.appendChild(login)
        login.onclick = function () {
            modal.style.display = "none";
            var xhr = new XMLHttpRequest();
            xhr.open("POST", "Support.aspx", true);
            xhr.setRequestHeader("X-Requested-With", "XMLHttpRequest");
            xhr.send();
            window.location.href = "Login.aspx";
        };
    }
    title.innerHTML = titulo;
    mensaje.innerText = mensajeN;
    rowMensaje.appendChild(mensaje);
    rowTitulo.appendChild(title);
    cardBody.appendChild(rowTitulo);
    cardBody.appendChild(rowMensaje);
    cardBody.appendChild(footer);
    modalContent.appendChild(cardBody);
    modal.appendChild(modalContent);
    document.body.appendChild(modal);
    if (mensajeN === "La contraseña fue cambiada exitosamente!") {
        boton.onclick = function () {
            modal.style.display = "none";
            window.location.href = "Login.aspx";
        };
    }
}

//Funcion para disparar alerta de tamaño excedido en caso de ser necesario.
function ValidateSize(file) {
    var maxFileSize = 2097152;
    if (file.files && file.files[0].size > maxFileSize) {
        mostrarModalSize();
        file.value = "";
    }
}

function mostrarModalSize() {
    var modal = document.getElementById("modal");
    modal.style.display = "block";

    var span = document.getElementsByClassName("close")[0];
    span.onclick = function () {
        modal.style.display = "none";
    }

    var button = document.getElementsByClassName("c")[0];
    button.onclick = function () {
        modal.style.display = "none";
    }

    window.onclick = function (event) {
        if (event.target == modal) {
            modal.style.display = "none";
        }
    }
}
