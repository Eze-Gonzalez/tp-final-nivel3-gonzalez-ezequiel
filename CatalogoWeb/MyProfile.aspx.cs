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
        private bool status;
        protected void Page_Load(object sender, EventArgs e)
        {
            Title = "Mi perfil";
            try
            {
                if (Validar.sesion((Usuario)Session["usuario"]))
                {
                    Usuario usuario = (Usuario)Session["usuario"];
                    lblUsuario.Text = Helper.nombre(usuario);
                    if (!IsPostBack)
                    {
                        emailCodificado = usuario.Email;
                        passCodificada = usuario.Pass;
                        Helper.codificar(ref emailCodificado, ref passCodificada);
                        txtNombre.Text = usuario.Nombre;
                        txtApellido.Text = usuario.Apellido;
                        lblEmailUser.Text = emailCodificado;
                        lblPassUser.Text = passCodificada;
                        imgPerfil.ImageUrl = Helper.cargarImagen(usuario);

                    }
                }
            }
            catch (Exception)
            {

                throw;
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
                        imagenPerfil = usuario.UrlImagen;
                }
                else
                    imagenPerfil = string.IsNullOrEmpty(txtImagenUrl.Text) ? usuario.UrlImagen : txtImagenUrl.Text;

                if (Validar.datosPerfil(usuario, txtNombre.Text, txtApellido.Text, imagenPerfil) || fileLocal.HasFile)
                {
                    lblSinCambios.Visible = false;
                    mensaje = Helper.cargarDatosUsuario(usuario, txtNombre.Text, txtApellido.Text, imagenPerfil, ref status, ref titulo);
                    script = string.Format("crearAlerta({0}, '{1}', '{2}');", status.ToString().ToLower(), titulo, mensaje);
                    ScriptManager.RegisterStartupScript(this, GetType(), "crearAlerta", script, true);
                    txtNombre.Text = usuario.Nombre;
                    txtApellido.Text = usuario.Apellido;
                }
                else
                    lblSinCambios.Visible = true;
            }
            catch (Exception)
            {

                throw;
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
            catch (Exception)
            {

                throw;
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
            catch (Exception)
            {

                throw;
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
                    mensaje = Helper.cargarEmail(usuario, txtEmailActual.Text, txtEmailNuevo.Text, ref status, ref titulo);
                    script = string.Format("crearAlerta({0}, '{1}', '{2}');", status.ToString().ToLower(), titulo, mensaje);
                }
                if (status)
                {
                    passCodificada = usuario.Pass;
                    emailCodificado = usuario.Email;
                    Helper.codificar(ref emailCodificado, ref passCodificada);
                    lblEmailUser.Text = emailCodificado;
                    lblPassUser.Text = passCodificada;
                }
                ScriptManager.RegisterStartupScript(this, GetType(), "crearAlerta", script, true);
            }
            catch (Exception)
            {

                throw;
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
    }
}