using Datos;
using Helpers;
using ModeloDominio;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Validaciones;
using static System.Net.Mime.MediaTypeNames;

namespace CatalogoWeb
{
    public partial class Profile : System.Web.UI.Page
    {
        public bool changePass { get; set; }
        public bool changeEmail { get; set; }
        protected void Page_Load(object sender, EventArgs e)
        {
            Title = "Mi perfil";
            try
            {
                if (Validar.sesion((Usuario)Session["usuario"]))
                {
                    if (!IsPostBack)
                    {
                        Usuario usuario = (Usuario)Session["usuario"];
                        string emailCodificado = usuario.Email;
                        string passCodificada = usuario.Pass;
                        Helper.codificar(ref emailCodificado, ref passCodificada);
                        lblUsuario.Text = Helper.nombre(usuario);
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
                string icono = "";
                string status = "Ok";
                if (rdbLocal.Checked)
                {
                    if (!string.IsNullOrEmpty(txtImagenLocal.Value))
                    {
                        string ruta = Server.MapPath("./Imagenes/Perfil/");
                        string img = "profile-" + usuario.Id + ".png";
                        txtImagenLocal.PostedFile.SaveAs(ruta + img);
                        imagenPerfil = img;
                    }
                    else
                        imagenPerfil = usuario.UrlImagen;
                }
                else
                    imagenPerfil = string.IsNullOrEmpty(txtImagenUrl.Text) ? usuario.UrlImagen : txtImagenUrl.Text;
                lblMensaje.Text = Helper.cargarDatosUsuario(usuario, txtNombre.Text, txtApellido.Text, imagenPerfil, ref icono, ref status);
                Modal.armarNotificacion(ajxNotificación, ref lblTituloNotificacion, status);
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
                string icono = "error";
                if (changePass)
                {
                    string mensaje = Helper.cargarPass(usuario, txtPassActual.Text, txtPassNueva.Text, txtPassRepetir.Text, ref icono);
                    if (icono == "error")
                    {
                        ClientScript.RegisterClientScriptBlock(GetType(), "alert", "swal('Listo!', 'Los cambios fueron guardados exitosamente', { button: {text:'Aceptar', className: 'swal-button'}, icon: '" + icono + "', className: 'swal-bg'})", true);
                    }
                    else
                        ClientScript.RegisterClientScriptBlock(GetType(), "alert", "swal('Listo!', 'Los cambios fueron guardados exitosamente', { button: {text:'Aceptar', className: 'swal-button'}, icon: '" + icono + "', className: 'swal-bg'})", true);

                }
                if (changeEmail)
                {
                    lblMensaje.Text = Helper.cargarEmail(usuario, txtEmailActual.Text, txtEmailNuevo.Text, ref icono);
                }
                string passCodificada = usuario.Pass;
                string emailCodificado = usuario.Email;
                Helper.codificar(ref emailCodificado, ref passCodificada);
                lblEmailUser.Text = emailCodificado;
                lblPassUser.Text = passCodificada;
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

        protected void btnAceptarN_Click(object sender, EventArgs e)
        {
            ajxNotificación.Hide();
        }

        protected void btnCerrarModal_Click(object sender, EventArgs e)
        {
            ajxModal.Hide();
        }
    }
}