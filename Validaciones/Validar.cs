using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Database;
using ModeloDominio;


namespace Validaciones
{
    public static class Validar
    {
        //Valida el inicio de sesión y carga los datos del usuario.
        public static bool inicioSesion(object user)
        {
            AccesoDatos datos = new AccesoDatos();
            Usuario usuario = user != null ? (Usuario)user : null;
            try
            {
                datos.consultaEmbebida("Select Id, email, pass, admin, urlImagenPerfil, nombre, apellido from USERS where email = @email and pass = @pass");
                datos.parametros("@email", usuario.Email);
                datos.parametros("@pass", usuario.Pass);
                datos.lectura();
                if (datos.Lector.Read())
                {
                    usuario.Id = (int)datos.Lector["Id"];
                    usuario.Email = (string)datos.Lector["email"];
                    usuario.Pass = (string)datos.Lector["pass"];
                    usuario.Admin = (bool)datos.Lector["admin"];
                    if (!(datos.Lector["urlImagenPerfil"] is DBNull))
                        usuario.UrlImagen = (string)datos.Lector["urlImagenPerfil"];
                    if (!(datos.Lector["nombre"] is DBNull))
                        usuario.Nombre = (string)datos.Lector["nombre"];
                    if (!(datos.Lector["apellido"] is DBNull))
                        usuario.Apellido = (string)datos.Lector["apellido"];
                    return true;
                }
                return false;

            }
            catch (Exception ex)
            {

                throw ex;
            }
            finally
            {
                datos.cerrarConexion();
            }
        }
        //Valida si el usuario que inició sesión es administrador.
        public static bool admin(object usuario)
        {
            try
            {
                return usuario != null ? ((Usuario)usuario).Admin : false;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        //Valida si hay una sesión activa.
        public static bool sesion(object usuario)
        {
            try
            {
                if (usuario != null && ((Usuario)usuario).Id != 0)
                    return true;
                return false;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        //Valida que el email ingresado existe (Esto es para que en el registro, no puedan ingresar 2 emails iguales)
        public static bool email(string email)
        {
            AccesoDatos datos = new AccesoDatos();
            try
            {
                datos.consultaEmbebida("Select Id from USERS where email = @email");
                datos.parametros("@email", email);
                datos.lectura();
                if (datos.Lector.Read())
                    return true;
                return false;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                datos.cerrarConexion();
            }
        }
        //Valida si hay una imagen disponible para cargar
        public static bool imagen(string imagen, int id = 0)
        {
            try
            {
                if (!string.IsNullOrEmpty(imagen) && (!imagen.Contains(" ") && imagen.ToLower().Contains("http") || imagen.ToLower().Contains(id + ".png")))
                {
                    return true;
                }
                return false;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        //Valida si hay articulos favoritos.
        public static bool favExistente(int idProd, int idUser)
        {
            AccesoDatos datos = new AccesoDatos();
            try
            {
                datos.consultaEmbebida("select id from FAVORITOS where IdUser = @iduser and IdArticulo = @idprod");
                datos.parametros("@iduser", idUser);
                datos.parametros("@idprod", idProd);
                datos.lectura();
                if (datos.Lector.Read())
                    return true;
                return false;
            }
            catch (Exception ex)
            {

                throw ex;
            }
            finally
            {
                datos.cerrarConexion();
            }
        }
        //Valida los campos email y pass, para que no ingresen vacios (No manejo longitud por el momento)
        public static bool campos(string email, string pass)
        {
            string[] split;
            split = email.Split('@');
            if ((string.IsNullOrEmpty(email) || email == " " || email.Contains(" ") || !email.Contains("@") || !split[split.Length - 1].Contains(".com")) || (string.IsNullOrEmpty(pass) || pass == " "))
                return false;
            return true;
        }
        //Valida con regex el campo email (en el front me obliga a cargar el campo)
        public static bool campoEmail(string email)
        {
            try
            {
                Regex regex = new Regex(@"^([a-zA-Z0-9_\-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([a-zA-Z0-9\-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$", RegexOptions.CultureInvariant | RegexOptions.Singleline);
                bool validar = regex.IsMatch(email);
                return validar;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        //Valida con regex el campo pass (en el front me obliga a cargar la pass)
        public static bool campoPass(string pass)
        {
            try
            {
                Regex regex = new Regex("^(?=.*?[A-Z])(?=.*?[a-z])(?=.*?[0-9]).{3,20}$");
                bool validar = regex.IsMatch(pass);
                return validar;
            }
            catch (Exception)
            {

                throw;
            }
        }
        //Valida que el campo que se ingrese no este vacio.
        public static bool campo(string campo)
        {
            try
            {
                if (string.IsNullOrEmpty(campo) || campo == " ")
                    return false;
                return true;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        public static bool codigoExistente(string codigo)
        {
            AccesoDatos datos = new AccesoDatos();
            try
            {
                datos.consultaEmbebida("Select Id from Articulos where Codigo = @codigo");
                datos.parametros("@codigo", codigo);
                datos.lectura();
                if (datos.Lector.Read())
                    return true;
                return false;
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
        public static bool nombreExistente(string nombre)
        {
            AccesoDatos datos = new AccesoDatos();
            try
            {
                datos.consultaEmbebida("Select Id from Articulos where Nombre = @nombre");
                datos.parametros("@nombre", nombre);
                datos.lectura();
                if (datos.Lector.Read())
                    return true;
                return false;
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

        public static bool nombreApellido(Usuario usuario, string nombre, string apellido)
        {
            try
            {
                if (nombre != usuario.Nombre || apellido != usuario.Apellido)
                    return true;
                return false;
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
