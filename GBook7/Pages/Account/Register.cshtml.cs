using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using GBook.Models;
using GBook.Repository;
using System.Security.Cryptography;
using System.Text;

namespace GBook7.Pages.Account
{
    public class RegisterModel : PageModel
    {
        private readonly IRepository _repository;

        public RegisterModel(IRepository repository)
        {
            _repository = repository;
        }

        [BindProperty]
        public GBook.Models.RegisterModel RegisterViewModel { get; set; } = default!;

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

            if (await _repository.UserExistsAsync(RegisterViewModel.Login!))
            {
                ModelState.AddModelError("", "User with this login already exists!");
                return Page();
            }

            Users user = new Users();
            user.Login = RegisterViewModel.Login;

            byte[] saltbuf = new byte[16];

            RandomNumberGenerator randomNumberGenerator = RandomNumberGenerator.Create();
            randomNumberGenerator.GetBytes(saltbuf);

            StringBuilder sb = new StringBuilder(16);
            for (int i = 0; i < 16; i++)
                sb.Append(string.Format("{0:X2}", saltbuf[i]));
            string salt = sb.ToString();

            //convert password to byte array
            byte[] password = Encoding.Unicode.GetBytes(salt + RegisterViewModel.Password);

            //calculate hash representation in bytes
            byte[] byteHash = SHA256.HashData(password);

            StringBuilder hash = new StringBuilder(byteHash.Length);
            for (int i = 0; i < byteHash.Length; i++)
                hash.Append(string.Format("{0:X2}", byteHash[i]));

            user.Password = hash.ToString();
            user.Salt = salt;

            await _repository.CreateUserAsync(user);

            return RedirectToPage("./Login");
        }
    }
}
