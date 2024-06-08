//Las siguientes lineas de codigo son directivas de importacion en c# las cuales permiten acceder a diferentes componentes y funcionalidades dentro del entorno de desarrollo de ASP.NET Core y Entity Framework Core.
using System;    //Proporciona tipos fundamentales y primitivos para .NET.
using System.Collections.Generic;    //Proporciona interfaces y clases que definen colecciones genéricas.
using System.Linq;    //Proporciona clases y extensiones para consultar datos.
using System.Threading.Tasks;    //Proporciona tipos que admiten la implementacion de tareas asincrónicas, Explicacion de asincrónicas: permite expresar la espera de acciones de larga duración sin congelar el programa durante estas acciones.
using Microsoft.AspNetCore.Http;    //Proporciona tipos para manejar solicitudes HTTP y repsuestas.
using Microsoft.AspNetCore.Mvc;    //Proporciona funcionalidades para el desarrollo de aplicaciones web utilizando el patron Modelo-Vista-Controlador en ASP.NET Core.
using Microsoft.EntityFrameworkCore;    //Proporciona acceso a datos utilizando Entity Framework Core, un ORM = Objetc-Relational Mapper para .NET.
using AplicacionCliente.Models;    //Contiene modelos especificos de la aplicacion que representan entidades de datos y sus relaciones.

//El siguiente codigo define un controlador de API para manejar las solicitudes relcionadad con los clientes en la aplicación AplicacionCliente.
namespace AplicacionCliente.Controllers    //Define un espacio de nombres para el controlador de la aplicacion que maneja las solicitudes HTTP relacionadas con los clientes.
{    //ClientesController: Es una clase que actúa como un controlador para las operaciones relacionadas con los clientes.
    [Route("api/Clientes")]    //Define la ruta base para las solicitudes dirigidas a este controlador.
    [ApiController]    //Atributo que indica que la clase es un controlador de API y permite que ASP.NET Core realice configuraciones automaticas de comportamiento.
    public class ClientesController : ControllerBase    //Hereda de ControllerBase, proporcionando funcionalidades comunes para los controladores de API.
    {    //Constructor del controlador
        private readonly ClientesContext _context;    //Declara una variable privada para contener el contexto de la base de datos utilizado para acceder a los datos de los clientes.

        public ClientesController(ClientesContext context)    //Constructor del controlador que recibe un parámetro de tipo ClientesContext, que es utilizado para inyectar el contexto de la base de datos en el controlador.
        {
            _context = context;
        }

