<%@ Page Title="" Language="C#" MasterPageFile="~/Catalogo.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="CatalogoWeb.Default" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:UpdatePanel ID="UpdatePanel2" runat="server">
        <ContentTemplate>
            <div class="row mb-4 center-row text-light">
                <div class="col-2 center-col">
                    <label class="h3">Filtrar</label>
                </div>
                <div class="col-3">
                    <asp:DropDownList ID="ddlTipo" runat="server" CssClass="form-select"
                        OnSelectedIndexChanged="ddlTipo_SelectedIndexChanged" AutoPostBack="true">
                        <asp:ListItem Text="Seleccione un tipo" />
                        <asp:ListItem Text="Nombre" />
                        <asp:ListItem Text="Categoría" />
                        <asp:ListItem Text="Marca" />
                        <asp:ListItem Text="Precio" />
                    </asp:DropDownList>
                </div>
                <div class="col-3 center-col">
                    <asp:DropDownList ID="ddlFiltro" CssClass="form-select" runat="server" OnSelectedIndexChanged="ddlFiltro_SelectedIndexChanged" AutoPostBack="true" Visible="false">
                    </asp:DropDownList>
                    <asp:TextBox ID="txtFiltro" runat="server" OnTextChanged="txtFiltro_TextChanged" AutoPostBack="true" CssClass="form-control" Visible="false"></asp:TextBox>
                    <%-- Paneles --%>
                    <div class="center-row column-gap-2">
                        <asp:Panel ID="panelRango" Visible="false" runat="server" CssClass="center-col">

                            <div class="divToggleButton2 mb-1 align-center">
                                <asp:RadioButton ID="rango1" OnCheckedChanged="rango1_CheckedChanged" runat="server" AutoPostBack="true" GroupName="Eleccion" />
                                <asp:Label ID="Label5"
                                    AssociatedControlID="rango1" runat="server"
                                    ToolTip="Seleccione un rango" />
                            </div>
                            <div class="divToggleButton2 mb-1 align-center">
                                <asp:RadioButton ID="rango2" OnCheckedChanged="rango1_CheckedChanged" runat="server" AutoPostBack="true" GroupName="Eleccion" />
                                <asp:Label ID="Label4"
                                    AssociatedControlID="rango2" runat="server"
                                    ToolTip="Seleccione un rango" />
                            </div>
                            <div class="divToggleButton2 mb-1 align-center">
                                <asp:RadioButton ID="rango3" OnCheckedChanged="rango1_CheckedChanged" runat="server" AutoPostBack="true" GroupName="Eleccion" />
                                <asp:Label ID="Label3"
                                    AssociatedControlID="rango3" runat="server"
                                    ToolTip="Seleccione un rango" />
                            </div>
                        </asp:Panel>
                        <asp:Panel ID="panelLabel" runat="server" CssClass="wrap-start" Visible="false">
                            <asp:Label ID="lblRango1" runat="server" Text="$0 - $10000" CssClass="mb-1"></asp:Label>
                            <asp:Label ID="lblRango2" runat="server" Text="$10000 - $100000" CssClass="mb-1"></asp:Label>
                            <asp:Label ID="lblRango3" runat="server" Text="$100000 - $1000000" CssClass="mb-1"></asp:Label>
                        </asp:Panel>
                    </div>
                </div>
                <div class="col-3 center-col">
                    <asp:Button ID="btnBuscar" runat="server" Text="Buscar" CssClass="btn btn-outline-light btn-primary w-120" OnClick="btnBuscar_Click" Visible="false" />
                </div>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <div class="row row-cols-1 row-cols-md-3 g-4">
                <asp:Repeater runat="server" ID="repProductos">
                    <ItemTemplate>
                        <div class="col">
                            <div class="card mb-3 products bg-black bg-opacity-50 text-bg-dark" style="height: 420px;">
                                <div class="row g-0 products-size">
                                    <div class="col-md-7 center-col-justify">
                                        <asp:Image ID="imgProducto" ImageUrl='<%#Eval("imagenUrl") %>' AlternateText='<%#Eval("Id") %>'
                                            CssClass="img img-fluid" runat="server" />
                                    </div>
                                    <div class="col">
                                        <div class="row">
                                            <div class="card-body">
                                                <h5 class="card-title"><%#Eval("Nombre") %></h5>
                                                <asp:Label CssClass="card-text" ID="lblCategoria" runat="server" Text='<%#Eval("Categoria") %>'></asp:Label>
                                                <div>
                                                    <asp:Label CssClass="card-text" ID="lblMarca" runat="server" Text='<%#Eval("Marca") %>'></asp:Label>
                                                </div>
                                                <p class="card-text">$<%#Eval("Precio") %></p>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <div class="card-footer text-muted center-col position-relative" style="bottom: -30px;">
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
