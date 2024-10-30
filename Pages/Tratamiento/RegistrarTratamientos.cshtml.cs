using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.IO;

namespace CRM.Pages
{
    public class AgregarTratamientoModel : PageModel
    {
        [BindProperty]
        public string? NombreTratamiento { get; set; }

        [BindProperty]
        public decimal PrecioTratamiento { get; set; }

        public IActionResult OnPost()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            string filePath = "wwwroot/tratamientosdata.txt";
            string tratamientoData = $"Tratamiento: {NombreTratamiento} Precio: {PrecioTratamiento}";
            System.IO.File.AppendAllTextAsync(filePath, tratamientoData + "\n");

            TempData["SuccessMessage"] = "El tratamiento ha sido registrado con éxito.";
            return RedirectToPage();
        }
    }
}
