using BookVerse.DataAccess.Data;
using BookVerse.DataAccess.Repository.IRepository;
using BookVerse.Models;
using BookVerse.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace BookVerse.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ProductController : Controller
    {
        private readonly IProductRepository _context;
        private readonly ICategoryRepository _catcontext;
        private readonly IWebHostEnvironment _webHostEnvironment;
        public ProductController(IProductRepository context, ICategoryRepository catcontext, IWebHostEnvironment webHostEnvironment)
        {
            _context = context;
            _catcontext = catcontext;
            _webHostEnvironment = webHostEnvironment;
        }

        //Index Page for Product Show List Of Product and Actions
        public IActionResult Index()
        {

            return View();
        }

        #region API
        [HttpGet]
        public JsonResult GetProductData()
        {
            List<Product> products = _context.GetAll(includeProperty: "Category").ToList();
            return Json(new JsonResult(products));
        }

        #endregion

        //Create Page
        public IActionResult Upsert(int? id)
        {
            IEnumerable<SelectListItem> categorylist = _catcontext.GetAll().Select(x => new SelectListItem { Text = x.Name, Value = x.Id.ToString() });

            //passing data through viewbag ---alternative is viewmodels
            //ViewBag.categorylist = categorylist;

            ProductVM productVM = new ProductVM()
            {
                Product = new Product(),
                CategoryList = categorylist
            };

            if (id == null || id == 0)
            {
                //create
                return View(productVM);
            }
            else
            {
                //update
                productVM.Product = _context.Get(x => x.Id == id);
                return View(productVM);
            }
        }

        [NonAction]
        public void ValidateProduct(ProductVM productvm,int UpdateOrInsert)
        {
            
			//custom validations

			if (int.TryParse(productvm.Product.Title, out _) || float.TryParse(productvm.Product.Title, out _))
			{
				ModelState.TryAddModelError("Title", "Can not use Numeric Value as Product Name!");
                
			}

            if (_context.Get(x => x.Title == productvm.Product.Title) != null &&UpdateOrInsert == 1)
            {
                ModelState.AddModelError("", "Product Name Already Exist!");
               
            }
            
		}


		//Creating Product
		[HttpPost]
        public IActionResult Upsert(ProductVM productvm, IFormFile? productimg)
        {

            if (productvm.Product.Id == 0)
            {
                ValidateProduct(productvm, 1);
            }
            else
            {
                ValidateProduct(productvm, 0);
            }

				if (ModelState.IsValid)
                {
                string wwwrootpath = _webHostEnvironment.WebRootPath;
                if(productimg != null)
                {

                    string filename = Guid.NewGuid().ToString()+"-pid-"+productvm.Product.Id+Path.GetExtension(productimg.FileName);
                    string productimagepath = Path.Combine(wwwrootpath, @"images\product");
                    
                    
                    if(!string.IsNullOrEmpty(productvm.Product.Image)) /// checking that image is update or not by checking the productvm 
                    {
                        string oldimage = Path.Combine(wwwrootpath,productvm.Product.Image.TrimStart('\\'));
                        if (System.IO.File.Exists(oldimage) && oldimage.Equals(Path.Combine(wwwrootpath, @"\images\product\DefaultImage\DefaultProduct.png")))
                        {
                            System.IO.File.Delete(oldimage);
                        }
                    }
                    using (var filestream = new FileStream(Path.Combine(productimagepath,filename),FileMode.Create))
                    {
						productimg.CopyTo(filestream);
                    }

                    productvm.Product.Image = @"\images\product\" + filename;
                }
                else if (productimg == null)
				{
					productvm.Product.Image = Path.Combine(wwwrootpath, @"\images\product\DefaultImage\DefaultProduct.png");
				}
				if (productvm.Product.Id == 0)
                {
                    
                _context.Add(productvm.Product);
                TempData["success"] = $"{productvm.Product.Title} added successfully!";

                }
                else
                {
                    _context.UpdateProduct(productvm.Product); 
                TempData["success"] = $"{productvm.Product.Title} update successfully!";
                }

                _context.Save();
                return RedirectToAction("Index");
            }

            //After Invalid Model State it will Populate the category list
            else
            {
                productvm.CategoryList = _catcontext.GetAll().Select(x => new SelectListItem { Text = x.Name, Value = x.Id.ToString() });
            }
            return View(productvm);

        }


        /*

                //Edit Product
                [HttpGet]
                public IActionResult Edit(int? id)
                {
                    if(id==null || id == 0)
                    {
                        return NotFound();
                    }
                    Product? product = _context.Get(x => x.Id == id);
                    if(product  == null)
                    {
                        return NotFound();
                    }
                    return View(product);
                }





                [HttpPost]
                public IActionResult Edit(Product product)
                {
                    if(product == null)
                    {
                        return NotFound();
                    }
                    if (int.TryParse(product.Title, out _) || float.TryParse(product.Title, out _))
                    {
                        ModelState.AddModelError("Title", "Can not use Numeric Value as Product Name!");
                    }

                    if (ModelState.IsValid)
                    {
                        _context.Update(product);
                        _context.Save();
                        TempData["success"] = $"Product {product.Title} update successfully!";
                        return RedirectToAction("Index");
                    }
                    return View();
                }


        */



        //Delete Controller
        [HttpGet]
        public IActionResult Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            Product? product = _context.Get(d => d.Id == id);
            if (product == null)
            {
                return NotFound();
            }

            return View(product);
        }

        [HttpPost, ActionName("Delete")]
        public IActionResult DeleteProduct(int? id)
        {
            if (id == null)
            {
                NotFound();
            }
            Product? p = _context.Get(x => x.Id == id);

            if (p == null)
            {
                return NotFound();
            }
            _context.Remove(p);
            _context.Save();
            TempData["success"] = $"Product [{p.Title}] deleted successfully!";

            return RedirectToAction("Index");
        }
    }
}
