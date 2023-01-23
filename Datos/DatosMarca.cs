using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Database;
using ModeloDominio;

namespace Datos
{
    public class DatosMarca
    {
        public List<Marca> listar()
        {
            AccesoDatos datos = new AccesoDatos();
            List<Marca> lista = new List<Marca>();
            try
            {
                datos.consultaEmbebida("select id, descripcion from MARCAS");
                datos.lectura();
                while (datos.Lector.Read())
                {
                    Marca marca = new Marca();
                    marca.Id = (int)datos.Lector["Id"];
                    marca.Descripcion = (string)datos.Lector["Descripcion"];
                    lista.Add(marca);
                }
                return lista;
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
    }
}
