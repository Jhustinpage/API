//La siguiente clase define el modelo de datos para representar a un cliente en la aplicación AplicacionCliente.
namespace AplicacionCliente.Models
{
    public class Clientes    //Clase que representa el modelo de datos para un cliente en la aplicación.
    {
        public int Id { get; set; }    //Propiedad que representa el identificador único del cliente.
        public string? RazonSocial { get; set; }    //Propiedad que representa la razón social del cliente.
        public string Direccion {get; set; }    //Propiedad que representa la dirección del cliente.
        public string? Rut { get; set; }    //Propiedad que representa el RUT del cliente, opcional.
    }
}
