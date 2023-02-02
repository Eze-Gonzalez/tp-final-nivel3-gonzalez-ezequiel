using Datos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace CatalogoWeb
{
    public partial class ProductList : System.Web.UI.Page
    {
        public bool Avanzada { get; set; }
        private bool status = true;
        private string script;
        private string titulo = "No se encontraron coincidencias";
        private string mensaje = "No se encontro ningun artículo que coincida con el filtro deseado.";
        protected void Page_Load(object sender, EventArgs e)
        {
            Title = "Listado de productos";
            try
            {
                if (!IsPostBack)
                {
                    DatosProducto datos = new DatosProducto();
                    Session.Add("productos", datos.listar());
                    dgvProductos.DataSource = Session["productos"];
                    dgvProductos.DataBind();
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
                script = string.Format("filtro('{0}', '{1}');", titulo, mensaje);
            }
        }

        protected void dgvProductos_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            try
            {
                dgvProductos.DataSource = Session["productos"];
                dgvProductos.PageIndex = e.NewPageIndex;
                dgvProductos.DataBind();
            }
            catch (Exception ex)
            {
                Session.Add("ErrorCode", "Hubo un problema al cargar la página");
                Session.Add("Error", ex.Message);
                Response.Redirect("Error.aspx", false);
            }
        }

        protected void dgvProductos_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                int id = (int)dgvProductos.SelectedDataKey.Value;
                DatosProducto datos = new DatosProducto();
                Session.Add("producto", datos.traerProducto(id));
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

        protected void btnFiltro_Click(object sender, EventArgs e)
        {
            try
            {
                DatosProducto datos = new DatosProducto();
                dgvProductos.DataSource = datos.filtroRapido(txtFiltro.Text, ref status);
                if (!status)
                    ScriptManager.RegisterStartupScript(this, GetType(), "filtro", script, true);
                dgvProductos.DataBind();
            }
            catch (Exception ex)
            {
                Session.Add("ErrorCode", "Hubo un problema al cargar la página");
                Session.Add("Error", ex.Message);
                Response.Redirect("Error.aspx");
            }
        }

        protected void txtFiltro_TextChanged(object sender, EventArgs e)
        {
            try
            {
                DatosProducto datos = new DatosProducto();
                dgvProductos.DataSource = datos.filtroRapido(txtFiltro.Text, ref status);
                if (!status)
                    ScriptManager.RegisterStartupScript(this, GetType(), "filtro", script, true);
                dgvProductos.DataBind();
            }
            catch (Exception ex)
            {
                Session.Add("ErrorCode", "Hubo un problema al cargar la página");
                Session.Add("Error", ex.Message);
                Response.Redirect("Error.aspx");
            }
        }

        protected void btnAvanzada_Click(object sender, EventArgs e)
        {
            try
            {
                DatosProducto datos = new DatosProducto();
                if (btnAvanzada.Text == "Búsqueda avanzada")
                {
                    btnAvanzada.Text = "Mostrar todo";
                    Avanzada = true;
                }
                else
                {
                    btnAvanzada.Text = "Búsqueda avanzada";
                    Session.Add("productos", datos.listar());
                    dgvProductos.DataSource = Session["productos"];
                    dgvProductos.DataBind();
                    ddlTipo.ClearSelection();
                    ddlCriterio.Items.Clear();
                    Avanzada = false;
                }
            }
            catch (Exception ex)
            {
                Session.Add("ErrorCode", "Hubo un problema al cargar la página");
                Session.Add("Error", ex.Message);
                Response.Redirect("Error.aspx");
            }
        }

        protected void btnBuscar_Click(object sender, EventArgs e)
        {
            try
            {
                Avanzada = true;
                DatosProducto datos = new DatosProducto();
                Session.Add("productos", datos.filtroAvanzado(ddlTipo.SelectedItem.Text, ddlCriterio.SelectedItem.Text, txtBuscar.Text, ref status));
                if (!status)
                    ScriptManager.RegisterStartupScript(this, GetType(), "filtro", script, true);
                dgvProductos.DataSource = Session["productos"];
                dgvProductos.DataBind();
            }
            catch (NullReferenceException)
            {
                DatosProducto datos = new DatosProducto();
                Session.Add("productos", datos.listar());
                if (!status)
                    ScriptManager.RegisterStartupScript(this, GetType(), "filtro", script, true);
                dgvProductos.DataSource = Session["productos"];
                dgvProductos.DataBind();
            }
            catch (FormatException)
            {
                txtBuscar.Text = "Debe ingresar solo numeros sin separacion de mil.";
            }
            catch (Exception ex)
            {
                Session.Add("ErrorCode", "Hubo un problema al cargar la página");
                Session.Add("Error", ex.Message);
                Response.Redirect("Error.aspx");
            }
        }

        protected void ddlTipo_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                Avanzada = true;
                if (ddlTipo.SelectedItem.Text == "Precio")
                {
                    ddlCriterio.Items.Clear();
                    ddlCriterio.Items.Add("Igual a");
                    ddlCriterio.Items.Add("Menor a");
                    ddlCriterio.Items.Add("Mayor a");
                }
                else
                {
                    ddlCriterio.Items.Clear();
                    ddlCriterio.Items.Add("Comienza con");
                    ddlCriterio.Items.Add("Termina con");
                    ddlCriterio.Items.Add("Contiene");
                }
            }
            catch (Exception ex)
            {
                Session.Add("ErrorCode", "Hubo un problema al cargar la página");
                Session.Add("Error", ex.Message);
                Response.Redirect("Error.aspx");
            }
        }
    }
}