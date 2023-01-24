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
        public int nuevoUsuario(Usuario usuario)
        {
            AccesoDatos datos = new AccesoDatos();
            try
            {
                datos.consultaEmbebida("insert into USERS (email, pass, admin) output inserted.Id values (@email, @pass, @admin)");
                datos.parametros("@email", usuario.Email);
                datos.parametros("@pass", usuario.Pass);
                datos.parametros("@admin", usuario.Admin);
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
                datos.consultaEmbebida("update USERS set nombre = @nombre, apellido = @apellido, urlImagenPerfil = @img where id = @id");
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
