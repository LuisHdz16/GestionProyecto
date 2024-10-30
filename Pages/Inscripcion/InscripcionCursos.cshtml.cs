using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel.DataAnnotations;
using static CRM.Pages.VerTratamientosModel;

namespace CRM.Pages
{
    public class IncripcionCursosModel : PageModel
    {
        [BindProperty]
        [Required(ErrorMessage = "Cliente is required.")]
        public string? Cliente { get; set; }
        [BindProperty]
        [Required(ErrorMessage = "Curso is required.")]
        public string? Curso { get; set; }
        [BindProperty]
        [Required(ErrorMessage = "FechaInicio is required.")]
        public string? FechaInicio { get; set; }
        [BindProperty]
        [Required(ErrorMessage = "FechaFin is required.")]
        public string? FechaFin { get; set; }
        [BindProperty]
        [Required(ErrorMessage = "Duracion is required.")]
        public string? Duracion { get; set; }

        [BindProperty]
        [Required(ErrorMessage = "Precio is required.")]
        public double Precio { get; set; }
        [BindProperty]
        [Required(ErrorMessage = "Estatus is required.")]
        public string? Estatus { get; set; }
        public List<InscrpcionCliente> Clientes { get; set; } = new List<InscrpcionCliente>();
        public List<InscripcionCurso> Cursos { get; set; } = new List<InscripcionCurso>();

        public void OnGet()
        {
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
            string filePathCursos = "wwwroot/Cursos.txt";
            if (System.IO.File.Exists(filePath))
            {
                var lines = System.IO.File.ReadAllLines(filePathCursos);
                foreach (var line in lines)
                {
                    var data = line.Split(new string[] { "Curso: ", "Descripcion: ", "Estatus: " }, System.StringSplitOptions.RemoveEmptyEntries);
                    if (data.Length == 3)
                    {
                        Cursos.Add(new InscripcionCurso
                        {
                            Curso = data[0],
                            Descripcion = data[1],
                            Estatus = data[2]
                        });
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

            string filePath = "wwwroot/InscripcionCursos.txt";
            string Cursos = $"Cliente: {Cliente} Curso: {Curso} FechaInicio: {FechaInicio} FechaFin: {FechaFin} Duracion: {Duracion} Precio: {Precio} Estatus: {Estatus}";
            System.IO.File.AppendAllTextAsync(filePath, Cursos + "\n");

            TempData["SuccessMessage"] = "La inscripción ha sido registrada con éxito.";
            return RedirectToPage();
        }
        public class InscrpcionCliente
        {
            public string? Nombre { get; set; }
            public string? Apellido { get; set; }
            public string? Telefono { get; set; }
            public string? Email { get; set; }
        }

        public class InscripcionCurso
        {
            public string? Curso { get; set; }
            public string? Descripcion { get; set; }
            public string? Estatus { get; set; }
        }
    }
}
