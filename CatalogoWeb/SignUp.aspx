<%@ Page Title="" Language="C#" MasterPageFile="~/Catalogo.Master" AutoEventWireup="true" CodeBehind="SignUp.aspx.cs" Inherits="CatalogoWeb.SignUp" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="row">
        <div class="col center-col">
            <div class="col-6">
                <div class="card text-center bg-black bg-opacity-25 text-light">
                    <div class="card-header">
                        Registrarse
                    </div>
                    <div class="card-body text-black">
                        <div class="form-floating mb-3">
                            <asp:TextBox ID="txtEmail" CssClass="form-control" runat="server" placeholder="ejemplo@ejemplo.com"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtEmail" ErrorMessage="Este campo es requerido" CssClass="danger"></asp:RequiredFieldValidator>
                            <label for="txtEmail">Ingrese un email</label>
                            <div class="row">
                                <asp:Label ID="lblErrorEmail" CssClass="danger" Visible="false" runat="server" Text="Label"></asp:Label>
                            </div>
                        </div>
                        <div class="form-floating mb-3">
                            <asp:TextBox ID="txtPassword" TextMode="Password" CssClass="form-control" runat="server" placeholder="Contraseña"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtPassword" ErrorMessage="Este campo es requerido" CssClass="danger"></asp:RequiredFieldValidator>
                            <label for="txtPassword">Ingrese una contraseña</label>
                            <div class="row">
                                <asp:Label ID="lblErrorPass" CssClass="danger" Visible="false" runat="server" Text="Label"></asp:Label>
                            </div>
                        </div>
                        <div class="form-floating mb-3">
                            <asp:TextBox ID="txtRepetir" TextMode="Password" CssClass="form-control" runat="server" placeholder="Repetir Contraseña"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="txtRepetir" ErrorMessage="Este campo es requerido" CssClass="danger"></asp:RequiredFieldValidator>
                            <label for="txtPassword">Repita la contraseña ingresada</label>
                            <div class="row">
                                <asp:Label ID="lblErrorRep" CssClass="danger" Visible="false" runat="server" Text="Label"></asp:Label>
                            </div>
                        </div>
                    </div>
                    <div class="card-footer center-row mb-3">
                        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                            <ContentTemplate>
                                <asp:Button ID="btnAceptar" runat="server" CssClass="btn btn-outline-light btn-primary w-120 me-3" Text="Aceptar" OnClick="btnAceptar_Click" />
                            </ContentTemplate>
                        </asp:UpdatePanel>
                        <a href="Default.aspx" class="btn btn-outline-light btn-danger w-120">Cancelar</a>
                    </div>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
