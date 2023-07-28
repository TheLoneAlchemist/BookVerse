using BookVerse.DataAccess.Data;
using BookVerse.DataAccess.Repository.IRepository;
using BookVerse.Models;
using BookVerse.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace BookVerse.Controllers
{
    [Area("Admin")]
    public class ProductController : Controller
    {
        private readonly IProductRepository _context;
        private readonly ICategoryRepository _catcontext;
        public ProductController(IProductRepository context, ICategoryRepository catcontext)
        {
            _context=context;
            this._catcontext = catcontext;
        }

        //Index Page for Product Show List Of Product and Actions
        public IActionResult Index()
        {
            List<Product> products = _context.GetAll().ToList();

            return View(products);
        }

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

        //Creating Product
        [HttpPost]
        public IActionResult Upsert(ProductVM productvm,IFormFile? file)
        {
            if(int.TryParse(productvm.Product.Title, out _) || float.TryParse(productvm.Product.Title,out _))
            {
                ModelState.TryAddModelError("Title","Can not use Numeric Value as Product Name!");
            }

            if(_context.Get(x=>x.Title == productvm.Product.Title) != null)
            {
                ModelState.AddModelError("", "Product Name Already Exist!");
            }

            if(ModelState.IsValid)
            {
                _context.Add(productvm.Product);
                _context.Save();
                TempData["success"] = $"Product {productvm.Product.Title} created successfully!";
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
            if(id== null)
            {
                return NotFound();
            }
            Product? product = _context.Get(d => d.Id == id);
            if(product == null)
            {
                return NotFound();
            }

            return View(product);
        }

        [HttpPost,ActionName("Delete")]
        public IActionResult DeleteProduct(int? id)
        {
            if(id == null)
            {
                NotFound();
            }
            Product? p = _context.Get(x => x.Id ==id);

            if(p == null)
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
