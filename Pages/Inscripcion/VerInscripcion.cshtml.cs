using CRM.Pages;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace CRM.Pages
{
    public class VerIncripcionModel : PageModel
    {
        public List<VerInscrpcion> InscripcionRegistrados { get; set; } = new List<VerInscrpcion>();

        public void OnGet()
        {
            string filePath = "wwwroot/InscripcionCursos.txt";
            if (System.IO.File.Exists(filePath))
            {
                var lines = System.IO.File.ReadAllLines(filePath);
                int id = 1;
                foreach (var line in lines)
                {
                    var data = line.Split(new string[] { "Cliente: ", "Curso: ", "FechaInicio: ", "FechaFin: ", "Duracion: ", "Precio: ", "Estatus: " }, System.StringSplitOptions.RemoveEmptyEntries);
                    if (data.Length == 7)
                    {
                        InscripcionRegistrados.Add(new VerInscrpcion
                        {
                            Id = id++,
                            Cliente = data[0],
                            Curso = data[1],
                            FechaInicio = data[2],
                            FechaFin = data[3],
                            Duracion = data[4],
                            Precio = decimal.Parse(data[5]),
                            Estatus = data[6]
                        });
                    }
                }
            }
        }
        public IActionResult OnPostDelete(int id)
        {
            string filePath = "wwwroot/InscripcionCursos.txt";
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
    public class VerInscrpcion
    {
        public int Id { get; set; }
        public string? Cliente { get; set; }
        public string? Curso { get; set; }
        public string? FechaInicio { get; set; }
        public string? FechaFin { get; set; }
        public string? Duracion { get; set; }
        public decimal Precio { get; set; }
        public string? Estatus { get; set; }
    }
}
