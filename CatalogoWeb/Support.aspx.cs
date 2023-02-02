using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace CatalogoWeb
{
    public partial class Support : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (Request.Headers["X-Requested-With"] == "XMLHttpRequest")
                {
                    Session.Clear();
                }
                Response.Redirect("Default.aspx", false);
            }
            catch (ThreadAbortException) { }
            catch (Exception ex)
            {
                Session.Add("ErrorCode", "Hubo un problema al cargar la página");
                Session.Add("Error", ex.Message);
                Response.Redirect("Error.aspx");
            }
        }
    }
}