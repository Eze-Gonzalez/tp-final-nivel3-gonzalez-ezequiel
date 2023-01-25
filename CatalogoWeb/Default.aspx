<%@ Page Title="" Language="C#" MasterPageFile="~/Catalogo.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="CatalogoWeb.Default" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server"></asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="row mb-4 center-row">
        <div class="col-4">
            <asp:TextBox ID="txtBuscar" CssClass="form-control" OnTextChanged="txtBuscar_TextChanged" AutoPostBack="true" runat="server"></asp:TextBox>
        </div>
        <div class="col-4">
            <asp:Button ID="btnBuscar" CssClass="btn btn-outline btn-primary w-120" runat="server" Text="Buscar" OnClick="btnBuscar_Click" />
        </div>
    </div>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <div class="row row-cols-1 row-cols-md-3 g-4">
                <asp:Repeater runat="server" ID="repProductos">
                    <ItemTemplate>
                        <div class="col">
                            <div class="card mb-3 products bg-black bg-opacity-50 text-bg-dark">
                                <div class="row g-0 products-size">
                                    <div class="col-md-7 center-col-justify">
                                        <asp:Image ID="imgProducto" ImageUrl='<%#Eval("imagenUrl") %>' AlternateText='<%#Eval("Id") %>' CssClass="img" runat="server" />
                                    </div>
                                    <div class="col">
                                        <div class="card-body">
                                            <h5 class="card-title"><%#Eval("Nombre") %></h5>
                                            <asp:Label CssClass="card-text" ID="lblCategoria" runat="server" Text='<%#Eval("Categoria") %>'></asp:Label>
                                            <asp:Label CssClass="card-text" ID="lblMarca" runat="server" Text='<%#Eval("Marca") %>'></asp:Label>
                                            <p class="card-text">$<%#Eval("Precio") %></p>
                                        </div>
                                    </div>
                                </div>
                                <div class="card-footer text-muted center-col">
                                    <asp:Button ID="btnDetalles" runat="server" Text="Ver detalles" CssClass="btn btn-outline-light btn-primary w-120 accordion-body" OnClick="btnDetalles_Click" />
                                </div>
                            </div>
                        </div>
                    </ItemTemplate>
                </asp:Repeater>
            </div>
        </ContentTemplate>
        <Triggers>
            <asp:AsyncPostBackTrigger ControlID="btnPrimero" EventName="Click" />
            <asp:AsyncPostBackTrigger ControlID="btnAnterior" EventName="Click" />
            <asp:AsyncPostBackTrigger ControlID="btnSiguiente" EventName="Click" />
            <asp:AsyncPostBackTrigger ControlID="btnUltimo" EventName="Click" />
        </Triggers>
    </asp:UpdatePanel>
    <nav class="center-row">
        <ul class="pagination" style="text-align: center">
            <li class="page-item">
                <asp:Button ID="btnPrimero" Text="<<" Width="50" CssClass="page-link bg-black bg-opacity-25 border-primary text-bg-danger" runat="server" OnClick="btnPrimero_Click" /></li>
            <li class="page-item">
                <asp:Button ID="btnAnterior" Text="<" Width="50" CssClass="page-link bg-black bg-opacity-25 border-primary text-bg-danger" runat="server" OnClick="btnAnterior_Click" /></li>
            <li class="page-item">
                <asp:TextBox ID="txtPosicion" Width="100" ReadOnly="true" CssClass="page-link text-center bg-black bg-opacity-25 border-primary text-bg-danger" runat="server" /></li>
            <li class="page-item">
                <asp:Button ID="btnSiguiente" Text=">" Width="50" CssClass="page-link bg-black bg-opacity-25 border-primary text-bg-danger" runat="server" OnClick="btnSiguiente_Click" /></li>
            <li class="page-item">
                <asp:Button ID="btnUltimo" Text=">>" Width="50" CssClass="page-link bg-black bg-opacity-25 border-primary text-bg-danger" runat="server" OnClick="btnUltimo_Click" /></li>
        </ul>
    </nav>
</asp:Content>
