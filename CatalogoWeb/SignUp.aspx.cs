using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Validaciones;
using Datos;
using ModeloDominio;
using System.Data;
using System.Threading;
using Servicios;

namespace CatalogoWeb
{
    public partial class SignUp : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Title = "Registrarse";
            if (Validar.sesion(Session["usuario"]))
            {
                Session.Add("ErrorCode", "Ya hay una sesión iniciada");
                Session.Add("Error", "Actualmente ya hay una sesión activa, para registrarse, no debe tener una cuenta en el sitio, si desea crear otra cuenta, primero cierre sesión y luego vaya a la pagina registro.");
                Response.Redirect("Error.aspx");
            }
        }

        protected void btnAceptar_Click(object sender, EventArgs e)
        {
            try
            {
                Page.Validate();
                if (!Page.IsValid)
                {
                    return;
                }
                string email = txtEmail.Text;
                if (!Validar.email(email))
                {
                    if (Validar.campoEmail(email))
                    {
                        lblErrorEmail.Visible = false;
                        Usuario nuevo = new Usuario();
                        DatosUsuario datos = new DatosUsuario();
                        nuevo.Email = email;
                        if (txtRepetir.Text == txtPassword.Text)
                        {
                            ServicioEmail envioEmail = new ServicioEmail();
                            lblErrorRep.Visible = false;
                            nuevo.Pass = txtPassword.Text;
                            nuevo.Id = datos.nuevoUsuario(nuevo);
                            envioEmail.armarEmail(nuevo.Email, "Bienvenido", "Gracias por registrarte en la web");
                            envioEmail.enviarEmail();
                            Session.Add("usuario", nuevo);
                            Response.Redirect("Profile.aspx?id=" + nuevo.Id);
                        }
                        else
                        {
                            lblErrorRep.Text = "Las contraseñas no coinciden, repita la misma contraseña.";
                            lblErrorRep.Visible = true;
                        }
                    }
                    else
                    {
                        lblErrorEmail.Text = "Debe completar este campo con un email válido.";
                        lblErrorEmail.Visible = true;
                    }
                }
                else
                {
                    lblErrorEmail.Text = "El email ingresado ya se encuentra registrado, intente con otro.";
                    lblErrorEmail.Visible = true;
                }

            }
            catch (ThreadAbortException) { }
            catch (Exception)
            {

                throw;
            }

        }
    }
}