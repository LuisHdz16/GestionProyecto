using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel.DataAnnotations;
using System.IO;
using static CRM.Pages.VerTratamientosModel;

namespace CRM.Pages
{
    public class AgregarPromocionModel : PageModel
    {
        [BindProperty]
        [Required]
        public string? Tratamiento { get; set; }

        [BindProperty]
        [Required]
        public string? Descripcion { get; set; }

        [BindProperty]
        [Required]
        public decimal PrecioNuevo { get; set; }

        [BindProperty]
        [Required]
        public string? Estatus { get; set; }

        public List<string> Tratamientos_ { get; set; } = new List<string>();

        string filePathTratamientos = "wwwroot/tratamientosdata.txt";

        public void OnGet()
        {
            if (System.IO.File.Exists(filePathTratamientos))
            {
                var lines_ = System.IO.File.ReadAllLines(filePathTratamientos);

                foreach (var line in lines_)
                {
                    var data = line.Split(new string[] { "Tratamiento: ", " Precio: " }, StringSplitOptions.RemoveEmptyEntries);
                    if (data.Length == 2)
                    {
                        Tratamientos_.Add($"{data[0]}");
                    }
                }
            }
        }

        public IActionResult OnPost()
        {
            
            if (!ModelState.IsValid)
            {
                return Page();
            }

            string filePath = "wwwroot/promociones.txt";
            string nuevaPromocion = $"Tratamiento: {Tratamiento} Descripcion: {Descripcion} PrecioNuevo: {PrecioNuevo} Estatus: {Estatus}\n";
            System.IO.File.AppendAllText(filePath, nuevaPromocion);

            TempData["SuccessMessage"] = "La promoción se ha agregado correctamente.";
            return RedirectToPage("VerPromociones");
        }
    }
}
