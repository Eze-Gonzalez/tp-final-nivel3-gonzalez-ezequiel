using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Validaciones;
using Helpers;
using ModeloDominio;

namespace CatalogoWeb
{
    public partial class Catalogo : System.Web.UI.MasterPage
    {
        public string Status { get; set; }
        protected void Page_Load(object sender, EventArgs e)
        {
            //Valida si esta iniciada una sesion en las paginas necesarias
            if (!(Page is Login || Page is SignUp || Page is ChangePass || Page is Default || Page is Details))
            {
                if (!Validar.sesion(Session["usuario"]))
                    Response.Redirect("Login.aspx");
            }
            //Valida si el usuario es admin para ingresar a ciertas paginas
            if (Page is ListaProductos || Page is Product)
            {
                if (!Validar.admin(Session["usuario"]))
                {
                    Session.Add("ErrorCode", "No tiene permiso para acceder");
                    Session.Add("Error", "Lo sentimos, no tiene permiso para acceder a esta página. Si tiene una cuenta de administrador, inicie sesón con su cuenta administrador, si usted es un empleado y necesita su codigo de admin envíe un email a administración.");
                    Response.Redirect("Error.aspx");
                }
            }
            //Valida si hay sesion iniciada, para luego cargar los controles en la pagina
            if (Validar.sesion(Session["usuario"]))
            {
                Usuario usuario = Session["usuario"] != null ? (Usuario)Session["usuario"] : null;
                lblUsuario.Text = Helper.nombre((Usuario)Session["usuario"]);
                lblPerfil.Text = lblUsuario.Text;
                //Carga imagen de perfil
                imgPerfil.ImageUrl = Helper.cargarImagen(usuario);
                imgProfileCard.ImageUrl = imgPerfil.ImageUrl;
                //Valida si el usuario es administrador, para agregar la etiqueta admin
                if (Validar.admin(Session["usuario"]))
                    lblTipoUsuario.Visible = true;
                else
                    lblTipoUsuario.Visible = false;
            }


        }

        protected void btnCerrarSesion_Click(object sender, EventArgs e)
        {
            Session.Clear();
            Response.Redirect("Default.aspx");
        }

        //protected void btnAceptarN_Click(object sender, EventArgs e)
        //{
        //    Status = 
        //    ajxNotificacion.Hide();
        //}
    }
}