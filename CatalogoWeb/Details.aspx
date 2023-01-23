<%@ Page Title="" Language="C#" MasterPageFile="~/Catalogo.Master" AutoEventWireup="true" CodeBehind="Details.aspx.cs" Inherits="CatalogoWeb.Details" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="card text-center bg-black bg-opacity-25 text-bg-dark">
        <div class="card-header bg-black bg-opacity-50">
            <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                <ContentTemplate>
                    <ul class="nav nav-pills card-header-pills center-row">
                        <li class="nav-item me-3">
                            <asp:Button ID="btnRegresar" runat="server" Text="Volver al inicio" CssClass="btn btn-outline-light btn-primary w-120" OnClick="btnRegresar_Click" />
                        </li>
                        <%if (!Eliminar)
                            {
                        %>
                        <%if (Validaciones.Validar.admin(Session["usuario"]))
                            {  %>
                        <li class="nav-item">
                            <asp:Button ID="btnModificar" runat="server" Text="Modificar" OnClick="btnModificar_Click" CssClass="btn btn-outline-light btn-warning w-120" />
                        </li>
                        <li class="nav-item ms-3">
                            <asp:Button ID="btnEliminar" runat="server" Text="Eliminar" OnClick="btnEliminar_Click" CssClass="btn btn-outline-light btn-danger w-120" />
                        </li>
                        <%} %>
                        <%}
                            else
                            {  %>
                        <li class="nav-item ms-3">
                            <asp:Label ID="lblConfirmación" CssClass="form-control-plaintext text-danger alert-link" runat="server" Text="Label"></asp:Label>
                        </li>
                        <li class="nav-item ms-3">
                            <asp:Button ID="btnConfirmar" runat="server" Text="Confirmar" OnClick="btnConfirmar_Click" CssClass="btn btn-outline-light btn-success w-120" />
                        </li>
                        <li class="nav-item ms-3">
                            <asp:Button ID="btnCancelar" runat="server" Text="Cancelar" OnClick="btnCancelar_Click" CssClass="btn btn-outline-light btn-primary w-120" />
                        </li>
                        <%} %>
                    </ul>
                </ContentTemplate>
            </asp:UpdatePanel>
        </div>
        <div class="card-body">
            <div class="row">
                <div class="col-6 border-end">
                    <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                        <ContentTemplate>
                            <%if (Validaciones.Validar.sesion(Session["usuario"]))
                                { %>
                            <div class="col-1">
                                <asp:ImageButton ID="imgFav" CssClass="btn h-42" runat="server" OnClick="imgFav_Click" />
                            </div>
                            <%}  %>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                    <div class="col-5 content">
                        <div class="mb-3">
                            <asp:Image ID="imgProducto" ImageUrl="..." CssClass="img-fluid img-details" runat="server" />
                        </div>
                    </div>
                </div>
                <div class="col center-col">
                    <div class="col-7">
                        <div class="mb-4">
                            <asp:Label ID="lblId" runat="server" Text="Id" CssClass="card-text"></asp:Label>
                            <asp:TextBox ID="txtId" CssClass="form-control text-center text-bg-dark" Enabled="false" runat="server"></asp:TextBox>
                        </div>
                        <div class="mb-4">
                            <asp:Label ID="lblCodigo" runat="server" Text="Código" CssClass="card-text"></asp:Label>
                            <asp:TextBox ID="txtCodigo" CssClass="form-control text-center text-bg-dark" Text="s01" Enabled="false" runat="server"></asp:TextBox>
                        </div>
                        <div class="mb-4">
                            <asp:Label ID="LblNombre" runat="server" Text="Nombre" CssClass="card-text"></asp:Label>
                            <asp:TextBox ID="txtNombre" CssClass="form-control text-center text-bg-dark" Enabled="false" runat="server"></asp:TextBox>
                        </div>
                        <div class="mb-4">
                            <asp:Label ID="lblDescripcion" runat="server" Text="Descripción" CssClass="card-text"></asp:Label>
                            <asp:TextBox ID="txtDescripcion" CssClass="form-control text-center text-bg-dark" TextMode="MultiLine" Enabled="false" runat="server" Style="height: 180px"></asp:TextBox>
                        </div>
                        <div class="mb-4">
                            <asp:Label ID="lblCategoria" runat="server" Text="Categoría" CssClass="card-text"></asp:Label>
                            <asp:TextBox ID="txtCategoria" CssClass="form-control text-center text-bg-dark" Enabled="false" runat="server"></asp:TextBox>
                        </div>
                        <div class="mb-4">
                            <asp:Label ID="lblMarca" runat="server" Text="Marca" CssClass="card-text"></asp:Label>
                            <asp:TextBox ID="txtMarca" CssClass="form-control text-center text-bg-dark" Enabled="false" runat="server"></asp:TextBox>
                        </div>
                        <div class="mb-4">
                            <asp:Label ID="lblPrecio" runat="server" Text="Precio" CssClass="card-text"></asp:Label>
                            <asp:TextBox ID="txtPrecio" CssClass="form-control text-center text-bg-dark" Enabled="false" runat="server"></asp:TextBox>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
