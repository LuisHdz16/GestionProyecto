using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace CRM.Pages
{
    public class VerTratamientosModel : PageModel
    {
        public List<Tratamiento> Tratamientos { get; set; } = new List<Tratamiento>();

        public void OnGet()
        {
            string filePath = "wwwroot/tratamientosdata.txt";
            if (System.IO.File.Exists(filePath))
            {
                var lines = System.IO.File.ReadAllLines(filePath);
                foreach (var line in lines)
                {
                    var data = line.Split(new string[] { "Tratamiento: ", " Precio: " }, System.StringSplitOptions.RemoveEmptyEntries);
                    if (data.Length == 2)
                    {
                        Tratamientos.Add(new Tratamiento
                        {
                            Nombre = data[0],
                            Precio = decimal.Parse(data[1])
                        });
                    }
                }
            }
        }

        public class Tratamiento
        {
            public string? Nombre { get; set; }
            public decimal Precio { get; set; }
        }
    }
}
