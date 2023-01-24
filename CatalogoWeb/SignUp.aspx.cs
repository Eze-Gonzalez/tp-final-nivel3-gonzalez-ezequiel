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
                if (Validar.campoEmail(txtEmail.Text))
                {
                    if (!Validar.email(txtEmail.Text))
                    {
                        lblErrorEmail.Visible = false;
                        if (Validar.campoPass(txtPassword.Text))
                        {
                            lblErrorPass.Visible = false;
                            if (txtRepetir.Text == txtPassword.Text)
                            {
                                lblErrorRep.Visible = false;
                                DatosUsuario datos = new DatosUsuario();
                                Usuario usuario = new Usuario();
                                usuario.Email = txtEmail.Text;
                                usuario.Pass = txtRepetir.Text;
                                usuario.Id = datos.nuevoUsuario(usuario);
                                Session.Add("usuario", usuario);
                                Response.Redirect("Profile.aspx?id=" + usuario.Id, false);
                            }
                            else
                            {
                                lblErrorRep.Text = "Las contraseñas no coinciden, intente nuevamente.";
                            }
                        }
                        else
                        {
                            lblErrorPass.Text = "Debe introducir una contraseña de 3 a 20 dígitos, incluyendo al menos, una mayúscula, una minúscula y un número.";
                            lblErrorPass.Visible = true;
                        }
                    }
                    else
                    {
                        lblErrorEmail.Text = "El email ingresado ya se encuentra registrado, intente con otro.";
                        lblErrorEmail.Visible = true;
                    }
                }
                else
                {
                    lblErrorEmail.Text = "Debe ingresar un email válido.";
                    lblErrorEmail.Visible = true;
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
    }
}