using BookHaven_DatabaseAccess.Data;
using BookHaven_DataBaseAccess.Repository.IRepository;
using BookHaven_Models;
using BookHaven_Utility;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BookHaven.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles =StaticDetails.Role_Admin)] // чтобы не было доступа по прямому url
    public class CategoryController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;

        public CategoryController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public IActionResult Index()
        {
            List<Category> objCategoryList = _unitOfWork.Category.GetAll().ToList();

            return View(objCategoryList);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(Category obj)
        {
            /* проверка ошибка на совпадение имени и порядка отображения. Добавление кастомной ошибка в ModelState
            if (obj.Name == obj.DisplayOrder.ToString())
            {
                ModelState.AddModelError("name", "The display order cannot exactly match the Name");
            }
            */
            /*TODO 
             * Проверка на уже существующую категорию в БД
             */
            if (ModelState.IsValid)
            {
                _unitOfWork.Category.Add(obj);
                _unitOfWork.Save();
                TempData["success"] = "Category created successfully";
                return RedirectToAction("Index");
            }
            return View(obj);
        }

        public IActionResult Edit(int? id)
        {
            if (id == 0 && id == null)
            {
                return NotFound();
            }

            //Category? category = _db.Categories.FirstOrDefault(u => u.Id == id);
            Category? category = _unitOfWork.Category.Get(u => u.Id == id);
            if (category == null)
            {
                return NotFound();
            }
            //Category category2 = _db.Categories.Find(id); // работает только с первичным ключом (id)
            //Также Find ищет в кэше контекста, если элемент есть в кэше, то он будет возвращен без запроса к БД
            return View(category);

        }

        [HttpPost]
        public IActionResult Edit(Category category)
        {
            if (ModelState.IsValid)
            {
                _unitOfWork.Category.Update(category);
                _unitOfWork.Save();
                TempData["success"] = "Category updated successfully";
                return RedirectToAction("Index");
            }
            return View();
        }


        public IActionResult Delete(int? id)
        {
            if (id == 0 && id == null)
            {
                return NotFound();
            }

            Category? category = _unitOfWork.Category.Get(u => u.Id == id);
            if (category == null)
            {
                return NotFound();
            }
            return View(category);

        }

        [HttpPost]
        [ActionName("Delete")]
        public IActionResult DeletePost(int? id)
        {
            Category? categoryForDelete = _unitOfWork.Category.Get(u => u.Id == id);
            if (categoryForDelete == null)
            {
                return NotFound();
            }

            _unitOfWork.Category.Remove(categoryForDelete);
            _unitOfWork.Save();
            TempData["success"] = "Category deleted successfully";
            return RedirectToAction("Index");

        }
    }
}
