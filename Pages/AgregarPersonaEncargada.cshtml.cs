using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.IO;

namespace CRM.Pages
{
    public class AgregarPersonaEncargadaModel : PageModel
    {
        [BindProperty]
        public string? Nombre { get; set; }

        [BindProperty]
        public string? Puesto { get; set; }

        public IActionResult OnPost()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            string filePath = "wwwroot/personasEncargadas.txt";
            string personaEncargadaData = $"Nombre: {Nombre} Puesto: {Puesto}";
            System.IO.File.AppendAllTextAsync(filePath, personaEncargadaData + "\n");

            TempData["SuccessMessage"] = "La persona encargada ha sido registrada con éxito.";
            return RedirectToPage();
        }
    }
}
