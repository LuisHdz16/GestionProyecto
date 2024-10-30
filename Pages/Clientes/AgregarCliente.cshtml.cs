using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel.DataAnnotations;
using System.IO;

namespace CRM.Pages
{
    public class AgregarClienteModel : PageModel
    {
        [BindProperty]
        [Required(ErrorMessage = "Name is required.")]
        public string? Name { get; set; }

        [BindProperty]
        [Required(ErrorMessage = "LastName is required.")]
        public string? LastName { get; set; }

        [BindProperty]
        [Required(ErrorMessage = "Email is required.")]
        [EmailAddress(ErrorMessage = "Invalid email address.")]
        public string? Email { get; set; }

        [BindProperty]
        [Required(ErrorMessage = "Telefono is required.")]
        public double Telefono { get; set; }

        public void OnGet()
        {

        }

        public IActionResult OnPost()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            string filePath = "wwwroot/formdata.txt";
            string formData = $"Nombre: {Name} {LastName} Telefono: {Telefono} Correo: {Email}\n";
            System.IO.File.AppendAllTextAsync(filePath, formData);

            // Process form submission (e.g., save to database, send an email)
            TempData["SuccessMessage"] = "Your message has been sent successfully!";
            return RedirectToPage();
        }
    }
}