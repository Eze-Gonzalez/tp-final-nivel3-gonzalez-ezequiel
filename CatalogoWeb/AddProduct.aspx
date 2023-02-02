<%@ Page Title="" Language="C#" MasterPageFile="~/Catalogo.Master" AutoEventWireup="true" CodeBehind="AddProduct.aspx.cs" Inherits="CatalogoWeb.AddProduct" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="row text-light mb-5">
        <div class="col center-row">
            <h3>Agregar un nuevo producto</h3>
        </div>
    </div>
    <div class="row text-light">
        <div class="col-6">
            <div class="mb-3">
                <asp:Label ID="lblCodigo" CssClass="form-label" runat="server" Text="Código"></asp:Label>
                <asp:TextBox ID="txtCodigo" CssClass="form-control" runat="server"></asp:TextBox>
                <div class="row">
                    <asp:Label ID="lblErrorCodigo" CssClass="danger" Visible="false" runat="server" Text="Label"></asp:Label>
                </div>
                <asp:RequiredFieldValidator ErrorMessage="Debe ingresar un código para el producto" CssClass="danger" ControlToValidate="txtCodigo" runat="server" />
            </div>
            <div class="mb-3">
                <asp:Label ID="lblNombre" CssClass="form-label" runat="server" Text="Nombre"></asp:Label>
                <asp:TextBox ID="txtNombre" CssClass="form-control" runat="server"></asp:TextBox>
                <div class="row">
                    <asp:Label ID="lblErrorNombre" CssClass="danger" Visible="false" runat="server" Text="Label"></asp:Label>
                </div>
                <asp:RequiredFieldValidator ErrorMessage="Debe ingresar un nombre para el producto" CssClass="danger" ControlToValidate="txtNombre" runat="server" />
            </div>
            <div class="mb-3">
                <asp:Label ID="lblDescripcion" CssClass="form-label" runat="server" Text="Descripción"></asp:Label>
                <asp:TextBox ID="txtDescripcion" CssClass="form-control" TextMode="MultiLine" runat="server"></asp:TextBox>
                <asp:RequiredFieldValidator ErrorMessage="Debe ingresar una descripción para el producto" CssClass="danger" ControlToValidate="txtDescripcion" runat="server" />
            </div>
            <div class="mb-3">
                <asp:Label ID="lblCategoria" CssClass="form-label" runat="server" Text="Categoría"></asp:Label>
                <asp:DropDownList ID="ddlCategoria" runat="server" CssClass="form-select"></asp:DropDownList>
            </div>
            <div class="mb-3">
                <asp:Label ID="lblMarca" CssClass="form-label" runat="server" Text="Marca"></asp:Label>
                <asp:DropDownList ID="ddlMarca" runat="server" CssClass="form-select"></asp:DropDownList>
            </div>
            <div class="mb-5">
                <asp:Label ID="lblPrecio" CssClass="form-label" runat="server" Text="Precio"></asp:Label>
                <asp:TextBox ID="txtPrecio" CssClass="form-control" runat="server"></asp:TextBox>
                <div class="row">
                    <asp:Label ID="lblErrorPrecio" CssClass="danger" Visible="false" runat="server" Text="Label"></asp:Label>
                </div>
                <asp:RequiredFieldValidator ErrorMessage="Debe ingresar un precio para el producto " CssClass="danger" ControlToValidate="txtPrecio" runat="server" />
            </div>
        </div>
        <div class="col-6">
            <div class="mb-3">
                <asp:Label ID="lblImagen" CssClass="form-label" runat="server" Text="Seleccione una opción para cargar la imagen"></asp:Label>
            </div>
            <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                <ContentTemplate>
                    <div class="mb-2 center-row">
                        <div class="divToggleButton me-2 align-center">
                            <asp:RadioButton ID="rdbUrl" runat="server" AutoPostBack="true" GroupName="Eleccion" />
                            <asp:Label ID="lblToggleButton"
                                AssociatedControlID="rdbUrl" runat="server"
                                ToolTip="Cambiar imagen mediante un enlace de internet" />
                        </div>
                        <asp:Label ID="lblUrl" runat="server" Text="Mediante URL (enlace de internet)" CssClass="me-2"></asp:Label>
                        <div class="divToggleButton me-2 align-center">
                            <asp:RadioButton ID="rdbLocal" runat="server" AutoPostBack="true" GroupName="Eleccion" />
                            <asp:Label ID="lblToggleButtonLocal"
                                AssociatedControlID="rdbLocal" runat="server"
                                ToolTip="Cambiar imagen desde su dispositivo" />
                        </div>
                        <asp:Label ID="lblLocal" runat="server" Text="Subir una imagen desde su dispositivo"></asp:Label>
                    </div>
                    <div class="mb-3">
                        <%if (rdbLocal.Checked)
                            { %>
                        <input class="form-control" type="file" id="txtImagenLocal" runat="server" onchange="ValidateSize(this)">
                        <%}
                            else if (rdbUrl.Checked)
                            { %>
                        <asp:TextBox ID="txtImagenUrl" CssClass="form-control" placeholder="Ingrese una url de imagen" runat="server" OnTextChanged="txtImagenUrl_TextChanged" AutoPostBack="true"></asp:TextBox>
                        <%}  %>
                    </div>
                    <div class="mb-5 center-col">
                        <asp:Image ID="imgProducto" CssClass="img-fluid img-add" ImageUrl="https://i.imgur.com/8I5eyGa.jpg" runat="server" Style="" />
                    </div>
                </ContentTemplate>
            </asp:UpdatePanel>
        </div>
    </div>
    <div class="row">
        <div class="col-6 flex-end">
            <asp:Button ID="btnAgregar" runat="server" Text="Agregar" CssClass="w-120 btn btn-outline-light btn-primary" OnClick="btnAgregar_Click" />
        </div>
        <div class="col-6">
            <a href="ProductList.aspx" class="w-120 btn btn-outline-light btn-danger">Cancelar</a>
        </div>
    </div>
</asp:Content>
