using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Datos;
using ModeloDominio;
using Validaciones;
using Helpers;

namespace CatalogoWeb
{
    public partial class Details : System.Web.UI.Page
    {
        public bool Eliminar { get; set; }
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (Validar.admin(Session["usuario"]))
                    btnRegresar.Text = "Ver listado";
                else
                    btnRegresar.Text = "Volver al inicio";
                DatosProducto datos = new DatosProducto();
                Producto producto = datos.traerProducto(int.Parse(Request.QueryString["id"]));
                Title = "Detalles de " + producto.Nombre;
                if (!IsPostBack)
                {
                    Eliminar = false;
                    imgProducto.ImageUrl = Helper.cargarImagen(producto);
                    txtId.Text = producto.Id.ToString();
                    txtCodigo.Text = producto.Codigo;
                    txtNombre.Text = producto.Nombre;
                    txtDescripcion.Text = producto.Descripcion;
                    txtCategoria.Text = producto.Categoria.Descripcion;
                    txtMarca.Text = producto.Marca.Descripcion;
                    txtPrecio.Text = producto.Precio.ToString();
                    Session.Add("id", producto.Id);
                }
                if (Validar.sesion(Session["usuario"]))
                {
                    int idUser = ((Usuario)Session["usuario"]).Id;
                    if (Validar.favExistente(producto.Id, idUser))
                        imgFav.ImageUrl = "https://i.imgur.com/69Mns5z.png";
                    else
                        imgFav.ImageUrl = "https://i.imgur.com/w9bX4Nr.png";
                }
            }
            catch (ArgumentNullException)
            {
                Session.Add("ErrorCode", "No se seleccionó ningun producto.");
                Session.Add("Error", "No se encontró ningun artículo. Para mostrar los detalles de un artículo, por favor seleccione un artículo y presione en el boton 'Ver detalles'");
                Response.Redirect("Error.aspx");
            }
            catch (Exception)
            {

                throw;
            }
        }
        protected void btnModificar_Click(object sender, EventArgs e)
        {
            Response.Redirect("Product.aspx?id=" + Session["id"], false);
        }
        protected void btnEliminar_Click(object sender, EventArgs e)
        {
            Producto producto = ((List<Producto>)Session["productos"]).Find(p => p.Id == int.Parse(Request.QueryString["Id"]));
            Eliminar = true;
            lblConfirmación.Text = "¿Está seguro que desea eliminar el producto " + producto.Nombre + "?. Ésta acción no se puede revertir.";
        }
        protected void btnConfirmar_Click(object sender, EventArgs e)
        {
            try
            {
                DatosProducto.eliminar(int.Parse(Request.QueryString["id"]));
                Response.Redirect("ListaProductos.aspx", false);
            }
            catch (Exception)
            {

                throw;
            }
        }
        protected void btnCancelar_Click(object sender, EventArgs e)
        {
            Eliminar = false;
        }

        protected void btnRegresar_Click(object sender, EventArgs e)
        {
            if (Validar.admin(Session["usuario"]))
                Response.Redirect("ListaProductos.aspx");
            else
                Response.Redirect("Default.aspx");
            //string page = Request.QueryString["page"] != null ? Request.QueryString["page"] : "";
            //switch (page)
            //{
            //    case "fav":
            //        Response.Redirect("Favorite.aspx");
            //        break;
            //    case "lp":
            //        Response.Redirect("ListaProductos.aspx");
            //        break;
            //    default:
            //        Response.Redirect("Default.aspx");
            //        break;
            //}
        }

        protected void imgFav_Click(object sender, ImageClickEventArgs e)
        {
            try
            {
                {
                    int idProd = Session["id"] != null ? (int)Session["id"] : 0;
                    int idUser = Session["usuario"] != null ? ((Usuario)Session["usuario"]).Id : 0;
                    if (Validar.favExistente(idProd, idUser))
                    {
                        DatosFavorito.eliminarFav(idProd, idUser);
                        imgFav.ImageUrl = "https://i.imgur.com/w9bX4Nr.png";

                    }
                    else
                    {
                        Favorito fav = new Favorito();
                        fav.Producto = new Producto();
                        fav.Usuario = new Usuario();
                        fav.Producto.Id = idProd;
                        fav.Usuario.Id = idUser;
                        DatosFavorito.agregarFav(fav);
                        imgFav.ImageUrl = "https://i.imgur.com/Bem812K.gif";

                    }
                }
            }
            catch (Exception)
            {

                throw;
            }
        }

        protected void Button1_Click(object sender, EventArgs e)
        {

        }
    }
}