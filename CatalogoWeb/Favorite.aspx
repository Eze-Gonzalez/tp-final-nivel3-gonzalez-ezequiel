<%@ Page Title="" Language="C#" MasterPageFile="~/Catalogo.Master" AutoEventWireup="true" CodeBehind="Favorite.aspx.cs" Inherits="CatalogoWeb.Favorite" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:UpdatePanel ID="UpdatePanel2" runat="server">
        <ContentTemplate>
            <asp:Label ID="lblFavoritos" CssClass="text-light" runat="server" Text="Su lista de favoritos esta vacía, si desea comenzar a agregar, vea los detalles de algun producto y toque la estrella en la parte superior izquierda del panel de imagen."></asp:Label>
            <div class="row row-cols-1 row-cols-md-3 g-4">
                <asp:Repeater ID="repFav" runat="server">
                    <ItemTemplate>
                        <div class="card text-bg-dark me-2 mb-2 bg-black bg-opacity-50" style="max-width: 265px;">
                            <div class="card-body">
                                <asp:ImageButton ID="imgProd" runat="server" ImageUrl='<%#Eval("ImagenUrl") %>' AlternateText='<%#Eval("Id") %>' CssClass="card-img favorites" OnClick="imgProd_Click" />
                            </div>
                            <div class="card-footer center-col">
                                <p class="card-text"><small><%#Eval("Nombre") %></small></p>
                                <p class="card-text"><small>$<%#Eval("Precio") %></small></p>
                            </div>
                            <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                                <ContentTemplate>
                                    <div class="card-footer center-col">
                                        <asp:ImageButton ID="imgFav" runat="server" OnClick="imgFav_Click" ImageUrl="https://i.imgur.com/69Mns5z.png" CssClass="h-30" />
                                    </div>
                                </ContentTemplate>
                            </asp:UpdatePanel>
                        </div>
                    </ItemTemplate>
                </asp:Repeater>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
