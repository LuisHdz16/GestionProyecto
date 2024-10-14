using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel.DataAnnotations;
using System.IO;

namespace CRM.Pages
{
    public class AgregarPromocionModel : PageModel
    {
        [BindProperty]
        [Required(ErrorMessage = "La descripción es requerida.")]
        public string? Descripcion { get; set; }

        [BindProperty]
        [Required(ErrorMessage = "La fecha de inicio es requerida.")]
        public DateTime FechaInicio { get; set; }

        [BindProperty]
        [Required(ErrorMessage = "La fecha final es requerida.")]
        public DateTime FechaFinal { get; set; }

        public IActionResult OnPost()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            // Guardar la promoción en un archivo de texto
            string filePath = "wwwroot/promociones.txt";
            string nuevaPromocion = $"Descripcion: {Descripcion}, FechaInicio: {FechaInicio.ToShortDateString()}, FechaFinal: {FechaFinal.ToShortDateString()}\n";
            System.IO.File.AppendAllText(filePath, nuevaPromocion);

            TempData["SuccessMessage"] = "La promoción se ha agregado correctamente.";
            return RedirectToPage("VerPromociones");
        }
    }
}
