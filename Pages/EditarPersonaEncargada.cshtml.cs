using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.IO;
using System.Linq;

namespace CRM.Pages
{
    public class EditarPersonaEncargadaModel : PageModel
    {
        [BindProperty]
        public string? Nombre { get; set; }

        [BindProperty]
        public string? Puesto { get; set; }

        public int Id { get; set; }

        public void OnGet(int id)
        {
            Id = id;
            string filePath = "wwwroot/personasEncargadas.txt";
            if (System.IO.File.Exists(filePath))
            {
                var lines = System.IO.File.ReadAllLines(filePath).ToList();
                if (id >= 1 && id <= lines.Count)
                {
                    var data = lines[id - 1].Split(new string[] { "Nombre: ", " Puesto: " }, System.StringSplitOptions.RemoveEmptyEntries);
                    if (data.Length == 2)
                    {
                        Nombre = data[0];
                        Puesto = data[1];
                    }
                }
            }
        }

        public IActionResult OnPost(int id)
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            string filePath = "wwwroot/personasEncargadas.txt";
            if (System.IO.File.Exists(filePath))
            {
                var lines = System.IO.File.ReadAllLines(filePath).ToList();
                if (id >= 1 && id <= lines.Count)
                {
                    lines[id - 1] = $"Nombre: {Nombre} Puesto: {Puesto}";
                    System.IO.File.WriteAllLines(filePath, lines);
                }
            }

            return RedirectToPage("VerPersonasEncargadas");
        }
    }
}
