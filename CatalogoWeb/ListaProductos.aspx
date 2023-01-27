<%@ Page Title="" Language="C#" MasterPageFile="~/Catalogo.Master" AutoEventWireup="true" CodeBehind="ListaProductos.aspx.cs" Inherits="CatalogoWeb.ListaProductos" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <div class="row mb-5 mt-5">
                <%if (!Avanzada)
                    { %>
                <div class="col-4 mb-4">
                    <asp:TextBox ID="txtFiltro" runat="server" CssClass="form-control" OnTextChanged="txtFiltro_TextChanged" AutoPostBack="true"></asp:TextBox>
                </div>
                <div class="col-4 mb-4">
                    <asp:Button ID="btnFiltro" runat="server" Text="Buscar" CssClass="btn btn-outline-light btn-primary w-120" OnClick="btnFiltro_Click" />
                </div>
                <%}%>
                <div class="col flex-end mb-4">
                    <asp:Button ID="btnAvanzada" runat="server" Text="Búsqueda avanzada" CssClass="btn btn-outline-light btn-primary w-170" OnClick="btnAvanzada_Click" />
                </div>
            </div>
            <%if (Avanzada)
                { %>
            <div class="row text-light mt-4">
                <div class="col-auto mb-4 center-col-justify">
                    <label for="exampleFormControlInput1" class="form-label h4">Tipo</label>
                </div>
                <div class="col mb-4">
                    <asp:DropDownList ID="ddlTipo" runat="server" CssClass="form-select"
                        OnSelectedIndexChanged="ddlTipo_SelectedIndexChanged" AutoPostBack="true">
                        <asp:ListItem Text="Seleccione un tipo" />
                        <asp:ListItem Text="Precio" />
                        <asp:ListItem Text="Codigo" />
                        <asp:ListItem Text="Nombre" />
                        <asp:ListItem Text="Categoría" />
                        <asp:ListItem Text="Marca" />
                    </asp:DropDownList>
                </div>
                <div class="col-auto mb-4 center-col-justify">
                    <label for="exampleFormControlInput1" class="form-label h4">Criterio</label>
                </div>
                <div class="col mb-4">
                    <asp:DropDownList ID="ddlCriterio" runat="server" CssClass="form-select"></asp:DropDownList>
                </div>
                <div class="col-auto mb-4 center-col-justify">
                    <label for="exampleFormControlInput1" class="form-label h4">Filtro</label>
                </div>
                <div class="col mb-4">
                    <asp:TextBox ID="txtBuscar" CssClass="form-control" runat="server"></asp:TextBox>
                </div>
                <div class="col mb-4 flex-end">
                    <asp:Button ID="btnBuscar" runat="server" Text="Buscar" CssClass="btn btn-outline-light btn-primary w-120" OnClick="btnBuscar_Click" />
                </div>
            </div>
            <%}  %>
            <div class="row">
                <div class="col">
                    <asp:GridView ID="dgvProductos"
                        CssClass="table bg-black bg-opacity-10 text-bg-danger text-center table-bordered"
                        AutoGenerateColumns="false" AllowPaging="true" PageSize="6"
                        OnPageIndexChanging="dgvProductos_PageIndexChanging"
                        OnSelectedIndexChanged="dgvProductos_SelectedIndexChanged"
                        DataKeyNames="Id" runat="server" HeaderStyle-BackColor="Black" Font-Size="Larger">
                        <Columns>
                            <asp:BoundField HeaderText="Código" DataField="Codigo" />
                            <asp:BoundField HeaderText="Nombre" DataField="Nombre" />
                            <asp:BoundField HeaderText="Categoría" DataField="Categoria.Descripcion" />
                            <asp:BoundField HeaderText="Marca" DataField="Marca.Descripcion" />
                            <asp:BoundField HeaderText="Precio" DataField="Precio" />
                            <asp:CommandField HeaderText="Detalles" ShowSelectButton="true" SelectText="🔎" ControlStyle-CssClass="text-decoration-none" />
                        </Columns>
                    </asp:GridView>
                </div>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
    <div class="row">
        <div class="col center-row">
            <a href="Product.aspx" class="btn btn-outline-light btn-primary w-120">Agregar</a>
        </div>
    </div>
</asp:Content>
