using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ModeloDominio;
using Datos;

namespace CatalogoWeb
{
    public partial class ListaProductos : System.Web.UI.Page
    {
        public bool Avanzada { get; set; }
        protected void Page_Load(object sender, EventArgs e)
        {
            Title = "Listado de productos";
            if (!IsPostBack)
            {
                DatosProducto datos = new DatosProducto();
                Session.Add("productos", datos.listar());
                dgvProductos.DataSource = Session["productos"];
                dgvProductos.DataBind();
            }
        }

        protected void dgvProductos_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            dgvProductos.DataSource = Session["productos"];
            dgvProductos.PageIndex = e.NewPageIndex;
            dgvProductos.DataBind();
        }

        protected void dgvProductos_SelectedIndexChanged(object sender, EventArgs e)
        {
            int id = (int)dgvProductos.SelectedDataKey.Value;
            DatosProducto datos = new DatosProducto();
            Session.Add("producto", datos.traerProducto(id));
            Response.Redirect("Details.aspx?id=" + id);
        }

        protected void btnFiltro_Click(object sender, EventArgs e)
        {
            DatosProducto datos = new DatosProducto();
            dgvProductos.DataSource = datos.filtroRapido(txtFiltro.Text);
            dgvProductos.DataBind();
        }

        protected void txtFiltro_TextChanged(object sender, EventArgs e)
        {
            DatosProducto datos = new DatosProducto();
            dgvProductos.DataSource = datos.filtroRapido(txtFiltro.Text);
            dgvProductos.DataBind();
        }

        protected void btnAvanzada_Click(object sender, EventArgs e)
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

        protected void btnBuscar_Click(object sender, EventArgs e)
        {
            try
            {
                Avanzada = true;
                DatosProducto datos = new DatosProducto();
                Session.Add("productos", datos.filtroAvanzado(ddlTipo.SelectedItem.Text, ddlCriterio.SelectedItem.Text, txtBuscar.Text));
                dgvProductos.DataSource = Session["productos"];
                dgvProductos.DataBind();
            }
            catch (NullReferenceException)
            {
                DatosProducto datos = new DatosProducto();
                Session.Add("productos", datos.listar());
                dgvProductos.DataSource = Session["productos"];
                dgvProductos.DataBind();
            }
            catch(FormatException)
            {
                txtBuscar.Text = "Debe ingresar solo numeros sin separacion de mil.";
            }
            catch (Exception)
            {

                throw;
            }
        }

        protected void ddlTipo_SelectedIndexChanged(object sender, EventArgs e)
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
    }
}