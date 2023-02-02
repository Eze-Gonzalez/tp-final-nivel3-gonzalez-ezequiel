using Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Validaciones;
using ModeloDominio;
using System.Threading;

namespace CatalogoWeb
{
    public partial class Catalogo : System.Web.UI.MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                //Valida si esta iniciada una sesion en las paginas necesarias
                if (!(Page is Login || Page is Register || Page is ChangePass || Page is Default || Page is Details))
                {
                    if (!Validar.sesion(Session["usuario"]))
                        Response.Redirect("Login.aspx");
                }
                //Valida si el usuario es admin para ingresar a ciertas paginas
                if (Page is ProductList || Page is AddProduct)
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
                    lblPerfil.Text = Helper.nombre(usuario);
                    //Carga imagen de perfil
                    imgPerfil.ImageUrl = Helper.cargarImagen(usuario);
                }
            }
            catch (ThreadAbortException) { }
            catch (Exception ex)
            {
                Session.Add("ErrorCode", "Hubo un problema al cargar la página");
                Session.Add("Error", ex.Message);
                Response.Redirect("Error.aspx", false);
            }
        }

        protected void btnCerrarSesion_Click(object sender, EventArgs e)
        {
            try
            {
                Session.Clear();
                Response.Redirect("Default.aspx");
            }
            catch (ThreadAbortException) { }
            catch (Exception ex)
            {
                Session.Add("ErrorCode", "Hubo un problema al cargar la página");
                Session.Add("Error", ex.Message);
                Response.Redirect("Error.aspx", false);
            }
        }
    }
}