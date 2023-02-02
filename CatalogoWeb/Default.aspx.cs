using Datos;
using Helpers;
using ModeloDominio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace CatalogoWeb
{
    public partial class Default : System.Web.UI.Page
    {
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
                    List<Producto> listaProducto = datos.listar();
                    Helper.cargarImgRep(listaProducto);
                    crearPaginacion(listaProducto);
                }
                else
                {
                    indiceUltimaPagina = (int)ViewState["indiceUltimaPagina"];
                }
            }
            catch (Exception ex)
            {
                Session.Add("ErrorCode", "Hubo un problema al cargar la página");
                Session.Add("Error", ex.Message);
                Response.Redirect("Error.aspx", false);
            }
            finally
            {
                script = string.Format("filtro('{0}','{1}');", titulo, mensaje);
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
                Session.Add("ErrorCode", "Hubo un problema al cargar la página");
                Session.Add("Error", ex.Message);
                Response.Redirect("Error.aspx", false);
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
                Session.Add("ErrorCode", "Hubo un problema al cargar la página");
                Session.Add("Error", ex.Message);
                Response.Redirect("Error.aspx", false);
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
                Session.Add("ErrorCode", "Hubo un problema al cargar la página");
                Session.Add("Error", ex.Message);
                Response.Redirect("Error.aspx", false);
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
                Session.Add("ErrorCode", "Hubo un problema al cargar la página");
                Session.Add("Error", ex.Message);
                Response.Redirect("Error.aspx", false);
            }
        }
        protected void btnDetalles_Click(object sender, EventArgs e)
        {
            try
            {
                RepeaterItem item = (RepeaterItem)((Button)sender).NamingContainer;
                Image img = (Image)item.FindControl("imgProducto");
                string id = img.AlternateText;
                Response.Redirect("Details.aspx?id=" + id);
            }
            catch (ThreadAbortException) { }
            catch (Exception ex)
            {
                Session.Add("ErrorCode", "Hubo un problema al cargar la página");
                Session.Add("Error", ex.Message);
                Response.Redirect("Error.aspx", false);
            }
        }

        protected void ddlFiltro_SelectedIndexChanged(object sender, EventArgs e)
        {
            DatosProducto datos = new DatosProducto();
            try
            {
                List<Producto> filtrada = datos.filtroRapido(ddlTipo.SelectedItem.Text, ref status, ddlFiltro.SelectedValue.ToString());
                if (!status)
                    ScriptManager.RegisterStartupScript(this, GetType(), "filtro", script, true);
                Helper.cargarImgRep(filtrada);
                indiceUltimaPagina = (filtrada.Count / registrosPagina);
                crearPaginacion(filtrada);
            }
            catch (Exception ex)
            {
                Session.Add("ErrorCode", "Hubo un problema al cargar la página");
                Session.Add("Error", ex.Message);
                Response.Redirect("Error.aspx", false);
            }
        }

        protected void ddlTipo_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                DatosProducto datosP = new DatosProducto();
                List<Producto> listaProductos = datosP.listar();
                crearPaginacion(listaProductos);
                switch (ddlTipo.SelectedItem.Text)
                {
                    case "Nombre":
                        {
                            panelRango.Visible = false;
                            panelLabel.Visible = false;
                            ddlFiltro.Visible = false;
                            txtFiltro.Visible = true;
                            btnBuscar.Visible = true;
                        }
                        break;
                    case "Categoría":
                        {
                            DatosCategoria datos = new DatosCategoria();
                            panelLabel.Visible = false;
                            panelRango.Visible = false;
                            btnBuscar.Visible = false;
                            txtFiltro.Visible = false;
                            ddlFiltro.Visible = true;
                            ddlFiltro.Items.Clear();
                            ddlFiltro.DataSource = datos.listar();
                            ddlFiltro.DataTextField = "Descripcion";
                            ddlFiltro.DataValueField = "Descripcion";
                            ddlFiltro.DataBind();
                            ddlFiltro_SelectedIndexChanged(sender, e);
                        }
                        break;
                    case "Marca":
                        {
                            DatosMarca datos = new DatosMarca();
                            panelLabel.Visible = false;
                            panelRango.Visible = false;
                            btnBuscar.Visible = false;
                            txtFiltro.Visible = false;
                            ddlFiltro.Visible = true;
                            ddlFiltro.Items.Clear();
                            ddlFiltro.DataSource = datos.listar();
                            ddlFiltro.DataTextField = "Descripcion";
                            ddlFiltro.DataValueField = "Descripcion";
                            ddlFiltro.DataBind();
                            ddlFiltro_SelectedIndexChanged(sender, e);
                        }
                        break;
                    case "Precio":
                        {
                            panelLabel.Visible = true;
                            panelRango.Visible = true;
                            ddlFiltro.Visible = false;
                            txtFiltro.Visible = false;
                            btnBuscar.Visible = false;
                        }
                        break;
                    default:
                        {
                            panelRango.Visible = false;
                            panelLabel.Visible = false;
                            ddlFiltro.Visible = false;
                            btnBuscar.Visible = false;
                            txtFiltro.Visible = false;
                        }
                        break;
                }
            }
            catch (Exception ex)
            {
                Session.Add("ErrorCode", "Hubo un problema al cargar la página");
                Session.Add("Error", ex.Message);
                Response.Redirect("Error.aspx", false);
            }
        }

        protected void txtFiltro_TextChanged(object sender, EventArgs e)
        {
            DatosProducto datos = new DatosProducto();
            try
            {
                List<Producto> filtrada = datos.filtroRapido(ddlTipo.SelectedItem.Text, ref status, txtFiltro.Text);
                if (!status)
                    ScriptManager.RegisterStartupScript(this, GetType(), "filtro", script, true);
                Helper.cargarImgRep(filtrada);
                crearPaginacion(filtrada);
            }
            catch (Exception ex)
            {
                Session.Add("ErrorCode", "Hubo un problema al cargar la página");
                Session.Add("Error", ex.Message);
                Response.Redirect("Error.aspx", false);
            }
        }

        protected void btnBuscar_Click(object sender, EventArgs e)
        {
            DatosProducto datos = new DatosProducto();
            try
            {
                List<Producto> filtrada = datos.filtroRapido(ddlTipo.SelectedItem.Text, ref status, txtFiltro.Text);
                if (!status)
                    ScriptManager.RegisterStartupScript(this, GetType(), "filtro", script, true);
                Helper.cargarImgRep(filtrada);
                crearPaginacion(filtrada);
            }
            catch (Exception ex)
            {
                Session.Add("ErrorCode", "Hubo un problema al cargar la página");
                Session.Add("Error", ex.Message);
                Response.Redirect("Error.aspx", false);
            }
        }

        protected void rango1_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                DatosProducto datos = new DatosProducto();
                string[] precios;
                string precioMin, precioMax;
                int min = 0, max = 0;
                if (rango1.Checked)
                {
                    precios = lblRango1.Text.Split(' ');
                    precioMin = precios[0];
                    precioMax = precios[2];
                    min = int.Parse(precioMin.Replace("$", ""));
                    max = int.Parse(precioMax.Replace("$", ""));
                }
                else if (rango2.Checked)
                {
                    precios = lblRango2.Text.Split(' ');
                    precioMin = precios[0];
                    precioMax = precios[2];
                    min = int.Parse(precioMin.Replace("$", ""));
                    max = int.Parse(precioMax.Replace("$", ""));
                }
                else if (rango3.Checked)
                {
                    precios = lblRango3.Text.Split(' ');
                    precioMin = precios[0];
                    min = int.Parse(precioMin.Replace("$", ""));
                }
                List<Producto> filtrada = datos.filtroRapido(ddlTipo.SelectedItem.Text, ref status, "", min, max);
                if (!status)
                    ScriptManager.RegisterStartupScript(this, GetType(), "filtro", script, true);
                Helper.cargarImgRep(filtrada);
                crearPaginacion(filtrada);
            }
            catch (Exception ex)
            {
                Session.Add("ErrorCode", "Hubo un problema al cargar la página");
                Session.Add("Error", ex.Message);
                Response.Redirect("Error.aspx", false);
            }
        }
        private void crearPaginacion(List<Producto> lista)
        {
            try
            {
                indiceUltimaPagina = (lista.Count / registrosPagina);
                if (lista.Count <= 6)
                {
                    if (indiceUltimaPagina > 0)
                        indiceUltimaPagina--;
                }

                ViewState["indicePaginaActual"] = indicePaginaActual;
                ViewState["indiceUltimaPagina"] = indiceUltimaPagina;
                Session.Add("productos", lista);
                enlazarRepeater();
            }
            catch (Exception ex)
            {
                Session.Add("ErrorCode", "Hubo un problema al cargar la página");
                Session.Add("Error", ex.Message);
                Response.Redirect("Error.aspx", false);
            }
        }
        private void enlazarRepeater()
        {
            try
            {
                List<Producto> listaProducto = (List<Producto>)Session["productos"];
                Helper.cargarImgRep(listaProducto);
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
                txtPosicion.Text = (indicePaginaActual + 1) + " de " + (indiceUltimaPagina + 1);
                habilitarPaginacion(txtPosicion.Text);
            }
            catch (Exception ex)
            {
                Session.Add("ErrorCode", "Hubo un problema al cargar la página");
                Session.Add("Error", ex.Message);
                Response.Redirect("Error.aspx", false);
            }
        }
        private void habilitarPaginacion(string posicion)
        {
            if (posicion == "1 de 1")
            {
                txtPosicion.Visible = false;
                btnPrimero.Visible = false;
                btnAnterior.Visible = false;
                btnSiguiente.Visible = false;
                btnUltimo.Visible = false;
            }
            else
            {
                txtPosicion.Visible = true;
                btnPrimero.Visible = true;
                btnAnterior.Visible = true;
                btnSiguiente.Visible = true;
                btnUltimo.Visible = true;
            }
        }
    }

}