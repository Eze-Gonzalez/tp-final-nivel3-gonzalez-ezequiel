using Datos;
using ModeloDominio;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Validaciones;

namespace CatalogoWeb
{
    public partial class Login : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                Title = "Iniciar Sesión";
                if (Validar.sesion(Session["usuario"]))
                {
                    Session.Add("ErrorCode", "Ya hay una sesión iniciada");
                    Session.Add("Error", "Ya hay una sesión activa, si desea iniciar sesión con otra cuenta, primero cierre esta sesión y luego inicie sesión nuevamente.");
                    Response.Redirect("Error.aspx");
                }
                else if (Session["cont"] != null && (int)Session["cont"] >= 5 && Session["usuarioEmergencia"] != null)
                {
                    Session.Add("ErrorLogin", "Alcanzó los intentos máximos para iniciar sesión, si olvidó su contraseña, puede cambiarla");
                    Response.Redirect("ChangePass.aspx");
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

        protected void btnIniciarSesion_Click(object sender, EventArgs e)
        {
            bool status;
            string titulo;
            string mensaje;
            string script;
            try
            {
                Page.Validate();
                if (!Page.IsValid)
                {
                    status = false;
                    titulo = "No se admiten campos vacíos";
                    mensaje = "Debe completar ambos campos para iniciar sesión.";
                    script = string.Format("crearAlerta({0}, '{1}', '{2}');", status.ToString().ToLower(), titulo, mensaje);
                    ScriptManager.RegisterStartupScript(this, GetType(), "crearAlerta", script, true);
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
                Usuario usuario = new Usuario();
                if (Validar.campoEmail(txtEmail.Text))
                {
                    if (Validar.email(txtEmail.Text))
                    {
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
                            status = false;
                            titulo = "Credenciales incorrectas";
                            cont++;
                            mensaje = cont < 5 ? "Email o Contraseña incorrectos, intente nuevamente. Le quedan " + (6 - cont) + " intentos." : "Email o Contraseña incorrectos, intente nuevamente. Le queda 1 intento.";
                            script = string.Format("crearAlerta({0}, '{1}', '{2}');", status.ToString().ToLower(), titulo, mensaje);
                            ScriptManager.RegisterStartupScript(this, GetType(), "crearAlerta", script, true);
                            Session.Add("cont", cont);
                        }
                        else
                        {
                            DatosUsuario datos = new DatosUsuario();
                            usuario = datos.sesionEmergencia(txtEmail.Text);
                            Session.Add("usuarioEmergencia", usuario);
                            Session.Add("ErrorLogin", "Alcanzó los intentos máximos para iniciar sesión, si olvidó su contraseña, puede cambiarla");
                            Response.Redirect("ChangePass.aspx");
                        }
                    }
                    else
                    {
                        status = false;
                        titulo = "Email no registrado";
                        mensaje = "El email ingresado, no se encuentra registrado, si desea registrarse, haga click en Registrarse.";
                        script = string.Format("crearAlerta({0}, '{1}', '{2}');", status.ToString().ToLower(), titulo, mensaje);
                        ScriptManager.RegisterStartupScript(this, GetType(), "crearAlerta", script, true);
                    }
                }
                else
                {
                    status = false;
                    titulo = "Email no válido";
                    mensaje = "Debe introducir un email válido.";
                    script = string.Format("crearAlerta({0}, '{1}', '{2}');", status.ToString().ToLower(), titulo, mensaje);
                    ScriptManager.RegisterStartupScript(this, GetType(), "crearAlerta", script, true);
                }
            }
            catch (ThreadAbortException) { }
            catch (SqlException ex)
            {
                Session.Add("ErrorCode", "Hubo un error al conectar con la base de datos.");
                Session.Add("Error", ex.Message);
                Response.Redirect("Error.aspx", false);
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}