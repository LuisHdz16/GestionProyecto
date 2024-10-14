using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace CRM.Pages
{
    public class VerPersonasEncargadasModel : PageModel
    {
        public List<PersonaEncargada> PersonasEncargadas { get; set; } = new List<PersonaEncargada>();

        public void OnGet()
        {
            // Leer las personas encargadas desde el archivo
            string filePath = "wwwroot/personasEncargadas.txt";
            if (System.IO.File.Exists(filePath))
            {
                var lines = System.IO.File.ReadAllLines(filePath);
                int id = 1;
                foreach (var line in lines)
                {
                    var data = line.Split(new string[] { "Nombre: ", " Puesto: " }, System.StringSplitOptions.RemoveEmptyEntries);
                    if (data.Length == 2)
                    {
                        PersonasEncargadas.Add(new PersonaEncargada
                        {
                            Id = id++,
                            Nombre = data[0],
                            Puesto = data[1]
                        });
                    }
                }
            }
        }

        public IActionResult OnPostDelete(int id)
        {
            string filePath = "wwwroot/personasEncargadas.txt";
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

    public class PersonaEncargada
    {
        public int Id { get; set; }
        public string? Nombre { get; set; }
        public string? Puesto { get; set; }
    }
}
