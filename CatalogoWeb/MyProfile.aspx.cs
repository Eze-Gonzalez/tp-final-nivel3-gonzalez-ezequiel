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
    public partial class MyProfile : System.Web.UI.Page
    {
        public bool changePass { get; set; }
        public bool changeEmail { get; set; }
        private string mensaje;
        private string titulo;
        private string emailCodificado;
        private string passCodificada;
        private string script;
        private bool status = false;
        protected void Page_Load(object sender, EventArgs e)
        {
            Title = "Mi perfil";
            try
            {
                if (Validar.sesion((Usuario)Session["usuario"]))
                {
                    Usuario usuario = (Usuario)Session["usuario"];
                    if (!IsPostBack)
                    {
                        lblUsuario.Text = Helper.nombre(usuario);
                        codificar(usuario);
                        txtNombre.Text = usuario.Nombre;
                        txtApellido.Text = usuario.Apellido;
                        imgPerfil.ImageUrl = Helper.cargarImagen(usuario);
                    }
                }
            }
            catch (Exception ex)
            {
                Session.Add("ErrorCode", "Hubo un problema al cargar la página");
                Session.Add("Error", ex.Message);
                Response.Redirect("Error.aspx", false);
            }
        }
        protected void btnAceptar_Click(object sender, EventArgs e)
        {
            try
            {
                Usuario usuario = Session["usuario"] != null ? (Usuario)Session["usuario"] : null;
                string imagenPerfil;
                if (rdbLocal.Checked)
                {
                    if (fileLocal.HasFile)
                    {
                        string ruta = Server.MapPath("./Imagenes/Perfil/");
                        imagenPerfil = "profile-" + usuario.Id + ".png";
                        fileLocal.PostedFile.SaveAs(ruta + imagenPerfil);
                        imgPerfil.ImageUrl = Helper.cargarImagen(usuario);
                    }
                    else
                    {
                        imagenPerfil = usuario.UrlImagen;
                        imgPerfil.ImageUrl = Helper.cargarImagen(usuario);
                    }
                }
                else
                    imagenPerfil = string.IsNullOrEmpty(txtImagenUrl.Text) ? usuario.UrlImagen : txtImagenUrl.Text;

                if (Validar.longitudCampos(txtNombre.Text, txtApellido.Text, imgPerfil.ImageUrl))
                {
                    if (Validar.datosPerfil(usuario, txtNombre.Text, txtApellido.Text, imagenPerfil) || fileLocal.HasFile)
                    {
                        lblSinCambios.Visible = false;
                        mensaje = Helper.cargarDatosUsuario(usuario, txtNombre.Text, txtApellido.Text, imagenPerfil, ref status, ref titulo);
                        script = string.Format("crearAlerta({0}, '{1}', '{2}');", status.ToString().ToLower(), titulo, mensaje);
                        txtNombre.Text = usuario.Nombre;
                        txtApellido.Text = usuario.Apellido;
                    }
                    else
                        lblSinCambios.Visible = true;
                    if (status)
                        cargarControles(usuario);
                }
                else
                {
                    titulo = "Error al guardar los datos";
                    mensaje = "Uno o más campos exceden la cantidad máxima de caracteres, intente nuevamente.";
                    script = string.Format("crearAlerta({0}, '{1}', '{2}');", false.ToString().ToLower(), titulo, mensaje);
                }
                ScriptManager.RegisterStartupScript(this, GetType(), "crearAlerta", script, true);
            }
            catch (Exception ex)
            {
                Session.Add("ErrorCode", "Hubo un problema al cargar la página");
                Session.Add("Error", ex.Message);
                Response.Redirect("Error.aspx", false);
            }
        }

        protected void btnCambiarEmail_Click(object sender, EventArgs e)
        {
            changeEmail = true;
            try
            {
                lblCambioAcceso.Text = "Cambiar Email";
                ajxModal.Show();
            }
            catch (Exception ex)
            {
                Session.Add("ErrorCode", "Hubo un problema al cargar la página");
                Session.Add("Error", ex.Message);
                Response.Redirect("Error.aspx");
            }
        }

        protected void btnCambiarPass_Click(object sender, EventArgs e)
        {
            changePass = true;
            try
            {
                lblCambioAcceso.Text = "Cambiar Contraseña";
                ajxModal.Show();
            }
            catch (Exception ex)
            {
                Session.Add("ErrorCode", "Hubo un problema al cargar la página");
                Session.Add("Error", ex.Message);
                Response.Redirect("Error.aspx", false);
            }
        }

        protected void btnCambiar_Click(object sender, EventArgs e)
        {
            try
            {
                Usuario usuario = Session["usuario"] != null ? (Usuario)Session["usuario"] : null;
                if (lblCambioAcceso.Text == "Cambiar Contraseña")
                {
                    mensaje = Helper.cargarPass(usuario, txtPassActual.Text, txtPassNueva.Text, txtPassRepetir.Text, ref status, ref titulo);
                    script = string.Format("crearAlerta({0}, '{1}', '{2}');", status.ToString().ToLower(), titulo, mensaje);
                }
                else
                {
                    if (Validar.longitudCampos(txtEmailNuevo.Text))
                    {
                        txtEmailActual.Text = txtEmailActual.Text.ToLower();
                        txtEmailNuevo.Text = txtEmailNuevo.Text.ToLower();
                        mensaje = Helper.cargarEmail(usuario, txtEmailActual.Text, txtEmailNuevo.Text, ref status, ref titulo);
                        script = string.Format("crearAlerta({0}, '{1}', '{2}');", status.ToString().ToLower(), titulo, mensaje);
                    }
                    else
                    {
                        titulo = "Email demasiado largo";
                        mensaje = "El nuevo email es demasiado larga, pruebe con uno mas corto.";
                        script = string.Format("crearAlerta({0}, '{1}', '{2}');", false.ToString().ToLower(), titulo, mensaje);
                    }
                }
                if (status)
                {
                    codificar(usuario);
                    cargarControles(usuario);
                }
                ScriptManager.RegisterStartupScript(this, GetType(), "crearAlerta", script, true);
            }
            catch (Exception ex)
            {
                Session.Add("ErrorCode", "Hubo un problema al cargar la página");
                Session.Add("Error", ex.Message);
                Response.Redirect("Error.aspx", false);
            }
            finally
            {
                ajxModal.Hide();
            }
        }

        protected void btnCerrarModal_Click(object sender, EventArgs e)
        {
            ajxModal.Hide();
        }

        protected void txtNombre_TextChanged(object sender, EventArgs e)
        {
            lblSinCambios.Visible = false;
        }
        private void codificar(Usuario usuario)
        {
            try
            {
                emailCodificado = usuario.Email;
                passCodificada = usuario.Pass;
                Helper.codificar(ref emailCodificado, ref passCodificada);
                lblEmailUser.Text = emailCodificado;
                lblPassUser.Text = passCodificada;
            }
            catch (Exception ex)
            {
                Session.Add("ErrorCode", "Hubo un problema al cargar la pagina");
                Session.Add("Error", ex.Message);
                Response.Redirect("Default.aspx", false);
            }
        }
        private void cargarControles(Usuario usuario)
        {
            try
            {
                lblUsuario.Text = Helper.nombre(usuario);
                Label lblMaster = (Label)Master.FindControl("lblPerfil");
                Image imgMaster = (Image)Master.FindControl("imgPerfil");
                lblMaster.Text = lblUsuario.Text;
                imgMaster.ImageUrl = imgPerfil.ImageUrl;
                Session["usuario"] = usuario;
            }
            catch (Exception ex)
            {
                Session.Add("ErrorCode", "Hubo un error al cargar la página");
                Session.Add("Error", ex.Message);
                Response.Redirect("Error.aspx", false);
            }
        }
    }
}