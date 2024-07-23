using BookHaven_DatabaseAccess.Data;
using BookHaven_DataBaseAccess.Repository.IRepository;
using BookHaven_Models;
using BookHaven_Models.ViewModels;
using BookHaven_Utility;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace BookHaven.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = StaticDetails.Role_Admin)]
    public class ProductController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public ProductController(IUnitOfWork unitOfWork, IWebHostEnvironment webHostEnvironment)
        {
            _unitOfWork = unitOfWork;
            _webHostEnvironment = webHostEnvironment;
        }

        public IActionResult Index()
        {
            List<Product> objProductList = _unitOfWork.Product.GetAll(includeProperties:"Category").ToList();

            return View(objProductList);
        }

        public IActionResult Upsert(int? id)
        {
            IEnumerable<SelectListItem> CategoryList = _unitOfWork.Category
                    .GetAll().Select(u => new SelectListItem
                    {
                        Text = u.Name,
                        Value = u.Id.ToString()
                    }); // разобрать строку. Объснение на 6.18.40
            //ViewBag.CategoryList = CategoryList;
            //ViewData["CategoryList"] = CategoryList;

            ProductVM productVM = new()
            {
                Product = new Product(),
                CategoryList = CategoryList
            };
            if (id == null || id == 0)
            {
                //create
                return View(productVM);
            }
            else
            {
                //update
                productVM.Product = _unitOfWork.Product.Get(u => u.Id == id);
                return View(productVM);
            }

            
        }

        [HttpPost]
        public IActionResult Upsert(ProductVM productVM, IFormFile? file)
        {
           
            if (ModelState.IsValid)
            {
                string wwwRootPath = _webHostEnvironment.WebRootPath;
                if (file != null)
                {
                    string fileName = Guid.NewGuid().ToString() + Path.GetExtension(file.FileName);
                    string productPath = Path.Combine(wwwRootPath, @"Images\Product");

                    if (!string.IsNullOrEmpty(productVM.Product.ImageURL))
                    {
                        // удаляем старое изображение при обновлении файла, после чего загружаем новое
                        var oldImagePath = Path.Combine(wwwRootPath, productVM.Product.ImageURL.TrimStart('\\'));

                        if (System.IO.File.Exists(oldImagePath))
                        {
                            System.IO.File.Delete(oldImagePath);
                        }

                    }

                    using (var fileStream = new FileStream(Path.Combine(productPath, fileName), FileMode.Create))
                    {
                        file.CopyTo(fileStream);
                    }

                    productVM.Product.ImageURL = @"\Images\Product\"+fileName;

                }
                if (productVM.Product.Id == 0)
                {
                    _unitOfWork.Product.Add(productVM.Product);
                }
                else
                {
                    _unitOfWork.Product.Update(productVM.Product);
                }
               
                _unitOfWork.Save();
                TempData["success"] = "Product created successfully";
                return RedirectToAction("Index");
            }
            return View(productVM);
            //else // можно написать так, чтобы не выбрасывало экзепшен. При условии что [ValidateNever] не стоит
            //{
            //    productVM.CategoryList = _unitOfWork.Category
            //        .GetAll().Select(u => new SelectListItem
            //        {
            //            Text = u.Name,
            //            Value = u.Id.ToString()
            //        });
            //    return View(productVM);
            //}
        }


        //public IActionResult Edit(int? id)
        //{
        //    if (id == 0 || id == null)
        //    {
        //        return NotFound();
        //    }

        //    Product? productForEdit = _unitOfWork.Product.Get(u => u.Id == id);
        //    if (productForEdit == null)
        //    {
        //        return NotFound();
        //    }

        //    return View(productForEdit);
        //}

        //[HttpPost]
        //public IActionResult Edit(Product product)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        _unitOfWork.Product.Update(product);
        //        _unitOfWork.Save();
        //        TempData["success"] = "Product update successfully";
        //        return RedirectToAction("Index");
        //    }

        //    return View(product);
        //}


        //public IActionResult Delete(int? id)
        //{
        //    if (id == 0 || id == null)
        //    {
        //        return NotFound();
        //    }
        //    Product productForDelete = _unitOfWork.Product.Get(u => u.Id == id);
        //    return View(productForDelete);
        //}

        //[HttpPost]
        //[ActionName("Delete")]
        //public IActionResult DeletePost(int? id)
        //{
        //    if (id == 0 || id == null)
        //    {
        //        return NotFound();
        //    }

        //    Product productForDelete = _unitOfWork.Product.Get(u => u.Id == id);
        //    if (productForDelete == null)
        //    {
        //        return NotFound();
        //    }

        //    _unitOfWork.Product.Remove(productForDelete);
        //    _unitOfWork.Save();
        //    TempData["success"] = "Product Delete successfully";
        //    return RedirectToAction("Index");
        //}

        #region API CALLS
        [HttpGet]
        public IActionResult GetAll()
        {
            List<Product> objProductList = _unitOfWork.Product.GetAll(includeProperties: "Category").ToList();
            return Json(new { data = objProductList });
        }

        [HttpDelete]
        public IActionResult Delete(int? id) 
        {
            var productForDelete = _unitOfWork.Product.Get(u => u.Id == id);
            if (productForDelete == null)
            {
                return Json(new
                {
                    success = false,
                    message = "Error while deleting"
                });
            }

            var oldImagePath = Path.Combine(_webHostEnvironment.WebRootPath, productForDelete.ImageURL.TrimStart('\\'));

            if (System.IO.File.Exists(oldImagePath))
            {
                System.IO.File.Delete(oldImagePath);
            }

            _unitOfWork.Product.Remove(productForDelete);
            _unitOfWork.Save();
            return Json(new { success = true, message = "Delete successful" });
        }

        #endregion





        //    private readonly IUnitOfWork _unitOfWork;

        //    public ProductController(IUnitOfWork unitOfWork)
        //    {
        //        _unitOfWork = unitOfWork;
        //    }

        //    public IActionResult Index()
        //    {
        //        List<Product> objCategoryList = _unitOfWork.Product.GetAll().ToList();

        //        return View(objCategoryList);
        //    }

        //    public IActionResult Create()
        //    {
        //        return View();
        //    }

        //    [HttpPost]
        //    public IActionResult Create(Product product)
        //    {
        //        /* проверка ошибка на совпадение имени и порядка отображения. Добавление кастомной ошибка в ModelState
        //        if (obj.Name == obj.DisplayOrder.ToString())
        //        {
        //            ModelState.AddModelError("name", "The display order cannot exactly match the Name");
        //        }
        //        */
        //        /*TODO 
        //         * Проверка на уже существующую категорию в БД
        //         */
        //        if (ModelState.IsValid)
        //        {
        //            _unitOfWork.Product.Add(product);
        //            _unitOfWork.Save();
        //            TempData["success"] = "Category created successfully";
        //            return RedirectToAction("Index");
        //        }
        //        return View(product);
        //    }

        //    public IActionResult Edit(int? id)
        //    {
        //        if (id == 0 && id == null)
        //        {
        //            return NotFound();
        //        }

        //        //Category? category = _db.Categories.FirstOrDefault(u => u.Id == id);
        //        Product? product = _unitOfWork.Product.Get(u => u.Id == id);
        //        if (product == null)
        //        {
        //            return NotFound();
        //        }
        //        //Category category2 = _db.Categories.Find(id); // работает только с первичным ключом (id)
        //        //Также Find ищет в кэше контекста, если элемент есть в кэше, то он будет возвращен без запроса к БД
        //        return View(product);

        //    }

        //    [HttpPost]
        //    public IActionResult Edit(Product product)
        //    {
        //        if (ModelState.IsValid)
        //        {
        //            _unitOfWork.Product.Update(product);
        //            _unitOfWork.Save();
        //            TempData["success"] = "Category updated successfully";
        //            return RedirectToAction("Index");
        //        }
        //        return View();
        //    }


        //    public IActionResult Delete(int? id)
        //    {
        //        if (id == 0 && id == null)
        //        {
        //            return NotFound();
        //        }

        //        Product? product = _unitOfWork.Product.Get(u => u.Id == id);
        //        if (product == null)
        //        {
        //            return NotFound();
        //        }
        //        return View(product);

        //    }

        //    [HttpPost]
        //    [ActionName("Delete")]
        //    public IActionResult DeletePost(int? id)
        //    {
        //        Product? productForDelete = _unitOfWork.Product.Get(u => u.Id == id);
        //        if (productForDelete == null)
        //        {
        //            return NotFound();
        //        }

        //        _unitOfWork.Product.Remove(productForDelete);
        //        _unitOfWork.Save();
        //        TempData["success"] = "Category deleted successfully";
        //        return RedirectToAction("Index");

        //    }
    }
}
