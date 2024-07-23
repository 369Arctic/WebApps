using BookHavenRazorPage.Data;
using BookHavenRazorPage.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace BookHavenRazorPage.Pages.Categories
{
    //[BindProperties] можно написать тут, будет доступно для всех свойств
    public class CreateModel : PageModel
    {
        private readonly ApplicationDbContext _db;
        [BindProperty] // чтобы свойство было доступно для post-запроса, нужно добавить этот атрибут
        public Category Category{ get; set; }
        public CreateModel(ApplicationDbContext db)
        {
            _db = db;
        }

        public void OnGet()
        {
        }

        public IActionResult OnPost()
        {
            _db.Categories.Add(Category);
            _db.SaveChanges();
            return RedirectToPage("Index");
        }
    }
}
