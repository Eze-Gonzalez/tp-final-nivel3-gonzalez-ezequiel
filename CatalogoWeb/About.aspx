<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="About.aspx.cs" Inherits="CatalogoWeb.About" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>Acerca de la app</title>
    <link href="Contenido/Estilos.css" rel="stylesheet" />
    <link href="https://cdn.jsdelivr.net/npm/bootstrap@5.3.0-alpha1/dist/css/bootstrap.min.css" rel="stylesheet" integrity="sha384-GLhlTQ8iRABdZLl6O3oVMWSktQOp6b7In1Zl3/Jr59b6EGGoI1aFkw7cmDA6j6gD" crossorigin="anonymous" />
</head>
<body>
    <form id="form1" runat="server">
        <ul class="nav nav-tabs mb-4 bg-black bg-opacity-50 border-0 center-row bg-gradient" id="myTab" role="tablist">
            <li class="nav-item" role="presentation">
                <button class="nav-link active" id="home-tab" data-bs-toggle="tab" data-bs-target="#home-tab-pane" type="button" role="tab" aria-controls="home-tab-pane" aria-selected="true">Acerca de la aplicación</button>
            </li>
            <li class="nav-item" role="presentation">
                <button class="nav-link" id="details-tab" data-bs-toggle="tab" data-bs-target="#details-tab-pane" type="button" role="tab" aria-controls="details-tab-pane" aria-selected="false">Ver detalles de un producto</button>
            </li>
            <%if (Validaciones.Validar.admin(Session["usuario"]))
                { %>
            <li class="nav-item" role="presentation">
                <button class="nav-link" id="add-tab" data-bs-toggle="tab" data-bs-target="#add-tab-pane" type="button" role="tab" aria-controls="add-tab-pane" aria-selected="false">Agregar un producto</button>
            </li>
            <li class="nav-item" role="presentation">
                <button class="nav-link" id="modify-tab" data-bs-toggle="tab" data-bs-target="#modify-tab-pane" type="button" role="tab" aria-controls="modify-tab-pane" aria-selected="false">Modificar un producto</button>
            </li>
            <li class="nav-item" role="presentation">
                <button class="nav-link" id="delete-tab" data-bs-toggle="tab" data-bs-target="#delete-tab-pane" type="button" role="tab" aria-controls="delete-tab-pane" aria-selected="false">Eliminar un producto</button>
            </li>
            <%}
            %>
            <li class="nav-item">
                <a href="Default.aspx" class="nav-link">Ir al inicio</a>
            </li>
        </ul>
        <div class="tab-content container text-center text-light" id="myTabContent">
            <div class="tab-pane fade show active" id="home-tab-pane" role="tabpanel" aria-labelledby="home-tab" tabindex="0">
                <div class="card bg-black bg-opacity-25">
                    <div class="card-header">
                        <h3>Información sobre la aplicación.</h3>
                    </div>
                    <div class="card-body">
                        <blockquote class="blockquote mb-0">
                            <p>Bienvenido a la pagina de información del catalogo web, esta página explicara la funcionalidad básica de la app, para agregar, eliminar y modificar productos, a demas de ver detalles de algun producto.</p>
                            <p>Para saber cómo ver los detalles, haga click en la pestaña "Ver detalles de un producto", allí se indicara como hacer para ver los detalles del producto que desea.</p>
                            <%if (Validaciones.Validar.admin(Session["usuario"]))
                                { %>
                            <p>Para saber cómo agregar nuevos productos a la lista, haga click en la pestaña "Agregar un producto", allí se indicara como hacer para agregar un producto nuevo.</p>
                            <p>Para saber cómo modificar un producto, haga click en la pestaña "Modificar un producto", allí se indicara como hacer para poder modificar el producto que desea.</p>
                            <p>Para saber cómo eliminar un producto, haga click en la pestaña "Eliminar un producto", allí se indicara como hacer para eliminar definitivamente un producto.</p>
                            <%}  %>
                            <footer class="card-footer h6">Gracias por utilizar la app</footer>
                        </blockquote>
                    </div>
                </div>
            </div>
            <div class="tab-pane fade" id="details-tab-pane" role="tabpanel" aria-labelledby="details-tab" tabindex="0">
                <div class="card bg-black bg-opacity-25">
                    <div class="card-header">
                        <h3>Cómo ver los detalles de un producto.</h3>
                    </div>
                    <div class="card-body">
                        <blockquote class="blockquote mb-0">
                            <p>Para ver los detalles de un producto, puede hacer click en el boton "Ver detalles" que aparece justo debajo de cada producto en la página de inicio.</p>
                            <p>Tenga en cuenta que si no inició sesión, no podra agregar ese producto a su lista de favoritos, sin embargo, podrá ver los detalles sin ningun inconveniente.</p>
                            <%if (Validaciones.Validar.admin(Session["usuario"]))
                                { %>
                            <p>Si inició sesión con una cuenta de administrador, podrá dirigirse al boton "Lista de productos" del menú de inicio, sera redirigido a la página donde podrá ver la lista de productos disponibles, cada uno de los productos, al final de cada fila, encontrará un icono de lupa (🔎), haga click en ella para ver los detalles del producto de esa fila.</p>
                            <%}  %>
                            <footer class="card-footer h6">Gracias por utilizar la app</footer>
                        </blockquote>
                    </div>
                </div>
            </div>
            <div class="tab-pane fade" id="add-tab-pane" role="tabpanel" aria-labelledby="add-tab" tabindex="0">
                <div class="card bg-black bg-opacity-25">
                    <div class="card-header">
                        <h3>Cómo agregar un producto a la lista.</h3>
                    </div>
                    <div class="card-body">
                        <blockquote class="blockquote mb-0">
                            <p>Para agregar un producto a la lista, primero debe iniciar sesión y tener una cuenta de administrador, de lo contrario, no podrá acceder a la página para agregar nuevos productos.</p>
                            <p>Si ya inició sesión y con una cuenta de administrador, en el menú principal, podrá ver un boton que dice "Agregar un producto", si hace click allí, será redirigido a una nueva ventana que le permitirá introducir los datos necesarios para agregar un nuevo producto a la lista.</p>
                            <p>Así mismo, en el menú principal verá un botón que dice "Lista de productos", si hace click allí, será redirigido a la lista donde podrá ver todos los productos agregados, justo debajo encontrará un boton que dice "Agregar", si hace click allí será redirigido a una nueva ventana donde podrá rellenar los datos para agregar un nuevo producto a la lista.</p>
                            <p>Si desea cancelar la adición del producto, presione el botón "Cancelar" que hay abajo del formulario para ser redirigido a la lista de productos sin agregar uno nuevo.</p>
                            <footer class="card-footer h6">Gracias por utilizar la app</footer>
                        </blockquote>
                    </div>
                </div>
            </div>
            <div class="tab-pane fade" id="modify-tab-pane" role="tabpanel" aria-labelledby="modify-tab" tabindex="0">
                <div class="card bg-black bg-opacity-25">
                    <div class="card-header">
                        <h3>Cómo modificar un producto.</h3>
                    </div>
                    <div class="card-body">
                        <blockquote class="blockquote mb-0">
                            <p>Para Modificar un producto, primero debe iniciar sesión con una cuenta administrador, de lo contrario, no le aparecerán las opciones para modificar el producto.</p>
                            <p>Si ya inició sesión con una cuenta administrador, deberá ir a ver los detalles del producto, dentro de la nueva página, notará que hay 3 botones en la parte superior de los detalles, "Ver listado", "Modificar" y "Eliminar", si hace click en el segundo, será redirigido a una página, donde podrá ver los datos que tiene el producto seleccionado, allí podra modificar los campos que necesite y al presionar el boton aceptar, los nuevos datos serán guardados y sera redirigido a la pantalla de los detalles, para que pueda ver cada uno de los detalles del producto que modificó.</p>
                            <p>Si desea cancelar la modificación del producto, en la pantalla verá un boton que dice "Cancelar" haga click en dicho botón para ser redirigido a la ventana de detalles nuevamente</p>
                            <footer class="card-footer h6">Gracias por utilizar la app</footer>
                        </blockquote>
                    </div>
                </div>
            </div>
            <div class="tab-pane fade" id="delete-tab-pane" role="tabpanel" aria-labelledby="delete-tab" tabindex="0">
                <div class="card bg-black bg-opacity-25">
                    <div class="card-header">
                        <h3>Cómo eliminar un producto.</h3>
                    </div>
                    <div class="card-body">
                        <blockquote class="blockquote mb-0">
                            <p>Para eliminar un producto, primero debe iniciar sesión con una cuenta administrador, de lo contrario, no le aparecerán las opciones para eliminar el producto.</p>
                            <p>Si ya inició sesión con una cuenta administrador, deberá ir a ver los detalles del producto, dentro de la nueva página, notará que hay 3 botones en la parte superior de los detalles, "Ver listado", "Modificar" y "Eliminar", si hace click en este último, se mostrarán nuevos elementos en pantalla preguntándole si desea realizar la eliminación del producto, para proceder, deberá hacer click en el boton "Confirmar", si lo hace, eliminará el producto y será redirigido a la lista de productos, si hace click en el boton "Cancelar" la eliminación NO se llevará a cabo y desaparecerán los elementos de confirmación y volverá a ver los botones iniciales.</p>
                            <footer class="card-footer h6">Gracias por utilizar la app</footer>
                        </blockquote>
                    </div>
                </div>
            </div>
        </div>
    </form>
</body>
<script src="https://cdn.jsdelivr.net/npm/bootstrap@5.3.0-alpha1/dist/js/bootstrap.bundle.min.js" integrity="sha384-w76AqPfDkMBDXo30jS1Sgez6pr3x5MlQ1ZAGC+nuZB+EYdgRZgiwxhTBTkF7CXvN" crossorigin="anonymous"></script>
</html>
