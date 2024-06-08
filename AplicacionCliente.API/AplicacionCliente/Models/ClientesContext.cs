//La siguiente clase define el contexto de la base de datos para la gestión de clientes en la aplicación AplicacionCliente.
using Microsoft.EntityFrameworkCore;    //Esta directiva de uso permite acceder a las clases y funcionalidades proporcionadas por la biblioteca Microsoft.EntityFrameworkCore, que es un framework de mapeo objeto-relacional (ORM) utilizado para interactuar con bases de datos en aplicaciones .NET.

namespace AplicacionCliente.Models;    //El espacio de nombres Models dentro de AplicacionCliente se utiliza para agrupar todos los modelos de datos relacionados con la aplicación.

public class ClientesContext : DbContext    //Clase que representa el contexto de la base de datos para la gestión de clientes en la aplicación.
{
    public ClientesContext(DbContextOptions<ClientesContext> options) : base(options) {    //Constructor de la clase que recibe las opciones de configuración del contexto de la base de datos.
    }

    public DbSet<Clientes> Clientes {get; set;} = null!;    //Propiedad que representa una colección de clientes en la base de datos, que se mapea a la tabla de clientes en la base de datos.
}

