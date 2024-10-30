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
                int id = 1;

                foreach (var line in lines)
                {
                    var data = line.Split(new string[] { "Nombre: ", " Telefono: ", " Correo: " }, StringSplitOptions.RemoveEmptyEntries);
                    if (data.Length == 3)
                    {
                        Clientes.Add(new Cliente
                        {
                            Id = id++,
                            Nombre = data[0],
                            Telefono = data[1],
                            Email = data[2]
                        });
                    }
                }
            }
        }

        public IActionResult OnPostDelete(int id)
        {
            string filePath = "wwwroot/formdata.txt";
            if (System.IO.File.Exists(filePath))
            {
                var lines = System.IO.File.ReadAllLines(filePath).ToList();
                if (id >= 1 && id <= lines.Count)
                {
                    lines.RemoveAt(id - 1); // Remover la persona por ID
                    System.IO.File.WriteAllLines(filePath, lines);
                }
            }

            return RedirectToPage();
        }
    }

    public class Cliente
    {
        public int Id { get; set; }
        public string? Nombre { get; set; }
        public string? Telefono { get; set; }
        public string? Email { get; set; }
    }
}
