using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel.DataAnnotations;
using System.IO;

namespace CRM.Pages
{
    public class AgregarCursosModel : PageModel
    {
        [BindProperty]
        [Required(ErrorMessage = "Curso is required.")]
        public string? Curso { get; set; }

        [BindProperty]
        [Required(ErrorMessage = "Descripcion is required.")]
        public string? Descripcion { get; set; }
        [BindProperty]
        [Required(ErrorMessage = "Estatus is required.")]
        public string? Estatus { get; set; }

        public void OnGet()
        {
        }

        public IActionResult OnPost()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            string filePath = "wwwroot/Cursos.txt";
            string Cursos = $"Curso: {Curso} Descripcion: {Descripcion} Estatus: {Estatus}";
            System.IO.File.AppendAllTextAsync(filePath, Cursos + "\n");

            TempData["SuccessMessage"] = "El cursoa ha sido registrada con éxito.";
            return RedirectToPage();
        }
    }
}
