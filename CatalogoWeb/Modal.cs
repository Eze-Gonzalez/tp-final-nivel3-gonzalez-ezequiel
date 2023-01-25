using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI.WebControls;

namespace CatalogoWeb
{
    public static class Modal
    {
        public static string armarNotificacion(AjaxControlToolkit.ModalPopupExtender ajax, string status)
        {
            string titulo = "";
            switch (status)
            {
                case "ok":
                    titulo = "Listo!";
                    break;
                case "error":
                    titulo = "Hubo un problema";
                    break;
                case "advertencia":
                    titulo = "No encontrado";
                    break;
            }
            ajax.Show();
            return titulo;
        }
    }
}