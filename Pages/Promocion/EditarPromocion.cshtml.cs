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
        public string? Tratamiento { get; set; }

        [BindProperty]
        public string? Descripcion { get; set; }

        [BindProperty]
        public decimal PrecioNuevo { get; set; }

        [BindProperty]
        public string? Estatus { get; set; }
        public int Id { get; set; }
        public List<string> Tratamientos_ { get; set; } = new List<string>();

        string filePathTratamientos = "wwwroot/tratamientosdata.txt";




        public void OnGet(int id)
        {
            Id = id;

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
            string filePath = "wwwroot/promociones.txt";
            if (System.IO.File.Exists(filePath))
            {
                var lines = System.IO.File.ReadAllLines(filePath).ToList();
                if (id >= 1 && id <= lines.Count)
                {
                    var data = lines[id - 1].Split(new[] { "Tratamiento: ", ", Descripcion: ", ", PrecioNuevo: ", ", Estatus: " }, StringSplitOptions.RemoveEmptyEntries);
                    if (data.Length == 4)
                    {
                        Tratamiento = data[0];
                        Descripcion = data[1];
                        PrecioNuevo = decimal.Parse(data[2]);
                        Estatus = data[3];
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
                if (id >= 1 && id <= lines.Count)
                {
                    // Actualizar la línea correspondiente a la promoción
                    lines[id - 1] = $"Tratamiento: {Tratamiento}, Descripcion: {Descripcion}, PrecioNuevo: {PrecioNuevo}, Estatus: {Estatus}";
                    System.IO.File.WriteAllLines(filePath, lines);
                }
            }

            return RedirectToPage("VerPromociones");
        }
    }
}
