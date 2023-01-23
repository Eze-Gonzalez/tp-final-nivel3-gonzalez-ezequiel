using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Datos;
using ModeloDominio;
using Helpers;
using System.Threading;
using System.Text.RegularExpressions;
using System.Configuration;

namespace CatalogoWeb
{
    public partial class Default : System.Web.UI.Page
    {
        private List<Producto> listaProducto;
        private int registrosPagina = 6;
        private int indicePaginaActual;
        private int indiceUltimaPagina;
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!Page.IsPostBack)
                {
                    DatosProducto datos = new DatosProducto();
                    listaProducto = datos.listar();
                    foreach (Producto producto in listaProducto)
                    {
                        producto.ImagenUrl = Helper.cargarImagen(producto);
                    }
                    indiceUltimaPagina = (listaProducto.Count / registrosPagina);
                    if (listaProducto.Count == 0 && registrosPagina == 0)
                        indiceUltimaPagina--;
                    ViewState["indicePaginaActual"] = indicePaginaActual;
                    ViewState["indiceUltimaPagina"] = indiceUltimaPagina;
                    Session["productos"] = listaProducto;
                    enlazarRepeater();
                }
                else
                {
                    indiceUltimaPagina = (int)ViewState["indiceUltimaPagina"];
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        private void enlazarRepeater()
        {
            try
            {
                List<Producto> listaProducto = (List<Producto>)Session["productos"];
                List<Producto> listaPagina = new List<Producto>();
                int inicio = indicePaginaActual * registrosPagina;
                int fin = (indicePaginaActual * registrosPagina) + registrosPagina;
                for (int i = inicio; i < fin; i++)
                {
                    if (i < listaProducto.Count)
                        listaPagina.Add(listaProducto[i]);
                    else
                        break;
                }
                repProductos.DataSource = listaPagina;
                repProductos.DataBind();

                indicePaginaActual = (int)ViewState["indicePaginaActual"];
                indiceUltimaPagina = (int)ViewState["indiceUltimaPagina"];
                txtPosicion.Text = String.Format("{0} de {1}", indicePaginaActual + 1, indiceUltimaPagina + 1);
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        protected void btnPrimero_Click(object sender, EventArgs e)
        {
            try
            {
                indicePaginaActual = 0;
                ViewState["indicePaginaActual"] = indicePaginaActual;
                enlazarRepeater();
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        protected void btnAnterior_Click(object sender, EventArgs e)
        {
            try
            {
                indicePaginaActual = (int)ViewState["indicePaginaActual"];
                if (indicePaginaActual > 0)
                {
                    indicePaginaActual--;
                    ViewState["indicePaginaActual"] = indicePaginaActual;
                    enlazarRepeater();
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        protected void btnSiguiente_Click(object sender, EventArgs e)
        {
            try
            {
                indicePaginaActual = (int)ViewState["indicePaginaActual"];
                indiceUltimaPagina = (int)ViewState["indiceUltimaPagina"];
                if (indicePaginaActual < indiceUltimaPagina)
                {
                    indicePaginaActual++;
                    ViewState["indicePaginaActual"] = indicePaginaActual;
                    enlazarRepeater();
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        protected void btnUltimo_Click(object sender, EventArgs e)
        {
            try
            {
                indiceUltimaPagina = (int)ViewState["indiceUltimaPagina"];
                indicePaginaActual = indiceUltimaPagina;
                ViewState["indicePaginaActual"] = indicePaginaActual;
                enlazarRepeater();
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        protected void btnDetalles_Click(object sender, EventArgs e)
        {
            try
            {
                Button btn = (Button)sender;
                RepeaterItem item = (RepeaterItem)btn.NamingContainer;
                Label lbl = (Label)item.FindControl("lblCodigo");
                int id = DatosProducto.traerId(lbl.Text);
                Response.Redirect("Details.aspx?id=" + id);
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        protected void btnBuscar_Click(object sender, EventArgs e)
        {
            DatosProducto datos = new DatosProducto();
            try
            {
                List<Producto> filtrada = datos.filtrarNombre(txtBuscar.Text);
                foreach (Producto producto in filtrada)
                {
                    producto.ImagenUrl = Helper.cargarImagen(producto);
                }
                indiceUltimaPagina = (filtrada.Count / registrosPagina);
                if (filtrada.Count == 0 && registrosPagina == 0)
                    indiceUltimaPagina--;
                ViewState["indicePaginaActual"] = indicePaginaActual;
                ViewState["indiceUltimaPagina"] = indiceUltimaPagina;
                Session["productos"] = filtrada;
                enlazarRepeater();
            }
            catch (Exception)
            {
                throw;
            }
        }

        protected void txtBuscar_TextChanged(object sender, EventArgs e)
        {
            DatosProducto datos = new DatosProducto();
            try
            {
                List<Producto> filtrada = datos.filtrarNombre(txtBuscar.Text);
                foreach (Producto producto in filtrada)
                {
                    producto.ImagenUrl = Helper.cargarImagen(producto);
                }
                indiceUltimaPagina = (filtrada.Count / registrosPagina);
                if (filtrada.Count == 0 && registrosPagina == 0)
                    indiceUltimaPagina--;
                ViewState["indicePaginaActual"] = indicePaginaActual;
                ViewState["indiceUltimaPagina"] = indiceUltimaPagina;
                Session["productos"] = filtrada;
                enlazarRepeater();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        protected void btnModal_Click(object sender, EventArgs e)
        {
            ajxModal.Show();
        }
    }
}