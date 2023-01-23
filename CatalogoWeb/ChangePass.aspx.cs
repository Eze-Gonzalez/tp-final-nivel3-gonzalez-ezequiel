using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Validaciones;

namespace CatalogoWeb
{
    public partial class ChangePass : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Validar.sesion(Session["usuario"]))
            {
                Session.Add("ErrorCode", "Ya hay una sesión iniciada");
                Session.Add("Error", "Actualmente hay una sesión iniciada, para cambiar su contraseña, por favor, diríjase a la página Mi perfil y seleccione la opción de cambiar datos de acceso");
                Response.Redirect("Error.aspx");
            }
            else if (Session["ErrorLogin"] == null)
            {
                lblCambiar.Visible = true;
                lblError.Visible = false;
            }
            else
            {
                lblCambiar.Visible = false;
                lblError.Text = (string)Session["ErrorLogin"];
                lblError.Visible = true;
            }
        }

        protected void btnAceptar_Click(object sender, EventArgs e)
        {
            if (Validar.campoEmail(txtEmail.Text))
            {
                lblErrorPass.Text = "Debe ingresar un email válido.";
                lblErrorPass.Visible = true;
            }
            else if (Validar.campo(txtPassword.Text))
            {
                lblErrorPass.Text = "Debe ingresar una contraseña.";
                lblErrorPass.Visible = true;

            }
            else if (Validar.campo(txtPassword.Text))
            {
                lblErrorPass.Text = "Debe repetir la contraseña ingresada.";
                lblErrorPass.Visible = true;
            }
            else
            {
                lblErrorPass.Visible = false;
            }
        }
    }
}