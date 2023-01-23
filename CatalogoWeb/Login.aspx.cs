using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ModeloDominio;
using Validaciones;
using Helpers;

namespace CatalogoWeb
{
    public partial class Login : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Title = "Iniciar Sesión";
            if (Validar.sesion(Session["usuario"]))
            {
                Session.Add("ErrorCode", "Ya hay una sesión iniciada");
                Session.Add("Error", "Ya hay una sesión activa, si desea iniciar sesión con otra cuenta, primero cierre esta sesión y luego inicie sesión nuevamente.");
                Response.Redirect("Error.aspx");
            }
            else if (Session["cont"] != null && (int)Session["cont"] >= 5)
            {
                Session.Add("ErrorLogin", "Alcanzó los intentos máximos para iniciar sesión, si olvidó su contraseña, puede cambiarla");
                Response.Redirect("ChangePass.aspx");
            }
        }

        protected void btnIniciarSesion_Click(object sender, EventArgs e)
        {
            Page.Validate();
            if (!Page.IsValid)
            {
                ClientScript.RegisterClientScriptBlock(GetType(), "alert", "swal('No se admiten campos vacíos', 'Debe completar ambos campos para iniciar sesión', { button: {text:'Aceptar', className: 'swal-button'}, icon: 'error', className: 'swal-bg'})", true);
                return;
            }
            int cont;
            if (Session["cont"] == null)
            {
                cont = 1;
            }
            else
            {
                cont = (int)Session["cont"];
            }

            try
            {
                Usuario usuario = new Usuario();
                usuario.Email = txtEmail.Text;
                usuario.Pass = txtPassword.Text;
                if (Validar.inicioSesion(usuario) && cont < 5)
                {
                    Session["cont"] = null;
                    Session.Add("usuario", usuario);
                    Response.Redirect("Default.aspx");
                }
                else if (cont <= 4)
                {
                    ClientScript.RegisterClientScriptBlock(GetType(), "alert", "swal('Error al iniciar sesión', 'Usuario o contraseña incorrectos, intente nuevamente', { button: {text:'Aceptar', className: 'swal-button'}, icon: 'error', className: 'swal-bg'})", true);
                    cont++;
                    Session.Add("cont", cont);
                }
                else
                {
                    Session.Add("ErrorLogin", "Alcanzó los intentos máximos para iniciar sesión, si olvidó su contraseña, puede cambiarla");
                    Response.Redirect("ChangePass.aspx");
                }

            }
            catch (ThreadAbortException) { }
            catch (SqlException ex)
            {
                Session.Add("ErrorCode", "Hubo un error al conectar con la base de datos.");
                Session.Add("Error", ex.Message);
                Response.Redirect("Error.aspx");
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}