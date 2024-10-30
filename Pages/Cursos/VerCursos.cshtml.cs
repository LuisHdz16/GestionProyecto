using CRM.Pages;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace CRM.Pages
{
    public class VerCursosModel : PageModel
    {
        public List<VerCursos> CursosRegistrados { get; set; } = new List<VerCursos>();

        public void OnGet()
        {
            string filePath = "wwwroot/Cursos.txt";
            if (System.IO.File.Exists(filePath))
            {
                var lines = System.IO.File.ReadAllLines(filePath);
                int id = 1;
                foreach (var line in lines)
                {
                    var data = line.Split(new string[] { "Curso: ", "Descripcion: ", "Estatus: " }, System.StringSplitOptions.RemoveEmptyEntries);
                    if (data.Length == 3)
                    {
                        CursosRegistrados.Add(new VerCursos
                        {
                            Id = id++,
                            Curso = data[0],
                            Descripcion = data[1],
                            Estatus = data[2]
                        });
                    }
                }
            }
        }

        public IActionResult OnPostDelete(int id)
        {
            string filePath = "wwwroot/Cursos.txt";
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

    public class VerCursos
    {
        public int Id { get; set; }
        public string? Curso { get; set; }
        public string? Descripcion { get; set; }
        public string? Estatus { get; set; }
    }
}