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
        public List<PromocionVer> Promociones { get; set; } = new List<PromocionVer>();

        public void OnGet()
        {
            string filePath = "wwwroot/promociones.txt";

            if (System.IO.File.Exists(filePath))
            {
                var lines = System.IO.File.ReadAllLines(filePath);
                int id = 1;
                foreach (var line in lines)
                {
                    var data = line.Split(new[] { "Tratamiento: ", " Descripcion: ", " PrecioNuevo: ", " Estatus: " }, StringSplitOptions.RemoveEmptyEntries);
                    if (data.Length == 4)
                    {
                        Promociones.Add(new PromocionVer
                        {
                            Id = id++,
                            Tratamiento = data[0],
                            Descripcion = data[1],
                            PrecioNuevo = data[2],
                            Estatus = data[3]
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
                if (id >= 1 && id <= lines.Count)
                {
                    lines.RemoveAt(id - 1); // Remover la persona por ID
                    System.IO.File.WriteAllLines(filePath, lines);
                }
            }
            return RedirectToPage();
        }
    }

    public class PromocionVer
    {
        public int Id { get; set; }
        public string? Tratamiento { get; set; }
        public string? PrecioNuevo { get; set; }
        public string? Descripcion { get; set; }
        public string? Estatus { get; set; }
    }
}
