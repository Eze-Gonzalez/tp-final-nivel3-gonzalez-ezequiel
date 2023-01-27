<%@ Page Title="" Language="C#" MasterPageFile="~/Catalogo.Master" AutoEventWireup="true" CodeBehind="ChangePass.aspx.cs" Inherits="CatalogoWeb.ChangePass" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="row">
        <div class="col center-col">
            <div class="col-6">
                <div class="card text-center bg-black bg-opacity-25 text-light">
                    <div class="card-header">
                        <asp:Label ID="lblError" runat="server" Text="Session['Error']" CssClass="danger"></asp:Label>
                        <asp:Label ID="lblCambiar" runat="server" Text="Cambiar Contraseña" CssClass="alert-link"></asp:Label>
                    </div>
                    <div class="card-body text-black">
                        <div class="form-floating mb-3">
                            <asp:TextBox ID="txtEmail" TextMode="Email" CssClass="form-control" runat="server" placeholder="Email"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" ControlToValidate="txtEmail" runat="server" ErrorMessage="Este campo es requerido" CssClass="danger"></asp:RequiredFieldValidator>
                            <label for="txtEmail">Ingrese su Email</label>
                        </div>
                        <div class="form-floating mb-3">
                            <asp:TextBox ID="txtPassword" TextMode="Password" CssClass="form-control" runat="server" placeholder="Contrseña"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator2" ControlToValidate="txtPassword" runat="server" ErrorMessage="Este campo es requerido" CssClass="danger"></asp:RequiredFieldValidator>
                            <label for="txtPassword">Ingrese una contraseña</label>
                        </div>
                        <div class="form-floating mb-3">
                            <asp:TextBox ID="txtRepetir" TextMode="Password" CssClass="form-control" runat="server" placeholder="Contrseña"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator3" ControlToValidate="txtRepetir" runat="server" ErrorMessage="Este campo es requerido" CssClass="danger"></asp:RequiredFieldValidator>
                            <label for="txtRepetir">Repita la contraseña ingresada</label>
                        </div>
                    </div>
                    <div class="card-footer center-row mb-3">
                        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                            <ContentTemplate>
                                <asp:Button ID="btnAceptar" runat="server" CssClass="me-3 btn btn-outline-light btn-primary w-120" Text="Aceptar" OnClick="btnAceptar_Click" />
                            </ContentTemplate>
                        </asp:UpdatePanel>
                        <a href="Default.aspx" class="btn btn-outline-light btn-danger w-120">Cancelar</a>
                    </div>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
