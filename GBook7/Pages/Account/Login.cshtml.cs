using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using GBook.Models;
using GBook.Repository;
using System.Security.Cryptography;
using System.Text;

namespace GBook7.Pages.Account
{
    public class LoginModel : PageModel
    {
        private readonly IRepository _repository;

        public LoginModel(IRepository repository)
        {
            _repository = repository;
        }

        [BindProperty]
        public GBook.Models.LoginModel LoginViewModel { get; set; } = default!;

        public IActionResult OnGet()
        {
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            var user = await _repository.GetUserByLoginAsync(LoginViewModel.Login!);
            
            if (user == null)
            {
                ModelState.AddModelError("", "Wrong login or password!");
                return Page();
            }

            string? salt = user.Salt;

            //convert password to byte array
            byte[] password = Encoding.Unicode.GetBytes(salt + LoginViewModel.Password);

            //calculate hash representation in bytes
            byte[] byteHash = SHA256.HashData(password);

            StringBuilder hash = new StringBuilder(byteHash.Length);
            for (int i = 0; i < byteHash.Length; i++)
                hash.Append(string.Format("{0:X2}", byteHash[i]));

            if (user.Password != hash.ToString())
            {
                ModelState.AddModelError("", "Wrong login or password!");
                return Page();
            }

            HttpContext.Session.SetString("Name", user.Login!);
            HttpContext.Session.SetString("Id", user.Id.ToString());

            return RedirectToPage("/Index");
        }
    }
}
