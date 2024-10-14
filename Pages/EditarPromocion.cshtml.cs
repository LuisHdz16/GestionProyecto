using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace CRM.Pages
{
    public class EditarPromocionModel : PageModel
    {
        [BindProperty]
        public string? Descripcion { get; set; }

        [BindProperty]
        public DateTime FechaInicio { get; set; }

        [BindProperty]
        public DateTime FechaFinal { get; set; }

        public void OnGet(int id)
        {
            string filePath = "wwwroot/promociones.txt";
            if (System.IO.File.Exists(filePath))
            {
                var lines = System.IO.File.ReadAllLines(filePath).ToList();
                if (id >= 0 && id < lines.Count)
                {
                    var data = lines[id].Split(new string[] { "Descripcion: ", ", FechaInicio: ", ", FechaFinal: " }, StringSplitOptions.RemoveEmptyEntries);
                    if (data.Length == 3)
                    {
                        Descripcion = data[0];
                        FechaInicio = DateTime.Parse(data[1]);
                        FechaFinal = DateTime.Parse(data[2]);
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

            string filePath = "wwwroot/promociones.txt";
            if (System.IO.File.Exists(filePath))
            {
                var lines = System.IO.File.ReadAllLines(filePath).ToList();
                if (id >= 0 && id < lines.Count)
                {
                    // Actualizar la línea correspondiente a la promoción
                    lines[id] = $"Descripcion: {Descripcion}, FechaInicio: {FechaInicio.ToShortDateString()}, FechaFinal: {FechaFinal.ToShortDateString()}";
                    System.IO.File.WriteAllLines(filePath, lines);
                }
            }

            // Redirigir de nuevo a la página de ver promociones
            return RedirectToPage("VerPromociones");
        }
    }
}