        // GET: api/Clientes
        [HttpGet]    //Atributo que indica que el método responde a las solicitudes HTTP GET.
        public async Task<ActionResult<IEnumerable<Clientes>>> GetClientes()    //Método de acción que maneja las solicitudes GET para obtener una lista de clientes.
        {
            return await _context.Clientes.ToListAsync();    //Retorna una lista de clientes obtenida de la base de datos utilizando Entity Framework Core.
        }
            //El siguiente metodo de accion en el controlador de clientes maneja las solicitudes para obtener informacion de un cliente especidico por su ID en la API de la aplicacion AplicacionCliente.
        // GET: api/Clientes/5
        [HttpGet("{id}")]    //Atributo que indica que el método responde a las solicitudes HTTP GET con una ruta que incluye un parámetro de ID.
        public async Task<ActionResult<Clientes>> GetClientes(int id)    //Método de acción que maneja las solicitudes GET para obtener información de un cliente por su ID.
        {
            //var resultado = lista.Where(x => x.id == id)    
            // var lista    //
            var clientes = await _context.Clientes.FindAsync(id);    //Utiliza el contexto de la base de datos para buscar un cliente con el ID proporcionado en la base de datos utilizando el método FindAsync.
                                                                    //await: Espera de forma asincrónica a que se complete la operación de búsqueda en la base de datos.
            if (clientes == null)    //Verifica si el cliente no fue encontrado en la base de datos. Si es así, devuelve un resultado de tipo NotFound.
            {
                return NotFound();
            }

            return clientes; // lista.FirstorDefault(x => x.id == id);    //Retorna el cliente encontrado. En caso contrario, devuelve un resultado exitoso con la información del cliente solicitado.
        }
            //El siguiente metodo de accion en el controlador de clientes hace manejo de las solicitudes para actualizar la informacion de un cliente especifico por su id en la API de la aplicacion AplicacionCliente.
        // PUT: api/Clientes/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]    //Atributo que indica que el método responde a las solicitudes HTTP PUT con una ruta que incluye un parámetro de ID.
        public async Task<IActionResult> PutClientes(int id, Clientes clientes)    //Método de acción que maneja las solicitudes PUT para actualizar la información de un cliente por su ID. int id: Parámetro que representa el ID del cliente que se desea actualizar.
        {                                                                         //Clientes clientes: Objeto que contiene la nueva información del cliente a actualizar.

            if (id != clientes.Id)    //Verifica si el ID proporcionado en la ruta coincide con el ID del cliente proporcionado en el cuerpo de la solicitud. Si no coinciden, devuelve un resultado de tipo BadRequest.
            {
                return BadRequest();
            }

            _context.Entry(clientes).State = EntityState.Modified;    //Marca el estado del objeto clientes como modificado para que Entity Framework Core pueda rastrear los cambios en este objeto.

            try
            {
                await _context.SaveChangesAsync();    //Guarda los cambios en la base de datos de manera asincrónica.
            }
            catch (DbUpdateConcurrencyException)    //Captura excepciones de concurrencia al actualizar la base de datos. En este caso, verifica si el cliente no existe en la base de datos y devuelve un resultado de "No encontrado", de lo contrario, lanza la excepción.
            {
                if (!ClientesExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();    //Retorna un resultado exitoso sin contenido para indicar que la operación de actualización se realizó con éxito.
        }
        //Los siguientes métodos de accion en el controlador  de clientes manejan las solicitudes para crear y borrar clientes en la API de la aplicacion AplicacionCliente.
        // POST: api/Clientes
        [HttpPost]    //Atributo que indica que el método responde a las solicitudes HTTP POST.
        public async Task<ActionResult<Clientes>> PostClientes(Clientes clientes)    //Método de acción que maneja las solicitudes POST para crear un nuevo cliente. Clientes clientes: Objeto que contiene la información del nuevo cliente a crear.
        {
            _context.Clientes.Add(clientes);    //Agrega el nuevo cliente al conjunto de clientes en el contexto de la base de datos.
            await _context.SaveChangesAsync();    //uarda los cambios en la base de datos de manera asincrónica.

            return CreatedAtAction("GetClientes", new { id = clientes.Id }, clientes);    //etorna un resultado exitoso con el nuevo cliente creado y la ubicación del recurso creado en el encabezado de respuesta.
        }

        // DELETE: api/Clientes/5
        [HttpDelete("{id}")]    //Atributo que indica que el método responde a las solicitudes HTTP DELETE con una ruta que incluye un parámetro de ID.
        public async Task<IActionResult> DeleteClientes(int id)    //Método de acción que maneja las solicitudes DELETE para borrar un cliente por su ID. int id: Parámetro que representa el ID del cliente que se desea borrar.
        {

            //Cliente seleccionar = lista.FirstorDefault( x => x.id ==x);
            // seleccionar = value;
            var clientes = await _context.Clientes.FindAsync(id);    //Busca el cliente en la base de datos por su ID.
            if (clientes == null)    // Verifica si el cliente no fue encontrado en la base de datos. Si es así, devuelve un resultado de tipo NotFound.
            {
                return NotFound();
            }

            _context.Clientes.Remove(clientes);    //Elimina el cliente de la base de datos.
            await _context.SaveChangesAsync();    //Guarda los cambios en la base de datos de manera asincrónica.

            return NoContent();    //Retorna un resultado exitoso sin contenido para indicar que el cliente fue borrado con éxito.
        }

        private bool ClientesExists(int id)    //Método privado que verifica si un cliente con un ID específico existe en la base de datos. int id: Parámetro que representa el ID del cliente que se desea verificar.
        {
            return _context.Clientes.Any(e => e.Id == id);    //Utiliza LINQ para consultar la base de datos y determinar si hay algún cliente cuyo ID coincida con el ID proporcionado. Retorna true si existe al menos un cliente con el ID especificado, de lo contrario, retorna false.
        }
    }
}
