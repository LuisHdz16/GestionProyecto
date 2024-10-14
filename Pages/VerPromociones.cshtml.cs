using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace CRM.Pages
{
    public class VerPromocionesModel : PageModel
    {
        public List<Promocion> Promociones { get; set; } = new List<Promocion>();

        public void OnGet()
        {
            string filePath = "wwwroot/promociones.txt";

            if (System.IO.File.Exists(filePath))
            {
                var lines = System.IO.File.ReadAllLines(filePath);

                foreach (var line in lines)
                {
                    var data = line.Split(new string[] { "Descripcion: ", ", FechaInicio: ", ", FechaFinal: " }, StringSplitOptions.RemoveEmptyEntries);
                    if (data.Length == 3)
                    {
                        Promociones.Add(new Promocion
                        {
                            Id = Promociones.Count + 1,
                            Descripcion = data[0],
                            FechaInicio = DateTime.Parse(data[1]),
                            FechaFinal = DateTime.Parse(data[2])
                        });
                    }
                }
            }
        }

        public IActionResult OnPostEliminar(int id)
        {
            string filePath = "wwwroot/promociones.txt";
            if (System.IO.File.Exists(filePath))
            {
                var lines = System.IO.File.ReadAllLines(filePath).ToList();
                if (id >= 0 && id < lines.Count)
                {
                    lines.RemoveAt(id);
                    System.IO.File.WriteAllLines(filePath, lines);
                }
            }
            return RedirectToPage();
        }
    }

    public class Promocion
    {
        public int Id { get; set; }
        public string? Descripcion { get; set; }
        public DateTime FechaInicio { get; set; }
        public DateTime FechaFinal { get; set; }
    }
}
