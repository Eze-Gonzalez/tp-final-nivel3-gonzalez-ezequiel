using Datos;
using Helpers;
using ModeloDominio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Validaciones;

namespace CatalogoWeb
{
    public partial class AddProduct : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!IsPostBack)
                {
                    DatosCategoria categoria = new DatosCategoria();
                    DatosMarca marca = new DatosMarca();
                    ddlCategoria.DataSource = categoria.listar();
                    ddlCategoria.DataValueField = "Id";
                    ddlCategoria.DataTextField = "Descripcion";
                    ddlCategoria.DataBind();
                    ddlMarca.DataSource = marca.listar();
                    ddlMarca.DataValueField = "Id";
                    ddlMarca.DataTextField = "Descripcion";
                    ddlMarca.DataBind();
                }
                if (Request.QueryString["id"] != null && !IsPostBack)
                {
                    btnAgregar.Text = "Modificar";
                    DatosProducto datos = new DatosProducto();
                    Producto producto = datos.traerProducto(int.Parse(Request.QueryString["id"]));
                    Title = "Modificar " + producto.Nombre;
                    txtCodigo.Text = producto.Codigo;
                    txtNombre.Text = producto.Nombre;
                    txtDescripcion.Text = producto.Descripcion;
                    ddlCategoria.SelectedValue = producto.Categoria.Id.ToString();
                    ddlMarca.SelectedValue = producto.Marca.Id.ToString();
                    txtPrecio.Text = producto.Precio.ToString();
                    if (Validar.imagen(producto.ImagenUrl, producto.Id))
                    {
                        rdbUrl.Checked = true;
                        txtImagenUrl.Text = producto.ImagenUrl;
                        imgProducto.ImageUrl = producto.ImagenUrl;
                    }
                    else
                        imgProducto.ImageUrl = "https://i.imgur.com/yzczBvI.png";
                }
                else
                {
                    Title = "Agregar producto";
                    imgProducto.ImageUrl = "https://i.imgur.com/yzczBvI.png";
                }
            }
            catch (Exception ex)
            {
                Session.Add("ErrorCode", "Hubo un problema al ingresar a la página");
                Session.Add("Error", ex.Message);
                Response.Redirect("Error.aspx");
            }
        }

        protected void btnAgregar_Click(object sender, EventArgs e)
        {
            try
            {
                Page.Validate();
                if (!Page.IsValid)
                    return;

                Producto producto = new Producto();
                producto.Categoria = new Categoria();
                producto.Marca = new Marca();
                producto.Codigo = txtCodigo.Text;
                producto.Nombre = txtNombre.Text;
                producto.Descripcion = txtDescripcion.Text;
                producto.Categoria.Id = int.Parse(ddlCategoria.SelectedValue);
                producto.Marca.Id = int.Parse(ddlMarca.SelectedValue);
                producto.Precio = Helper.cargarPrecio(txtPrecio.Text);
                if (rdbLocal.Checked)
                {
                    if (Request.QueryString["id"] != null)
                    {
                        if (txtImagenLocal.Value != null)
                        {
                            producto.Id = int.Parse(Request.QueryString["id"]);
                            string ruta = Server.MapPath("./Imagenes/Productos/");
                            string img = "product-" + producto.Id + ".png";
                            txtImagenLocal.PostedFile.SaveAs(ruta + img);
                            producto.ImagenUrl = img;
                        }
                    }
                    else
                    {
                        if (txtImagenLocal.Value != null)
                        {
                            DatosProducto datos = new DatosProducto();
                            string ruta = Server.MapPath("./Imagenes/Productos/");
                            string img = "product-" + (datos.ultimoId() + 1) + ".png";
                            txtImagenLocal.PostedFile.SaveAs(ruta + img);
                            producto.ImagenUrl = img;
                        }
                    }
                }
                else if (Validar.campo(txtImagenUrl.Text))
                    producto.ImagenUrl = txtImagenUrl.Text;
                if (Request.QueryString["id"] != null)
                {
                    producto.Id = int.Parse(Request.QueryString["id"]);
                    DatosProducto.agregar(producto, false);
                    Response.Redirect("Details.aspx?id=" + producto.Id + "&page=lp");
                }
                else
                {
                    if (Validar.codigoExistente(producto.Codigo))
                    {
                        lblErrorCodigo.Text = "El código ingresado ya se encuentra en uso, ingrese otro código";
                        lblErrorCodigo.Visible = true;
                        return;
                    }
                    else if (Validar.nombreExistente(producto.Nombre))
                    {
                        lblErrorCodigo.Visible = false;
                        lblErrorNombre.Text = "El nombre ingresado ya se encuentra en uso, ingrese otro nombre";
                        lblErrorNombre.Visible = true;
                        return;
                    }
                    else
                    {
                        lblErrorCodigo.Visible = false;
                        lblErrorNombre.Visible = false;
                        DatosProducto.agregar(producto);
                        Response.Redirect("ProductList.aspx", false);
                    }
                }
            }
            catch (FormatException)
            {
                lblErrorPrecio.Text = "Debe completar este campo solo con números y sin separacion de mil";
                lblErrorPrecio.Visible = true;
            }
            catch (Exception ex)
            {

                Session.Add("ErrorCode", "Hubo un problema al ingresar a la página");
                Session.Add("Error", ex.Message);
                Response.Redirect("Error.aspx");
            }
        }

        protected void txtImagenUrl_TextChanged(object sender, EventArgs e)
        {
            try
            {

            }
            catch (Exception ex)
            {
                Session.Add("ErrorCode", "Hubo un problema al ingresar a la página");
                Session.Add("Error", ex.Message);
                Response.Redirect("Error.aspx");
            }
        }
    }

}