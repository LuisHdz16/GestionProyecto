using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Collections.Generic;
using System.IO;

namespace CRM.Pages
{
    public class VerTratamientosModel : PageModel
    {
        public List<Tratamiento> Tratamientos { get; set; } = new List<Tratamiento>();

        public void OnGet()
        {
            // Leer los registros de tratamientos desde el archivo de texto
            string filePath = "wwwroot/tratamientos.txt";

            if (System.IO.File.Exists(filePath))
            {
                var lines = System.IO.File.ReadAllLines(filePath);

                foreach (var line in lines)
                {
                    var data = line.Split(new string[] { "Cliente: ", ", Tratamiento: ", ", Encargado: ", ", Monto: ", ", Fecha: " }, StringSplitOptions.RemoveEmptyEntries);
                    if (data.Length == 5)
                    {
                        Tratamientos.Add(new Tratamiento
                        {
                            Cliente = data[0],
                            Tratamiento_ = data[1],
                            Encargado = data[2],
                            Monto = data[3],
                            Fecha = data[4]
                        });
                    }
                }
            }
        }
    }

    public class Tratamiento
    {
        public string? Cliente { get; set; }
        public string? Tratamiento_ { get; set; }
        public string? Encargado { get; set; }
        public string? Monto { get; set; }
        public string? Fecha { get; set; }
    }
}
