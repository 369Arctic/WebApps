using BookHaven_DataBaseAccess.Repository.IRepository;
using BookHaven_Models;
using BookHaven_Models.ViewModels;
using BookHaven_Utility;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace BookHaven.Areas.Customer.Controllers
{
    [Authorize]
    [Area("Customer")]
    public class CartController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        public ShoppingCartVM ShoppingCartVM { get; set; }
        public CartController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
           
        }

        //public IActionResult Index()
        //{
        //    // переписать под unitOfWork
        //    List<ShoppingCart> shoppingCartList = new List<ShoppingCart>();
        //    if (HttpContext.Session.Get<IEnumerable<ShoppingCart>>(StaticDetails.SessionCart) != null
        //        && HttpContext.Session.Get<IEnumerable<ShoppingCart>>(StaticDetails.SessionCart).Count() > 0)
        //    {
        //        shoppingCartList = HttpContext.Session.Get<List<ShoppingCart>>(StaticDetails.SessionCart);
        //    }

        //    List<int> prodInCart = shoppingCartList.Select(i => i.ProductId).ToList();
        //    IEnumerable<Product> productList = _unitOfWork.Product.GetAll(u => prodInCart.Contains(u.Id));
        //    //IEnumerable<Product> productList2 = _context.Products.Where(u => prodInCart.Contains(u.Id));

        //    return View(productList);
        //}

        public IActionResult Index()
        {
            //List<ShoppingCart> shoppingCartList = new List<ShoppingCart>();

            //// Получение данных корзины из сессии
            //if (HttpContext.Session.Get<IEnumerable<ShoppingCart>>(StaticDetails.SessionCart) != null
            //    && HttpContext.Session.Get<IEnumerable<ShoppingCart>>(StaticDetails.SessionCart).Count() > 0)
            //{
            //    shoppingCartList = HttpContext.Session.Get<List<ShoppingCart>>(StaticDetails.SessionCart);
            //}

            //// Получение списка ID продуктов из корзины
            //List<int> prodInCart = shoppingCartList.Select(u => u.ProductId).ToList();

            //// Получение всех продуктов по ID из репозитория продуктов, включая категории
            //var products = _unitOfWork.Product.GetAll(u => prodInCart.Contains(u.Id), includeProperties: "Category");

            //// Присваивание продуктов объектам ShoppingCart
            //foreach (var cart in shoppingCartList)
            //{
            //    cart.Product = products.FirstOrDefault(p => p.Id == cart.ProductId);
            //}

            //// Создание модели представления
            //ShoppingCartVM shoppingCartVM = new ShoppingCartVM()
            //{
            //    ShoppingCartsList = shoppingCartList
            //};
            //ViewBag.Total = shoppingCartList.Sum(x => x.Product.Price * x.Count);

            //return View(shoppingCartVM);

            var claimsIdentity = (ClaimsIdentity)User.Identity;
            var userId = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier).Value;

            ShoppingCartVM = new()
            {
                ShoppingCartsList = _unitOfWork.ShoppingCart.GetAll(u => u.ApplicationUserId == userId, includeProperties: "Product"),
                Order = new()
            };
            ViewBag.Total = ShoppingCartVM.ShoppingCartsList.Sum(x => x.Product.Price * x.Count);
            return View(ShoppingCartVM);
        }

        public IActionResult Plus(int cartId)
        {
            // переписать под unitOfWork
            //List<ShoppingCart> shoppingCartList = HttpContext.Session.Get<List<ShoppingCart>>(StaticDetails.SessionCart);
            //var cartItem = shoppingCartList.FirstOrDefault(c => c.ProductId == cartId);
            //if (cartItem != null)
            //{
            //    cartItem.Count++;
            //    HttpContext.Session.Set(StaticDetails.SessionCart, shoppingCartList);
            //}
            //return RedirectToAction(nameof(Index));

            var cartFromDb = _unitOfWork.ShoppingCart.Get(u => u.Id == cartId);
            cartFromDb.Count += 1;
            _unitOfWork.ShoppingCart.Update(cartFromDb);
            _unitOfWork.Save();
            return RedirectToAction(nameof(Index));
        }

        public IActionResult Minus (int cartId)
        {
            //List<ShoppingCart> shoppingCartList = HttpContext.Session.Get<List<ShoppingCart>>(StaticDetails.SessionCart);
            //ShoppingCart cartItem = shoppingCartList.FirstOrDefault(c => c.ProductId == cartId);
            //if (cartItem != null)
            //{
            //    if (cartItem.Count <= 1)
            //    {
            //        shoppingCartList.Remove(cartItem);
            //    }
            //    else
            //    {
            //        cartItem.Count--;
            //    }
            //    HttpContext.Session.Set(StaticDetails.SessionCart, shoppingCartList);
            //}
            //return RedirectToAction(nameof(Index));
            var cartFromDb = _unitOfWork.ShoppingCart.Get(u => u.Id == cartId);
            if (cartFromDb.Count <= 1)
            {
                _unitOfWork.ShoppingCart.Remove(cartFromDb);
                HttpContext.Session.SetInt32(StaticDetails.SessionCart, _unitOfWork.ShoppingCart
    .GetAll(u => u.ApplicationUserId == cartFromDb.ApplicationUserId).Count() - 1);
            }
            else
            {
                cartFromDb.Count -= 1;
                _unitOfWork.ShoppingCart.Update(cartFromDb);
            }
            _unitOfWork.Save();
            return RedirectToAction(nameof(Index));

        }

        public IActionResult Remove(int cartId)
        {
            //List<ShoppingCart> shoppingCartList = HttpContext.Session.Get<List<ShoppingCart>>(StaticDetails.SessionCart);
            //var cartItem = shoppingCartList.FirstOrDefault(c => c.ProductId == cartId);
            //if (cartItem != null)
            //{
            //    shoppingCartList.Remove(cartItem);
            //    HttpContext.Session.Set(StaticDetails.SessionCart, shoppingCartList);
            //}
            //return RedirectToAction(nameof(Index));
            var cartFromDb = _unitOfWork.ShoppingCart.Get(u => u.Id == cartId);
            _unitOfWork.ShoppingCart.Remove(cartFromDb);
            HttpContext.Session.SetInt32(StaticDetails.SessionCart, _unitOfWork.ShoppingCart
              .GetAll(u => u.ApplicationUserId == cartFromDb.ApplicationUserId).Count() - 1);
            _unitOfWork.Save();
            return RedirectToAction(nameof(Index));
        }

        //public IActionResult Summary()
        //{
        //    var claimsIdentity = (ClaimsIdentity)User.Identity;
        //    var userId = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier).Value;

        //    ShoppingCartVM shoppingCartVM = new ShoppingCartVM();

        //    if (HttpContext.Session.Get<IEnumerable<ShoppingCart>>(StaticDetails.SessionCart) != null
        //        && HttpContext.Session.Get<IEnumerable<ShoppingCart>>(StaticDetails.SessionCart).Count() > 0)
        //    {
        //        shoppingCartVM.ShoppingCartsList = HttpContext.Session.Get<List<ShoppingCart>>(StaticDetails.SessionCart);
        //    }

        //    shoppingCartVM.Order.ApplicationUser = _unitOfWork.ApplicationUser.Get(u => u.Id == userId);
        //    shoppingCartVM.Order.Name = shoppingCartVM.Order.ApplicationUser.Name;
        //    shoppingCartVM.Order.Address = shoppingCartVM.Order.ApplicationUser.Address;
        //    shoppingCartVM.Order.City = shoppingCartVM.Order.ApplicationUser.City;
        //    shoppingCartVM.Order.PhoneNumber = shoppingCartVM.Order.ApplicationUser.PhoneNumber;
        //    return View(shoppingCartVM);

        //}

        public IActionResult Summary()
        {
            var claimsIdentity = (ClaimsIdentity)User.Identity;
            var userId = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier).Value;

            ShoppingCartVM = new()
            {
                ShoppingCartsList = _unitOfWork.ShoppingCart.GetAll(u => u.ApplicationUserId == userId,
                includeProperties: "Product"),
                Order = new()
            };

            ShoppingCartVM.Order.ApplicationUser = _unitOfWork.ApplicationUser.Get(u => u.Id == userId);
            ShoppingCartVM.Order.Name = ShoppingCartVM.Order.ApplicationUser.Name;
            ShoppingCartVM.Order.Address = ShoppingCartVM.Order.ApplicationUser.Address;
            ShoppingCartVM.Order.City = ShoppingCartVM.Order.ApplicationUser.City;
            ShoppingCartVM.Order.PhoneNumber = ShoppingCartVM.Order.ApplicationUser.PhoneNumber;
            ViewBag.Total = ShoppingCartVM.ShoppingCartsList.Sum(x => x.Product.Price * x.Count);
            return View(ShoppingCartVM);
        }

    }
}
