using BookHavenRazorPage.Data;
using BookHavenRazorPage.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace BookHavenRazorPage.Pages.Categories
{
    public class DeleteModel : PageModel
    {
        private readonly ApplicationDbContext _db;

        [BindProperty]
        public Category? Category { get; set; }
        public DeleteModel(ApplicationDbContext db)
        {
            _db = db;
        }



        public void OnGet(int? id)
        {
            if (id != 0 && id != null)
            {
                Category = _db.Categories.FirstOrDefault(u => u.Id == id);
            }
        }

        public IActionResult OnPost(int id)
        {
            Category = _db.Categories.FirstOrDefault(u => u.Id == id);
            if (Category == null)
            {
                return NotFound();
            }
            _db.Categories.Remove(Category);
            _db.SaveChanges();
            return RedirectToPage("Index");

        }
    }
}
