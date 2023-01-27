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
        private bool status = true;
        private string mensaje = "No se encontró ningún artículo que corresponda con el filtro deseado.";
        private string titulo = "No se encontraron coincidencias";
        private string script;
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
                    if (listaProducto.Count <= 6)
                    {
                        if (indiceUltimaPagina > 0)
                            indiceUltimaPagina--;
                    }

                    ViewState["indicePaginaActual"] = indicePaginaActual;
                    ViewState["indiceUltimaPagina"] = indiceUltimaPagina;
                    Session.Add("productos", listaProducto);
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
                Image img = (Image)item.FindControl("imgProducto");
                int id = int.Parse(img.AlternateText);
                Response.Redirect("Details.aspx?id=" + id);
            }
            catch (ThreadAbortException) { }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        protected void ddlFiltro_SelectedIndexChanged(object sender, EventArgs e)
        {
            DatosProducto datos = new DatosProducto();
            try
            {
                List<Producto> filtrada = datos.filtroRapido(ddlTipo.SelectedItem.Text, ddlFiltro.SelectedValue.ToString(), ref status);
                if (!status)
                {
                    script = string.Format("filtro('{0}', '{1}');", titulo, mensaje);
                    ScriptManager.RegisterStartupScript(this, GetType(), "filtro", script, true);
                }
                foreach (Producto producto in filtrada)
                {
                    producto.ImagenUrl = Helper.cargarImagen(producto);
                }
                indiceUltimaPagina = (filtrada.Count / registrosPagina);
                if (filtrada.Count == 0)
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

        protected void ddlTipo_SelectedIndexChanged(object sender, EventArgs e)
        {
            string seleccion = ddlTipo.SelectedItem.Text;
            try
            {
                switch (seleccion)
                {
                    case "Nombre":
                        ddlFiltro.Visible = false;
                        txtFiltro.Visible = true;
                        btnBuscar.Visible = true;
                        break;
                    case "Categoría":
                        {
                            DatosCategoria datos = new DatosCategoria();
                            btnBuscar.Visible = false;
                            txtFiltro.Visible = false;
                            ddlFiltro.Visible = true;
                            ddlFiltro.Items.Clear();
                            ddlFiltro.DataSource = datos.listar();
                            ddlFiltro.DataTextField = "Descripcion";
                            ddlFiltro.DataValueField = "Descripcion";
                            ddlFiltro.DataBind();
                        }
                        break;
                    case "Marca":
                        {
                            DatosMarca datos = new DatosMarca();
                            btnBuscar.Visible = false;
                            txtFiltro.Visible = false;
                            ddlFiltro.Visible = true;
                            ddlFiltro.Items.Clear();
                            ddlFiltro.DataSource = datos.listar();
                            ddlFiltro.DataTextField = "Descripcion";
                            ddlFiltro.DataValueField = "Descripcion";
                            ddlFiltro.DataBind();
                        }
                        break;
                    case "Precio":
                        {
                            ddlFiltro.Visible = false;
                            txtFiltro.Visible = true;
                            btnBuscar.Visible = true;
                        }
                        break;
                    default:
                        {
                            ddlFiltro.Visible = false;
                            btnBuscar.Visible = false;
                            txtFiltro.Visible = false;
                        }
                        break;
                }
            }
            catch (Exception)
            {

                throw;
            }
        }

        protected void txtFiltro_TextChanged(object sender, EventArgs e)
        {
            DatosProducto datos = new DatosProducto();
            try
            {
                List<Producto> filtrada = datos.filtroRapido(ddlTipo.SelectedItem.Text, txtFiltro.Text, ref status);
                if (!status)
                {
                    script = string.Format("filtro('{0}', '{1}');", titulo, mensaje);
                    ScriptManager.RegisterStartupScript(this, GetType(), "filtro", script, true);
                }
                foreach (Producto producto in filtrada)
                {
                    producto.ImagenUrl = Helper.cargarImagen(producto);
                }
                indiceUltimaPagina = (filtrada.Count / registrosPagina);
                if (filtrada.Count == 0)
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

        protected void btnBuscar_Click(object sender, EventArgs e)
        {
            DatosProducto datos = new DatosProducto();
            try
            {
                List<Producto> filtrada = datos.filtroRapido(ddlTipo.SelectedItem.Text, txtFiltro.Text, ref status);
                if (!status)
                {
                    script = string.Format("filtro('{0}', '{1}');", titulo, mensaje);
                    ScriptManager.RegisterStartupScript(this, GetType(), "filtro", script, true);
                }
                foreach (Producto producto in filtrada)
                {
                    producto.ImagenUrl = Helper.cargarImagen(producto);
                }
                indiceUltimaPagina = (filtrada.Count / registrosPagina);
                if (filtrada.Count == 0)
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
    }
}