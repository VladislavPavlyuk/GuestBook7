using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using GBook.Models;
using GBook.Repository;

namespace GBook7.Pages
{
    public class CreateModel : PageModel
    {
        private readonly IRepository _repository;

        public CreateModel(IRepository repository)
        {
            _repository = repository;
        }

        [BindProperty]
        public Messages Message { get; set; } = default!;

        public IActionResult OnGet()
        {
            if (HttpContext.Session.GetString("Id") == null)
            {
                return RedirectToPage("/Account/Login");
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            try
            {
                Message.MessageDateTime = DateTime.Now;
                Message.UserId = int.Parse(HttpContext.Session.GetString("Id")!);

                await _repository.Create(Message);
                await _repository.Save();

                return RedirectToPage("./Index");
            }
            catch (Exception)
            {
                return RedirectToPage("./Index");
            }
        }
    }
}
