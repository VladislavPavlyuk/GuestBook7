using Microsoft.AspNetCore.Mvc.RazorPages;
using GBook.Models;
using GBook.Repository;

namespace GBook7.Pages
{
    public class IndexModel : PageModel
    {
        private readonly IRepository _repository;

        public IndexModel(IRepository repository)
        {
            _repository = repository;
        }

        public IList<Messages> Messages { get; set; } = default!;

        public bool IsAuthenticated => HttpContext.Session.GetString("Name") != null;

        public async Task OnGetAsync()
        {
            Messages = await _repository.GetMessageList();
        }
    }
}
