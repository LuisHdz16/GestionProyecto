using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Collections.Generic;
using static CRM.Pages.IncripcionCursosModel;
using System.Reflection;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace CRM.Pages.Clientes
{
    public class AgregarCitaModel : PageModel
    {
        [BindProperty, Required(ErrorMessage = "Cliente es requerido.")]
        public string? Cliente { get; set; }

        [BindProperty, Required(ErrorMessage = "Tratamiento es requerido.")]
        public string? Tratamiento { get; set; }

        [BindProperty]
        public string? Promocion { get; set; }

        [BindProperty, Required(ErrorMessage = "Precio es requerido.")]
        public double Precio { get; set; }

        [BindProperty, Required(ErrorMessage = "Fecha es requerida."), DataType(DataType.Date)]
        public string? Fecha { get; set; }

        [BindProperty, Required(ErrorMessage = "Estatus es requerido.")]
        public string? Estatus { get; set; }

        public List<InscrpcionCliente> Clientes { get; set; } = new List<InscrpcionCliente>();
        public List<TratamientoCitas> Tratamientos_ { get; set; } = new List<TratamientoCitas>();
        public List<PromocionCitas> PromocionesDisponibles { get; set; } = new List<PromocionCitas>();

        public void OnGet()
        {
            // Cargar clientes desde archivo
            string filePath = "wwwroot/formdata.txt";

            if (System.IO.File.Exists(filePath))
            {
                var lines = System.IO.File.ReadAllLines(filePath);

                foreach (var line in lines)
                {
                    var data = line.Split(new string[] { "Nombre: ", " Telefono: ", " Correo: " }, StringSplitOptions.RemoveEmptyEntries);
                    if (data.Length == 3)
                    {
                        Clientes.Add(new InscrpcionCliente
                        {
                            Nombre = data[0],
                            Telefono = data[1],
                            Email = data[2]
                        });
                    }
                }
            }

            // Cargar tratamientos desde archivo
            string filePathTratamientos = "wwwroot/tratamientosdata.txt";
            if (System.IO.File.Exists(filePathTratamientos))
            {
                var lines = System.IO.File.ReadAllLines(filePathTratamientos);
                foreach (var line in lines)
                {
                    var data = line.Split(new[] { "Tratamiento: ", " Precio: " }, StringSplitOptions.RemoveEmptyEntries);
                    if (data.Length == 2)
                    {
                        Tratamientos_.Add(new TratamientoCitas { Nombre = data[0], Precio = double.Parse(data[1]) });
                    }
                }
            }

            // Cargar promociones disponibles


        }


        public JsonResult OnGetPromociones(string tratamiento)
        {
            List<PromocionCitas> promocionesFiltradas = new List<PromocionCitas>();
            string filePathPromociones = "wwwroot/promociones.txt";

            if (System.IO.File.Exists(filePathPromociones))
            {
                var lines = System.IO.File.ReadAllLines(filePathPromociones);
                foreach (var line in lines)
                {
                    var data = line.Split(new[] { "Tratamiento: ", " Descripcion: ", " PrecioNuevo: ", " Estatus: " }, StringSplitOptions.RemoveEmptyEntries);
                    if (data.Length == 4 && data[3] == "Disponible")
                    {
                        promocionesFiltradas.Add(new PromocionCitas
                        {
                            Tratamiento = data[0],
                            Descripcion = data[1],
                            PrecioNuevo = double.Parse(data[2]),
                            Estatus = data[3]
                        });
                    }
                }
            }

            return new JsonResult(promocionesFiltradas);
        }


        public IActionResult OnPost()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            // Cálculo del precio
            var tratamientoSeleccionado = Tratamientos_.Find(t => t.Nombre == Tratamiento);
            Precio = tratamientoSeleccionado?.Precio ?? 0;

            if (!string.IsNullOrEmpty(Promocion))
            {
                var promocionSeleccionada = PromocionesDisponibles.Find(p => p.Descripcion == Promocion && p.Tratamiento == Tratamiento);
                if (promocionSeleccionada != null)
                {
                    Precio = promocionSeleccionada.PrecioNuevo;
                }
            }

            // Guardar la cita en un archivo de texto
            string filePath = "wwwroot/citas.txt";
            string citaData = $"Cliente: {Cliente} Tratamiento: {Tratamiento} Promocion: {Promocion} Precio: {Precio} Fecha: {Fecha} Estatus: {Estatus}\n";
            System.IO.File.AppendAllTextAsync(filePath, citaData);

            TempData["SuccessMessage"] = "Cita registrada exitosamente!";
            return RedirectToPage("~/Citas/VerRegistros");
        }
    }

    public class TratamientoCitas
    {
        public string? Nombre { get; set; }
        public double Precio { get; set; }
    }

    public class PromocionCitas
    {
        public string? Tratamiento { get; set; }
        public string? Descripcion { get; set; }
        public double PrecioNuevo { get; set; }
        public string? Estatus { get; set; }
    }
}

