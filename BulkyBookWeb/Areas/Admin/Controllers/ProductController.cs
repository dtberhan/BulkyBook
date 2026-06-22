using BulkyBook.DataAccess.Repo.IRepo;
using BulkyBook.Models;
using BulkyBook.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace BulkyBookWeb.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ProductController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IWebHostEnvironment _hostEnvironment;
        public ProductController(IUnitOfWork unitOfwork, IWebHostEnvironment hostEnvironment)
        {
            _unitOfWork = unitOfwork;
            _hostEnvironment = hostEnvironment;
        }
        public async Task<IActionResult> Index()
        {
            IEnumerable<Product> products =  await _unitOfWork.Product.GetAll(includeProperties:"Category");
          
            return View(products);
        }

        public async Task<IActionResult> Upsert(int? id)
        {
            IEnumerable<Category> categories = await _unitOfWork.Category.GetAll();
           
            ProductVM productVM = new()
            {
                CategoryList = categories.Select(c => new SelectListItem
                {
                    Value = c.Id.ToString(),
                    Text = c.Name
                }),
                Product = new Product()
            };
            if(id == null || id == 0)
            {
                return View(productVM);
            }
            else
            {
                productVM.Product = await _unitOfWork.Product.Get(u => u.Id == id);
                return View(productVM);
            }

           
        }

        [HttpPost]
        public async Task<IActionResult> Upsert(ProductVM productVM, IFormFile? file)
        {
            if (ModelState.IsValid)
            {
                string wwwRootPath = _hostEnvironment.WebRootPath;
                if (file != null)
                {
                    string fileName = Guid.NewGuid().ToString() + Path.GetExtension(file.FileName); ;
                    string productPath = Path.Combine(wwwRootPath, @"images\product");
                  if(!string.IsNullOrEmpty(productVM.Product.ImageUrl))
                    {
                        var oldImagePath = 
                            Path.Combine(wwwRootPath, productVM.Product.ImageUrl.TrimStart('\\'));
                        if (System.IO.File.Exists(oldImagePath))
                        {
                            System.IO.File.Delete(oldImagePath);
                        }
                    }
                    using (var fileStream = new FileStream(Path.Combine(productPath, fileName), FileMode.Create))
                    {
                        file.CopyTo(fileStream);
                    }
                    productVM.Product.ImageUrl = @"\images\product\" + fileName;
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
                TempData["success"] = "Product created successfully!";
                return RedirectToAction("Index");
            }
            else
            {
                IEnumerable<Category> categories = await _unitOfWork.Category.GetAll();

                productVM = new()
                {
                    CategoryList = categories.Select(c => new SelectListItem
                    {
                        Value = c.Id.ToString(),
                        Text = c.Name
                    }),
                    Product = new Product()
                };
            }
                return View(productVM);
        }

        

      /*  public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }
            var product = await _unitOfWork.Product.Get(u => u.Id == id);
            if (product == null)
            {
                return Json(new { success = false, message = "Error while deleting" });
            }

            return View(product);
        }

        [HttpPost, ActionName("Delete")]
        public IActionResult DeletePost(int? id)
        {
            var product = _unitOfWork.Product.Get(u => u.Id == id).Result;
            if (product == null)
            {
                return NotFound();
            }

            {
                _unitOfWork.Product.Remove(product);
                _unitOfWork.Save();
                TempData["success"] = "Product deleted successfully!";
                return RedirectToAction("Index");
            }

        }
      */
        //API Calls
        #region
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            IEnumerable<Product> products = await _unitOfWork.Product.GetAll(includeProperties: "Category");

            return Json(new { data = products });
        }

        [HttpDelete]
        public async Task<IActionResult> Delete(int? id)
        {
            var product = await _unitOfWork.Product.Get(u => u.Id == id);
            if (product == null)
            {
                return Json(new { success = false, message = "Error while deleting" });
            }
            var oldImagePath =
                Path.Combine(_hostEnvironment.WebRootPath, product.ImageUrl.TrimStart('\\'));
            if (System.IO.File.Exists(oldImagePath))
            {
                System.IO.File.Delete(oldImagePath);
            }
            _unitOfWork.Product.Remove(product);
            _unitOfWork.Save();
            return Json(new { success = true, message = "Delete successful" });
        }
        #endregion

    }
}
