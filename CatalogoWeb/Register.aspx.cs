﻿using Helpers;
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
    public partial class Register : System.Web.UI.Page
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
                string script;
                string titulo = "";
                string mensaje;
                bool status = false;
                if (!Page.IsValid)
                {
                    titulo = "No se permiten campos vacíos";
                    mensaje = "Debe completar todos los campos para continuar";
                    script = string.Format("crearAlerta({0},'{1}','{2}');", status.ToString().ToLower(), titulo, mensaje);
                    ScriptManager.RegisterStartupScript(this, GetType(), "crearAlerta", script, true);
                }
                Usuario usuario = new Usuario();
                mensaje = Helper.registro(usuario, txtEmail.Text, txtPassword.Text, txtRepetir.Text, ref status, ref titulo);
                script = string.Format("crearAlerta({0},'{1}','{2}');", status.ToString().ToLower(), titulo, mensaje);
                ScriptManager.RegisterStartupScript(this, GetType(), "crearAlerta", script, true);
                if (status)
                {
                    Session.Add("usuario", usuario);
                    Response.Redirect("Profile.aspx?id=" + usuario.Id);
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
    }
}