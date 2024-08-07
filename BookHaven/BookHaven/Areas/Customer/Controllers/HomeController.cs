using BookHaven_DataBaseAccess.Repository.IRepository;
using BookHaven_Models;
using BookHaven_Models.ViewModels;
using BookHaven_Utility;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Security.Claims;

namespace BookHaven.Areas.Customer.Controllers
{
    [Area("Customer")]
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IUnitOfWork _unitOfWork;

        public ShoppingCartVM ShoppingCartVM { get;  set; }

        public HomeController(ILogger<HomeController> logger, IUnitOfWork unitOfWork)
        {
            _logger = logger;
            _unitOfWork = unitOfWork;
        }

        public IActionResult Index()
        {

            HomeVM homeVM = new HomeVM()
            {
                Products = _unitOfWork.Product.GetAll(),
                Categories = _unitOfWork.Category.GetAll()
            };

            //IEnumerable<Product> productList = _unitOfWork.Product.GetAll(includeProperties:"Category");
            //return View(productList);
            return View(homeVM);
        }

        public IActionResult Details(int productId)
        {
            //Product product = _unitOfWork.Product.Get(u => u.Id == productId, includeProperties: "Category");
            //return View(product);
            ShoppingCart cart = new()
            {
                Product = _unitOfWork.Product.Get(u => u.Id == productId, includeProperties: "Category"),
                Count = 1,
                ProductId = productId
            };
            return View(cart);
        }

        //[HttpPost]
        //[ActionName("Details")]
        //public IActionResult DetailsPost(int productId)
        //{

        //    List<ShoppingCart> shoppingCartsList = new List<ShoppingCart>();
        //    if (HttpContext.Session.Get<IEnumerable<ShoppingCart>>(StaticDetails.SessionCart) != null
        //                    && HttpContext.Session.Get<IEnumerable<ShoppingCart>>(StaticDetails.SessionCart).Count() > 0)
        //    {
        //        shoppingCartsList = HttpContext.Session.Get<List<ShoppingCart>>(StaticDetails.SessionCart);
        //    }
        //    shoppingCartsList.Add(new ShoppingCart { ProductId = productId, Count = 1 });
        //    HttpContext.Session.Set(StaticDetails.SessionCart, shoppingCartsList);
        //    return RedirectToAction(nameof(Index));
        //}

        [HttpPost]
        [Authorize]
        public IActionResult Details (ShoppingCart shoppingCart)
        {
            var claimsIdentity = (ClaimsIdentity)User.Identity;
            var userId = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier).Value;
            shoppingCart.ApplicationUserId = userId;

            ShoppingCart cartFromDb = _unitOfWork.ShoppingCart.Get(u => u.ApplicationUserId == userId &&
            u.ProductId == shoppingCart.ProductId);

            if(cartFromDb != null)
            {
                cartFromDb.Count += shoppingCart.Count;
                _unitOfWork.ShoppingCart.Update(cartFromDb);
                _unitOfWork.Save();
            }
            else
            {
                _unitOfWork.ShoppingCart.Add(shoppingCart);
                _unitOfWork.Save();
                HttpContext.Session.SetInt32(StaticDetails.SessionCart,
    _unitOfWork.ShoppingCart.GetAll(u => u.ApplicationUserId == userId).Count());
            }

            TempData["success"] = "Cart updated successfullt";

            return RedirectToAction(nameof(Index));
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
