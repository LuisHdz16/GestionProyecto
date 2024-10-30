using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel.DataAnnotations;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace CRM.Pages.Cursos
{
    public class EditarCursoModel : PageModel
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
        public int Id { get; set; }
        public void OnGet(int id)
        {
            Id = id;
            string filePath = "wwwroot/Cursos.txt";
            if (System.IO.File.Exists(filePath))
            {
                var lines = System.IO.File.ReadAllLines(filePath).ToList();
                if (id >= 1 && id <= lines.Count)
                {
                    var data = lines[id - 1].Split(new string[] { "Curso: ", " Descripcion: ", " Estatus: " }, System.StringSplitOptions.RemoveEmptyEntries);
                    if (data.Length == 3)
                    {
                        Curso = data[0];
                        Descripcion = data[1];
                        Estatus = data[2];
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

            string filePath = "wwwroot/Cursos.txt";
            if (System.IO.File.Exists(filePath))
            {
                var lines = System.IO.File.ReadAllLines(filePath).ToList();
                if (id >= 1 && id <= lines.Count)
                {
                    lines[id - 1] = $"Curso: {Curso} Descripcion: {Descripcion} Estatus: {Estatus}";
                    System.IO.File.WriteAllLines(filePath, lines);
                }
            }

            return RedirectToPage("VerCursos");
        }
    }
}
