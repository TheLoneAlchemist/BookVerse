using BookVerse.DataAccess.Data;
using BookVerse.DataAccess.Repository.IRepository;
using BookVerse.Models;
using Microsoft.AspNetCore.Mvc;

namespace BookVerse.Controllers
{
    [Area("Admin")]
    public class CategoryController : Controller
    {
        private readonly ICategoryRepository _context;
        public CategoryController(ICategoryRepository context)
        {
            _context=context;
        }

        //Index Page for Category Show List Of Category and Actions
        public IActionResult Index()
        {
            List<Category> category = _context.GetAll().ToList();
            return View(category);
        }


        //Create Page
        public IActionResult Create()
        {

            return View();
        }

        //Creating Category
        [HttpPost]
        public IActionResult Create(Category category)
        {
            if(int.TryParse(category.Name, out _) || float.TryParse(category.Name,out _))
            {
                ModelState.AddModelError("Name","Can not use Numeric Value as Name!");
            }

            if(_context.Get(x=>x.Name == category.Name) != null)
            {
                ModelState.AddModelError("", "Category Name Already Exist!");
            }

            if(ModelState.IsValid)
            {
                _context.Add(category);
                _context.Save();
                TempData["success"] = $"Category {category.Name} created successfully!";
                return RedirectToAction("Index");
            }
            return View();
            
        }




        //Edit Category
        [HttpGet]
        public IActionResult Edit(int? id)
        {
            if(id==null || id == 0)
            {
                return NotFound();
            }
            Category? cat = _context.Get(x => x.Id == id);
            if(cat  == null)
            {
                return NotFound();
            }
            return View(cat);
        }





        [HttpPost]
        public IActionResult Edit(Category category)
        {
            if(category == null)
            {
                return NotFound();
            }
			if (int.TryParse(category.Name, out _) || float.TryParse(category.Name, out _))
			{
				ModelState.AddModelError("Name", "Can not use Numeric Value as Name!");
			}

			if (ModelState.IsValid)
			{
				_context.Update(category);
				_context.Save();
                TempData["success"] = $"Category {category.Name} update successfully!";
                return RedirectToAction("Index");
			}
			return View();
        }




        //Delete Controller
        [HttpGet]
        public IActionResult Delete(int? id)
        {
            if(id== null)
            {
                return NotFound();
            }
            Category? ca = _context.Get(d => d.Id == id);
            if(ca == null)
            {
                return NotFound();
            }

            return View(ca);
        }

        [HttpPost,ActionName("Delete")]
        public IActionResult DeleteCategory(int? id)
        {
            if(id == null)
            {
                NotFound();
            }
            Category? c = _context.Get(x => x.Id ==id);

            if(c == null)
            {
                return NotFound();
            }
            _context.Remove(c);
            _context.Save();
            TempData["success"] = $"Category [{c.Name}] deleted successfully!";

            return RedirectToAction("Index");
        }
    }
}
