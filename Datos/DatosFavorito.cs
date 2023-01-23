using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Database;
using ModeloDominio;

namespace Datos
{
    public static class DatosFavorito
    {
        public static List<Favorito> listar(int idUser)
        {
            AccesoDatos datos = new AccesoDatos();
            DatosProducto prods = new DatosProducto();
            List<Favorito> lista = new List<Favorito>();
            try
            {
                datos.consultaSP("listarFav");
                datos.parametros("@id", idUser);
                datos.lectura();
                while (datos.Lector.Read())
                {
                    Favorito fav = new Favorito();
                    fav.Id = (int)datos.Lector["Id"];
                    fav.Producto = new Producto();
                    fav.Usuario = new Usuario();
                    fav.Producto.Id = (int)datos.Lector["idArticulo"];
                    fav.Usuario.Id = (int)datos.Lector["idUser"];
                    lista.Add(fav);

                }
                return lista;
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
        public static void agregarFav(Favorito fav)
        {
            AccesoDatos datos = new AccesoDatos();
            try
            {
                datos.consultaSP("agregarFav");
                datos.parametros("@idUser", fav.Usuario.Id);
                datos.parametros("@idProd", fav.Producto.Id);
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
        public static void eliminarFav(int idProd, int idUser)
        {
            AccesoDatos datos = new AccesoDatos();
            try
            {
                datos.consultaSP("eliminarFav");
                datos.parametros("@idprod", idProd);
                datos.parametros("@iduser", idUser);
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
