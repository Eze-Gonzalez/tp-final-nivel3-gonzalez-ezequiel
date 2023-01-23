using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI.WebControls;

namespace CatalogoWeb
{
    public static class Modal
    {
        public static void armarNotificacion(AjaxControlToolkit.ModalPopupExtender ajax, ref Label lblTitulo, string status)
        {
            switch (status)
            {
                case "ok":
                    lblTitulo.Text = "Listo!";
                    break;
                case "error":
                    lblTitulo.Text = "Hubo un problema";
                    break;
                case "advertencia":
                    lblTitulo.Text = "No se detectaron cambios";
                    break;
            }
            ajax.Show();
        }
    }
}