<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Error.aspx.cs" Inherits="CatalogoWeb.Error" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>Error</title>
    <link href="Contenido/Estilos.css" rel="stylesheet" />
    <link href="https://cdn.jsdelivr.net/npm/bootstrap@5.3.0-alpha1/dist/css/bootstrap.min.css" rel="stylesheet" integrity="sha384-GLhlTQ8iRABdZLl6O3oVMWSktQOp6b7In1Zl3/Jr59b6EGGoI1aFkw7cmDA6j6gD" crossorigin="anonymous" />
</head>
<body>
    <form id="form1" runat="server">
        <div class="container center-row grande">
            <div class="col-7">
                <div class="card text-center mt-5 bg-black bg-opacity-25 bg-gradient text-bg-danger">
                    <div class="card-header">
                        Hubo un error al ingresar a la pagina.
                    </div>
                    <div class="card-body">
                        <div class="mb-3">
                            <asp:Label ID="lblErrorTitulo" runat="server" CssClass="card-text danger" Text="Label"></asp:Label>
                        </div>
                        <div class="mb-3">
                            <asp:Label ID="lblError" CssClass="card-text" runat="server" Text="Label"></asp:Label>
                        </div>
                    </div>
                    <div class="card-footer text-muted">
                        <a href="Default.aspx" class="btn btn-outline-light btn-primary w-170">Volver al inicio</a>
                        <%if (!Validaciones.Validar.sesion((ModeloDominio.Usuario)Session["usuario"]))
                            {

                        %>
                        <a href="Login.aspx" class="btn btn-outline-light btn-primary w-170">Iniciar Sesión</a>
                        <a href="Register.aspx" class="btn btn-outline-light btn-primary w-170">Registrarse</a>
                        <%}
                            else
                            {  %>
                        <asp:Button ID="btnCerrarSesion" runat="server" Text="Cerrar Sesión" CssClass="btn btn-outline-light btn-primary w-170" OnClick="btnCerrarSesion_Click" />
                        <%}  %>
                    </div>
                </div>
            </div>
        </div>
    </form>
</body>
<script src="https://cdn.jsdelivr.net/npm/bootstrap@5.3.0-alpha1/dist/js/bootstrap.bundle.min.js" integrity="sha384-w76AqPfDkMBDXo30jS1Sgez6pr3x5MlQ1ZAGC+nuZB+EYdgRZgiwxhTBTkF7CXvN" crossorigin="anonymous"></script>
</html>
