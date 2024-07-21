using GustoGlide.Web.Models;
using GustoGlide.Web.Service.IService;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace GustoGlide.Web.Controllers
{
    public class ProductController : Controller
    {
        private readonly IProductService _productService;
        public ProductController(IProductService productService)
        {
            _productService = productService;
        }

        public async Task<IActionResult> ProductIndex()
        {
            List<ProductDto>? products = [];

            ResponseDto responseDto = await _productService.GetAllProductsAsync();
            if (responseDto != null && responseDto.IsSuccess)
            {
                products = JsonConvert.DeserializeObject<List<ProductDto>>(Convert.ToString(responseDto.Result));
            }
            else
            {
                TempData["error"] = responseDto?.Message;
            }
            return View(products);
        }


        public async Task<IActionResult> ProductCreate()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> ProductCreate(ProductDto productDto)
        {
            if (ModelState.IsValid)
            {
                ResponseDto responseDto = await _productService.CreateProductAsync(productDto);
                if (responseDto!= null && responseDto.IsSuccess)
                {
                    TempData["success"] = "Product created successfully";
                    return RedirectToAction(nameof(ProductIndex));
                }
                else
                {
                    TempData["error"] = responseDto?.Message;
                }
            }
            return View(productDto);
        }


        public async Task<IActionResult> ProductEdit(int productId)
        {
            ResponseDto? responseDto = await _productService.GetProductByIdAsync(productId);

            if (responseDto != null && responseDto.IsSuccess)
            {
                ProductDto productDto = JsonConvert.DeserializeObject<ProductDto>(Convert.ToString(responseDto.Result));
                return View(productDto);
            }
            else
            {
                TempData["error"] = responseDto?.Message;
            }
            return NotFound();
        }

        [HttpPost]
        public async Task<IActionResult> ProductEdit (ProductDto productDto)
        {
            ResponseDto responseDto = await _productService.UpdateProductAsync(productDto);

            if (responseDto != null && responseDto.IsSuccess)
            {
                TempData["success"] = "Product deleted successfully";
                return RedirectToAction(nameof(ProductIndex));
            }
            else
            {
                TempData["error"] = responseDto.Message;
            }
            return View(productDto);
        }

        public async Task<IActionResult> ProductDelete(int productId)
        {
            ResponseDto? responseDto = await _productService.GetProductByIdAsync(productId);

            if (responseDto != null && responseDto.IsSuccess)
            {
               ProductDto productDto = JsonConvert.DeserializeObject<ProductDto>(Convert.ToString(responseDto.Result));
               return View(productDto);
            }
            else
            {
                TempData["error"] = responseDto?.Message;
            }
            return NotFound();
        }

        [HttpPost]
        public async Task<IActionResult> ProductDelete(ProductDto productDto)
        {
            ResponseDto responseDto = await _productService.DeleteProdyctAsync(productDto.ProductId);

            if (responseDto != null && responseDto.IsSuccess)
            {
                TempData["success"] = "Product deleted successfully";
                return RedirectToAction(nameof(ProductIndex));
            }
            else
            {
                TempData["error"] = responseDto.Message;
            }
            return View(productDto);
        }

    }
}
