using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Database;
using ModeloDominio;

namespace Datos
{
    public class DatosCategoria
    {
        public List<Categoria> listar()
        {
            AccesoDatos datos = new AccesoDatos();
            List<Categoria> lista = new List<Categoria>();
            try
            {
                datos.consultaEmbebida("select id, descripcion from Categorias");
                datos.lectura();
                while (datos.Lector.Read())
                {
                    Categoria categoria = new Categoria();
                    categoria.Id = (int)datos.Lector["Id"];
                    categoria.Descripcion = (string)datos.Lector["Descripcion"];
                    lista.Add(categoria);
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
