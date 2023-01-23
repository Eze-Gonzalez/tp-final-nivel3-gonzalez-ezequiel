using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Database;
using ModeloDominio;

namespace Datos
{
    public class DatosUsuario
    {
        public int nuevoUsuario(Usuario usuario, bool admin = false)
        {
            AccesoDatos datos = new AccesoDatos();
            try
            {
                datos.consultaSP("NuevoUsuario");
                datos.parametros("@email", usuario.Email);
                datos.parametros("@pass", usuario.Pass);
                if (!admin)
                    datos.parametros("@admin", false);
                else
                    datos.parametros("@admin", true);
                return datos.ejecutarScalar();
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                datos.cerrarConexion();
            }
        }
        public void modificarUsuario(Usuario usuario)
        {
            AccesoDatos datos = new AccesoDatos();
            try
            {
                datos.consultaSP("modificarUsuario");
                datos.parametros("@nombre", usuario.Nombre);
                datos.parametros("@apellido", usuario.Apellido);
                datos.parametros("@img", string.IsNullOrEmpty(usuario.UrlImagen) ? (object)DBNull.Value : usuario.UrlImagen);
                datos.parametros("@id", usuario.Id);
                datos.ejecutar();
            }
            catch (Exception)
            {

                throw;
            }
            finally
            {
                datos.cerrarConexion();
            }
        }
        public static int idUsuario(object usuario)
        {
            AccesoDatos datos = new AccesoDatos();
            Usuario user = usuario != null ? (Usuario)usuario : null;
            try
            {
                datos.consultaSP("idUsuario");
                datos.parametros("@pass", user.Pass);
                datos.parametros("@email", user.Email);
                datos.lectura();
                if (datos.Lector.Read())
                    return (int)datos.Lector["Id"];
                return 0;
            }
            catch (Exception)
            {

                throw;
            }
            finally
            {
                datos.cerrarConexion();
            }
        }

        public void cambiarEmail(string email, int id)
        {
            AccesoDatos datos = new AccesoDatos();
            try
            {
                datos.consultaEmbebida("Update Users set email = @email where Id = @id");
                datos.parametros("@email", email);
                datos.parametros("@id", id);
                datos.ejecutar();
            }
            catch (Exception)
            {

                throw;
            }
            finally
            {
                datos.cerrarConexion();
            }
        }
        public void cambiarPass(string pass, int id)
        {
            AccesoDatos datos = new AccesoDatos();
            try
            {
                datos.consultaEmbebida("Update Users set pass = @pass where Id = @id");
                datos.parametros("@pass", pass);
                datos.parametros("@id", id);
                datos.ejecutar();
            }
            catch (Exception)
            {

                throw;
            }
            finally
            {
                datos.cerrarConexion();
            }
        }
    }
}
