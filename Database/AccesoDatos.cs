using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;

namespace Database
{
    public class AccesoDatos
    {
        private SqlConnection conexion;
        private SqlCommand comando;
        private SqlDataReader lector;
        public SqlDataReader Lector
        {
            get { return lector; }
        }
        public AccesoDatos()
        {
            try
            {
                conexion = new SqlConnection("server=.\\SQLEXPRESS; database=CATALOGO_WEB_DB; integrated security=true");
                comando = new SqlCommand();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void consultaSP(string consulta)
        {
            try
            {
                comando.CommandType = System.Data.CommandType.StoredProcedure;
                comando.CommandText = consulta;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void consultaEmbebida(string consulta)
        {
            try
            {
                comando.CommandType = System.Data.CommandType.Text;
                comando.CommandText = consulta;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void lectura()
        {
            try
            {
                comando.Connection = conexion;
                conexion.Open();
                lector = comando.ExecuteReader();
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        public void ejecutar()
        {
            try
            {
                comando.Connection = conexion;
                conexion.Open();
                comando.ExecuteNonQuery();
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        public int ejecutarScalar()
        {
            try
            {
                comando.Connection = conexion;
                conexion.Open();
                return (int)comando.ExecuteScalar();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void parametros(string nombre, object valor)
        {
            try
            {
                comando.Parameters.AddWithValue(nombre, valor);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void cerrarConexion()
        {
            try
            {
                if (lector != null)
                    lector.Close();
                conexion.Close();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
