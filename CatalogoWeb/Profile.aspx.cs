using Datos;
using Helpers;
using ModeloDominio;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.IO;
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
        public string Status { get; set; }
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
                notificacion.Visible = true;
                Usuario usuario = Session["usuario"] != null ? (Usuario)Session["usuario"] : null;
                string imagenPerfil;
                string status = "";
                if (rdbLocal.Checked)
                {
                    if (fileLocal.HasFile)
                    {
                        if (fileLocal.PostedFile.ContentLength < 2097152)
                        {
                            string ruta = Server.MapPath("./Imagenes/Perfil/");
                            imagenPerfil = "profile-" + usuario.Id + ".png";
                            fileLocal.PostedFile.SaveAs(ruta + imagenPerfil);
                            imgPerfil.ImageUrl = Helper.cargarImagen(usuario);
                        }
                        else
                        {
                            imagenPerfil = usuario.UrlImagen;
                            lblMensaje.Text = "El tamaño del archivo es demasiado grande, elija uno mas pequeño (2MB Máximo)";
                            lblTituloNotificacion.Text = Modal.armarNotificacion(ajxNotificación, status = "error");
                        }
                    }
                    else
                        imagenPerfil = usuario.UrlImagen;
                }
                else
                    imagenPerfil = string.IsNullOrEmpty(txtImagenUrl.Text) ? usuario.UrlImagen : txtImagenUrl.Text;

                if (Validar.datosPerfil(usuario, txtNombre.Text, txtApellido.Text, imagenPerfil))
                {
                    lblMensaje.Text = Helper.cargarDatosUsuario(usuario, txtNombre.Text, txtApellido.Text, imagenPerfil, ref status);
                    Status = status;
                    lblTituloNotificacion.Text = Modal.armarNotificacion(ajxNotificación, status);
                }
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
            notificacion.Visible = true;
            try
            {
                string status = "";
                Usuario usuario = Session["usuario"] != null ? (Usuario)Session["usuario"] : null;
                if (lblCambioAcceso.Text == "Cambiar Contraseña")
                {
                    lblMensaje.Text = Helper.cargarPass(usuario, txtPassActual.Text, txtPassNueva.Text, txtPassRepetir.Text, ref status);
                    Status = status;
                    lblTituloNotificacion.Text = Modal.armarNotificacion(ajxNotificación, Status);
                }
                else
                {
                    lblMensaje.Text = Helper.cargarEmail(usuario, txtEmailActual.Text, txtEmailNuevo.Text, ref status);
                    Status = status;
                    lblTituloNotificacion.Text = Modal.armarNotificacion(ajxNotificación, Status);
                }
                if (status == "ok")
                {
                    string passCodificada = usuario.Pass;
                    string emailCodificado = usuario.Email;
                    Helper.codificar(ref emailCodificado, ref passCodificada);
                    lblEmailUser.Text = emailCodificado;
                    lblPassUser.Text = passCodificada;
                }
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