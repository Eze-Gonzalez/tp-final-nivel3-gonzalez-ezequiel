using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using ModeloDominio;
using Validaciones;
using Datos;
using System.Configuration;

namespace Helpers
{
    public static class Helper
    {
        //Carga el nombre o email de la persona, dependiendo si tiene cargado o no el nombre.
        public static string nombre(Usuario usuario)
        {
            try
            {
                string nombre = "";
                TextInfo Ti = new CultureInfo("es-MX", false).TextInfo;
                if (usuario != null)
                {
                    if (string.IsNullOrEmpty(usuario.Nombre))
                    {
                        nombre = usuario.Email;
                        string[] lbl;
                        lbl = nombre.Split('@');
                        nombre = Ti.ToTitleCase(lbl[0]);
                    }
                    else
                        nombre = Ti.ToTitleCase(usuario.Nombre);
                }
                return nombre;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        //Carga la imagen de producto o usuario, dependiendo del objeto que reciba.
        public static string cargarImagen(object objeto)
        {
            try
            {
                string img;
                int id;
                if (objeto is Usuario)
                {
                    Usuario usuario = (Usuario)objeto;
                    img = usuario.UrlImagen;
                    id = usuario.Id;
                }
                else
                {
                    Producto producto = (Producto)objeto;
                    img = producto.ImagenUrl;
                    id = producto.Id;
                }
                if (Validar.imagen(img, id))
                {
                    if (!img.ToLower().Contains("http"))
                    {
                        if (img.ToLower().Contains("profile-"))
                            img = "~/Imagenes/Perfil/" + img;
                        else if (img.ToLower().Contains("product-"))
                            img = "/Imagenes/Productos/" + img;
                    }
                    return img;
                }
                else
                {
                    if (objeto is Producto)
                        return "https://i.imgur.com/yzczBvI.png";
                    else
                        return "https://i.imgur.com/9jtQAyi.png";
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        //Carga el precio reemplazando el punto(.) por coma (,)
        public static decimal cargarPrecio(string precio)
        {
            decimal price = 0;
            try
            {
                if (Validar.campo(precio))
                {
                    if (precio.Contains("."))
                        precio = precio.Replace(".", ",");
                    price = decimal.Parse(precio);
                }
                return Math.Round(price, 2, MidpointRounding.AwayFromZero);
            }
            catch (FormatException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        //Carga los datos del usuario
        public static string cargarDatosUsuario(Usuario usuario, string nombre, string apellido, string imagenPerfil, ref string status)
        {
            try
            {
                status = "error";
                string mensaje;
                if (Validar.campo(nombre))
                {
                    if (Validar.campo(apellido))
                    {
                        status = "ok";
                        DatosUsuario datos = new DatosUsuario();
                        usuario.Nombre = nombre;
                        usuario.Apellido = apellido;
                        usuario.UrlImagen = imagenPerfil;
                        datos.modificarUsuario(usuario);
                        mensaje = "Los cambios fueron guardados exitosamente!";
                    }
                    else
                        mensaje = "Debe completar el campo, Apellido";
                }
                else
                    mensaje = "Debe completar el campo, Nombre";
                return mensaje;
            }
            catch (Exception)
            {

                throw;
            }
        }
        //En caso de que haya elegido cambiar email, lo cambia
        public static string cargarEmail(Usuario usuario, string emailActual, string emailNuevo, ref string status)
        {
            try
            {
                status = "error";
                string mensaje;
                if (Validar.campoEmail(emailActual))
                {
                    if (emailActual == usuario.Email)
                    {
                        if (Validar.campoEmail(emailNuevo))
                        {
                            if (!Validar.email(emailNuevo))
                            {
                                status = "ok";
                                DatosUsuario datos = new DatosUsuario();
                                usuario.Email = emailNuevo;
                                datos.cambiarEmail(usuario.Email, usuario.Id);
                                mensaje = "El email fue guardado exitosamente!";
                            }
                            else
                                mensaje = "El nuevo email ya se encuentra registrado, ingrese otro.";
                        }
                        else
                            mensaje = "Debe completar el campo nuevo email, con un email válido.";
                    }
                    else
                        mensaje = "El email ingresado en email actual, no coincide con su cuenta, debe ingresar su email.";
                }
                else
                    mensaje = "Debe ingresar un email válido en el campo, email actual";
                return mensaje;
            }
            catch (Exception)
            {

                throw;
            }
        }
        //en caso de que haya elegido cambiar contraseña, la cambia si los campos son válidos
        public static string cargarPass(Usuario usuario, string passActual, string passNueva, string passRepetir, ref string status)
        {
            try
            {
                status = "error";
                string mensaje;
                if (Validar.campo(passActual))
                {
                    if (passActual == usuario.Pass)
                    {
                        if (Validar.campoPass(passNueva))
                        {
                            if (passNueva != usuario.Pass)
                            {
                                if (Validar.campo(passRepetir))
                                {
                                    if (passRepetir == passNueva)
                                    {
                                        status = "ok";
                                        DatosUsuario datos = new DatosUsuario();
                                        usuario.Pass = passRepetir;
                                        datos.cambiarPass(usuario.Pass, usuario.Id);
                                        mensaje = "La contraseña fue guardada exitosamente!";
                                    }
                                    else
                                        mensaje = "Las contraseñas no coinciden, intente nuevamente.";
                                }
                                else
                                    mensaje = "Debe completar el campo, repetir contraseña.";
                            }
                            else
                                mensaje = "La contraseña nueva, es la misma que tiene actualmente, intente cambiarla.";
                        }
                        else
                            mensaje = "Debe ingresar una nueva contraseña de al menos 6 caracteres, con al menos un número, una mayúscula y una minúscula.";
                    }
                    else
                        mensaje = "La contraseña no coincide con su cuenta, debe ingresar su contraseña en Contraseña Actual.";
                }
                else
                    mensaje = "Debe rellenar el campo, contraseña actual, con su contraseña.";
                return mensaje;
            }
            catch (Exception)
            {

                throw;
            }
        }
        //Codifica el email y la pass para mostrar puntos (•) en la página del perfil
        public static void codificar(ref string email, ref string pass)
        {
            try
            {
                int longitud;
                string subSPrincipal = email.Substring(0, 3);
                string subSecundario = email.Substring(3);
                longitud = subSecundario.Length;
                string auxsubSecundario = "";
                for (int i = 1; i < subSecundario.Length; i++)
                {
                    auxsubSecundario += "•";
                }
                email = subSPrincipal + auxsubSecundario;
                longitud = pass.Length;
                pass = "";
                for (int i = 0; i < longitud; i++)
                {
                    pass += "•";
                }
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
