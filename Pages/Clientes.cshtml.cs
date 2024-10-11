using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Collections.Generic;
using System.IO;

namespace CRM.Pages
{
    public class ClientesModel : PageModel
    {
        public List<Cliente> Clientes { get; set; } = new List<Cliente>();

        public void OnGet()
        {
            string filePath = "wwwroot/formdata.txt";

            if (System.IO.File.Exists(filePath))
            {
                var lines = System.IO.File.ReadAllLines(filePath);

                foreach (var line in lines)
                {
                    var data = line.Split(new string[] { "Nombre: ", " Telefono: ", " Correo: " }, StringSplitOptions.RemoveEmptyEntries);
                    if (data.Length == 3)
                    {
                        var nombres = data[0].Split(' ');
                        Clientes.Add(new Cliente
                        {
                            Nombre = nombres[0],
                            Apellido = nombres[1],
                            Telefono = data[1],
                            Email = data[2]
                        });
                    }
                }
            }
        }
    }

    public class Cliente
    {
        public string? Nombre { get; set; }
        public string? Apellido { get; set; }
        public string? Telefono { get; set; }
        public string? Email { get; set; }
    }
}
