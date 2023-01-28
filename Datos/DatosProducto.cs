using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using Database;
using ModeloDominio;

namespace Datos
{
    public class DatosProducto
    {
        public List<Producto> listar()
        {
            AccesoDatos datos = new AccesoDatos();
            List<Producto> lista = new List<Producto>();
            try
            {
                datos.consultaEmbebida("select A.Id Id, Codigo, Nombre, A.Descripcion Descripcion, IdMarca, IdCategoria, ImagenUrl, Precio, M.Descripcion Marca, C.Descripcion Categoria  from ARTICULOS A, MARCAS M, CATEGORIAS C where M.Id = IdMarca and C.Id = IdCategoria");
                datos.lectura();
                while (datos.Lector.Read())
                {
                    Producto producto = new Producto();
                    //public int Id { get; set; }
                    //public string Codigo { get; set; }
                    //public string Nombre { get; set; }
                    //public string Descripcion { get; set; }
                    //public string UrlImagen { get; set; }
                    //public decimal Precio { get; set; }
                    //public Marca Marca { get; set; }
                    //public Categoria Categoria { get; set; }
                    producto.Marca = new Marca();
                    producto.Categoria = new Categoria();
                    producto.Id = (int)datos.Lector["Id"];
                    producto.Codigo = (string)datos.Lector["Codigo"];
                    producto.Nombre = (string)datos.Lector["Nombre"];
                    producto.Descripcion = (string)datos.Lector["Descripcion"];
                    if (!(datos.Lector["ImagenUrl"] is DBNull))
                        producto.ImagenUrl = (string)datos.Lector["ImagenUrl"];
                    producto.Precio = Math.Round((decimal)datos.Lector["Precio"], 2, MidpointRounding.AwayFromZero);
                    producto.Marca.Id = (int)datos.Lector["IdMarca"];
                    producto.Marca.Descripcion = (string)datos.Lector["Marca"];
                    producto.Categoria.Id = (int)datos.Lector["IdCategoria"];
                    producto.Categoria.Descripcion = (string)datos.Lector["Categoria"];
                    lista.Add(producto);
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
        public Producto traerProducto(int id)
        {
            AccesoDatos datos = new AccesoDatos();
            try
            {
                Producto producto = new Producto();
                datos.consultaEmbebida("select A.Id Id, Codigo, Nombre, A.Descripcion Descripcion, IdMarca, IdCategoria, ImagenUrl, Precio, M.Descripcion Marca, C.Descripcion Categoria  from ARTICULOS A, MARCAS M, CATEGORIAS C where M.Id = IdMarca and C.Id = IdCategoria and A.Id = @id");
                datos.parametros("@id", id);
                datos.lectura();
                if (datos.Lector.Read())
                {
                    producto.Marca = new Marca();
                    producto.Categoria = new Categoria();
                    producto.Id = (int)datos.Lector["Id"];
                    producto.Codigo = (string)datos.Lector["Codigo"];
                    producto.Nombre = (string)datos.Lector["Nombre"];
                    producto.Descripcion = (string)datos.Lector["Descripcion"];
                    if (!(datos.Lector["ImagenUrl"] is DBNull))
                        producto.ImagenUrl = (string)datos.Lector["ImagenUrl"];
                    producto.Precio = Math.Round((decimal)datos.Lector["Precio"], 2, MidpointRounding.AwayFromZero);
                    producto.Marca.Id = (int)datos.Lector["IdMarca"];
                    producto.Marca.Descripcion = (string)datos.Lector["Marca"];
                    producto.Categoria.Id = (int)datos.Lector["IdCategoria"];
                    producto.Categoria.Descripcion = (string)datos.Lector["Categoria"];
                }
                return producto;
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
        public static void agregar(Producto producto, bool nuevo = true)
        {
            AccesoDatos datos = new AccesoDatos();
            try
            {
                if (nuevo)
                {
                    datos.consultaEmbebida("Insert into ARTICULOS values (@codigo, @nombre, @descripcion, @idMarca, @idCategoria, @img, @precio)");
                    //@codigo varchar(50),
                    //@nombre varchar(50),
                    //@descripcion varchar(150),
                    //@idMarca int,
                    //@idCategoria int,
                    //@img varchar(1000),
                    //@precio money,
                    //@id int
                    datos.parametros("@codigo", producto.Codigo);
                    datos.parametros("@nombre", producto.Nombre);
                    datos.parametros("@descripcion", producto.Descripcion);
                    datos.parametros("@idMarca", producto.Marca.Id);
                    datos.parametros("@idCategoria", producto.Categoria.Id);
                    datos.parametros("@img", string.IsNullOrEmpty(producto.ImagenUrl) ? (object)DBNull.Value : producto.ImagenUrl);
                    datos.parametros("@precio", producto.Precio);
                    datos.ejecutar();
                }
                else
                {
                    datos.consultaEmbebida("update ARTICULOS set Codigo = @codigo, Nombre = @nombre, Descripcion = @descripcion, IdMarca = @idMarca, IdCategoria = @idCategoria, ImagenUrl = @img, Precio = @precio where Id = @id");
                    datos.parametros("@codigo", producto.Codigo);
                    datos.parametros("@nombre", producto.Nombre);
                    datos.parametros("@descripcion", producto.Descripcion);
                    datos.parametros("@idMarca", producto.Marca.Id);
                    datos.parametros("@idCategoria", producto.Categoria.Id);
                    datos.parametros("@img", string.IsNullOrEmpty(producto.ImagenUrl) ? (object)DBNull.Value : producto.ImagenUrl);
                    datos.parametros("@precio", producto.Precio);
                    datos.parametros("@id", producto.Id);
                    datos.ejecutar();
                }
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
        public static void eliminar(int id)
        {
            AccesoDatos datos = new AccesoDatos();
            try
            {
                datos.consultaEmbebida("Delete Articulos where Id = @id");
                datos.parametros("@id", id);
                datos.ejecutar();
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
        public List<Producto> filtroAvanzado(string tipo, string criterio, string filtro, ref bool status)
        {
            AccesoDatos datos = new AccesoDatos();
            try
            {
                List<Producto> lista = listar();
                List<Producto> filtrada = new List<Producto>();
                if (tipo == "Precio")
                {
                    if (filtro.Contains("."))
                    {
                        filtro = filtro.Replace(".", ",");
                    }
                    decimal precio = Math.Round(decimal.Parse(filtro), 2, MidpointRounding.AwayFromZero);
                    switch (criterio)
                    {
                        case "Igual a":
                            filtrada = lista.FindAll(p => p.Precio == precio);
                            break;
                        case "Menor a":
                            filtrada = lista.FindAll(p => p.Precio < precio);
                            break;
                        case "Mayor a":
                            filtrada = lista.FindAll(p => p.Precio > precio);
                            break;
                    }
                }
                else
                {
                    string consulta = "select A.Id Id, Codigo, Nombre, A.Descripcion Descripcion, IdMarca, IdCategoria, ImagenUrl, Precio, M.Descripcion Marca, C.Descripcion Categoria from ARTICULOS A, Marcas M, Categorias C where M.Id = IdMarca and C.Id = IdCategoria and ";
                    switch (tipo)
                    {
                        case "Codigo":
                            switch (criterio)
                            {
                                case "Comienza con":
                                    consulta += "Codigo like '" + filtro + "%'";
                                    break;
                                case "Termina con":
                                    consulta += "Codigo like '%" + filtro + "'";
                                    break;
                                case "Contiene":
                                    consulta += "Codigo like '%" + filtro + "%'";
                                    break;
                            }
                            break;
                        case "Nombre":
                            switch (criterio)
                            {
                                case "Comienza con":
                                    consulta += "Nombre like '" + filtro + "%'";
                                    break;
                                case "Termina con":
                                    consulta += "Nombre like '%" + filtro + "'";
                                    break;
                                case "Contiene":
                                    consulta += "Nombre like '%" + filtro + "%'";
                                    break;
                            }
                            break;
                        case "Categoría":
                            switch (criterio)
                            {
                                case "Comienza con":
                                    consulta += "C.Descripcion like '" + filtro + "%'";
                                    break;
                                case "Termina con":
                                    consulta += "C.Descripcion like '%" + filtro + "'";
                                    break;
                                case "Contiene":
                                    consulta += "C.Descripcion like '%" + filtro + "%'";
                                    break;
                            }
                            break;
                        case "Marca":
                            switch (criterio)
                            {
                                case "Comienza con":
                                    consulta += "M.Descripcion like '" + filtro + "%'";
                                    break;
                                case "Termina con":
                                    consulta += "M.Descripcion like '%" + filtro + "'";
                                    break;
                                case "Contiene":
                                    consulta += "M.Descripcion like '%" + filtro + "%'";
                                    break;
                            }
                            break;
                    }
                    datos.consultaEmbebida(consulta);
                    datos.lectura();
                    while (datos.Lector.Read())
                    {
                        Producto producto = new Producto();
                        producto.Marca = new Marca();
                        producto.Categoria = new Categoria();
                        producto.Id = (int)datos.Lector["Id"];
                        producto.Codigo = (string)datos.Lector["Codigo"];
                        producto.Nombre = (string)datos.Lector["Nombre"];
                        producto.Descripcion = (string)datos.Lector["Descripcion"];
                        if (!(datos.Lector["ImagenUrl"] is DBNull))
                            producto.ImagenUrl = (string)datos.Lector["ImagenUrl"];
                        producto.Precio = Math.Round((decimal)datos.Lector["Precio"], 2, MidpointRounding.AwayFromZero);
                        producto.Marca.Id = (int)datos.Lector["IdMarca"];
                        producto.Marca.Descripcion = (string)datos.Lector["Marca"];
                        producto.Categoria.Id = (int)datos.Lector["IdCategoria"];
                        producto.Categoria.Descripcion = (string)datos.Lector["Categoria"];
                        filtrada.Add(producto);
                    }
                }
                if (filtrada.Count == 0)
                {
                    status = false;
                    filtrada = lista;
                }
                return filtrada;

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
        public List<Producto> filtroRapido(string tipo, ref bool status, string filtro = "", int precioMin = 0, int precioMax = 0)
        {
            filtro = filtro.ToLower();
            List<Producto> lista = listar();
            List<Producto> filtrada = new List<Producto>();
            try
            {
                switch (tipo)
                {
                    case "Nombre":
                        filtrada = lista.FindAll(p => p.Nombre.ToLower().Contains(filtro));
                        if (filtrada.Count == 0)
                        {
                            status = false;
                            filtrada = lista;
                        }
                            
                        break;
                    case "Precio":
                        filtrada = lista.FindAll(p => p.Precio >= precioMin && p.Precio <= precioMax);
                        if (filtrada.Count == 0)
                        {
                            status = false;
                            filtrada = lista;
                        }
                        break;
                    case "Categoría":
                        filtrada = lista.FindAll(p => p.Categoria.Descripcion.ToLower().Contains(filtro));
                        if(filtrada.Count == 0)
                        {
                            status = false;
                            filtrada = lista;
                        }
                        break;
                    case "Marca":
                        filtrada = lista.FindAll(p => p.Marca.Descripcion.ToLower().Contains(filtro));
                        if(filtrada.Count == 0)
                        {
                            status = false;
                            filtrada = lista;
                        }
                        break;
                }
                return filtrada;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        public List<Producto> filtroRapido(string filtro, ref bool status)
        {
            filtro = filtro.ToLower();
            List<Producto> lista = listar();
            List<Producto> filtrada = new List<Producto>();
            try
            {
                if (filtro.Contains("."))
                    filtro = filtro.Replace(".", ",");
                filtrada = lista.FindAll(p => p.Nombre.ToLower().Contains(filtro) || p.Codigo.ToLower().Contains(filtro) || p.Categoria.Descripcion.ToLower().Contains(filtro) || p.Marca.Descripcion.ToLower().Contains(filtro) || p.Precio.ToString().Contains(filtro));
                if (filtrada.Count == 0)
                {
                    status = false;
                    filtrada = lista;
                }
                return filtrada;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        public int ultimoId()
        {
            try
            {
                List<Producto> lista = listar();
                Producto producto = lista[lista.Count - 1];
                return producto.Id;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
    }
}
