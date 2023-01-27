<%@ Page Title="" Language="C#" MasterPageFile="~/Catalogo.Master" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="CatalogoWeb.Login" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="row center-col">
        <div class="col-6 mt-5">
            <div class="card text-center bg-black bg-opacity-25 text-light">
                <div class="card-header h5">
                    Iniciar Sesión
                </div>
                <div class="card-body text-black">
                    <div class="form-floating mb-3">
                        <asp:TextBox ID="txtEmail" CssClass="form-control" runat="server" placeholder="ejemplo@ejemplo.com"></asp:TextBox>
                        <asp:RequiredFieldValidator ErrorMessage="Por favor, ingrese un email para iniciar sesión" CssClass="danger" ControlToValidate="txtEmail" runat="server" />
                        <label for="txtEmail">Ingrese su email</label>
                    </div>
                    <div class="form-floating mb-3">
                        <asp:TextBox ID="txtPassword" TextMode="Password" CssClass="form-control" runat="server" placeholder="Contrseña"></asp:TextBox>
                        <asp:RequiredFieldValidator ErrorMessage="Por favor ingrese una contraseña para iniciar sesión" CssClass="danger" ControlToValidate="txtPassword" runat="server" />
                        <label for="txtPassword">Ingrese su contraseña</label>
                    </div>
                    <div class="mb3">
                        <asp:Label ID="lblErrorLogin" CssClass="danger" Visible="false" runat="server" Text="Label"></asp:Label>
                    </div>
                </div>
                <div class="card-footer center-row mb-3">
                    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                        <ContentTemplate>
                            <asp:Button ID="btnIniciarSesion" runat="server" CssClass="me-3 btn btn-outline-light btn-primary w-120" Text="Iniciar Sesión" OnClick="btnIniciarSesion_Click" />
                        </ContentTemplate>
                    </asp:UpdatePanel>
                    <a href="Default.aspx" class="btn btn-outline-light btn-danger w-120">Cancelar</a>
                </div>
            </div>
            <div class="center-col">
                <div class="row mt-3">
                    <a href="SignUp.aspx" class="text-decoration-none text-light">¿No tiene cuenta? Registrese aquí.</a>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
