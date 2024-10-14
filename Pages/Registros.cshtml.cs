using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Collections.Generic;

namespace CRM.Pages
{
    public class RegistroTratamientosModel : PageModel
    {
        [BindProperty]
        [Required(ErrorMessage = "Cliente es requerido.")]
        public string? Cliente { get; set; }

        [BindProperty]
        [Required(ErrorMessage = "Tratamiento es requerido.")]
        public string? Tratamiento { get; set; }

        [BindProperty]
        [Required(ErrorMessage = "Encargado es requerido.")]
        public string? Encargado { get; set; }

        [BindProperty]
        [Required(ErrorMessage = "Monto es requerido.")]
        [Range(0, double.MaxValue, ErrorMessage = "El monto debe ser un n�mero positivo.")]
        public double Monto { get; set; }

        [BindProperty]
        [Required(ErrorMessage = "Fecha es requerida.")]
        [DataType(DataType.Date)]
        public string? Fecha { get; set; }

        public List<string> Clientes { get; set; } = new List<string>();
        public List<string> Tratamientos_ { get; set; } = new List<string>();
        public List<string> Personaencargada { get; set; } = new List<string>();

        public void OnGet()
        {
            // Cargar clientes registrados desde el archivo
            string filePathClientes = "wwwroot/formdata.txt";
            string filePathTratamientos = "wwwroot/tratamientosdata.txt";
            string filePathPersona = "wwwroot/personasEncargadas.txt";

            if (System.IO.File.Exists(filePathClientes))
            {
                var lines = System.IO.File.ReadAllLines(filePathClientes);

                foreach (var line in lines)
                {
                    var data = line.Split(new string[] { "Nombre: ", " Telefono: ", " Correo: " }, StringSplitOptions.RemoveEmptyEntries);
                    if (data.Length == 3)
                    {
                        var nombres = data[0].Split(' ');
                        Clientes.Add($"{nombres[0]} {nombres[1]}");
                    }
                }
            }
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
            if (System.IO.File.Exists(filePathPersona))
            {
                var lines_ = System.IO.File.ReadAllLines(filePathPersona);

                foreach (var line in lines_)
                {
                    var data = line.Split(new string[] {"Nombre: ", " Puesto: "}, StringSplitOptions.RemoveEmptyEntries);
                    if (data.Length == 2)
                    {
                        var nombres = data[0].Split(',');
                        Personaencargada.Add($"{data[0]}");
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

            // Guardar el registro del tratamiento en un archivo de texto
            string filePath = "wwwroot/tratamientos.txt";
            string tratamientoData = $"Cliente: {Cliente} Tratamiento: {Tratamiento} Encargado: {Encargado} Monto: {Monto} Fecha: {Fecha}\n";
            System.IO.File.AppendAllTextAsync(filePath, tratamientoData);

            TempData["SuccessMessage"] = "Tratamiento registrado exitosamente!";
            return RedirectToPage();
        }
    }
}