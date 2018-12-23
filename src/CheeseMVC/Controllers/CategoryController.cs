using System.Collections.Generic;
using System.Linq;
using CheeseMVC.Data;
using CheeseMVC.Models;
using CheeseMVC.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace CheeseMVC.Controllers
{
    public class CategoryController : Controller

    {
        private readonly CheeseDbContext context;  

        public CategoryController(CheeseDbContext dbContext)
        {
            context = dbContext;
        }

        public int CheeseCatagories { get; private set; }

        public IActionResult Index()
        {//retrieve all cheeses stored in db - db object are  not lists - turned into list with ToList command
            List<CheeseCategory> categories = context.Categories.ToList();
            return View(categories);
        }

        public IActionResult Add()
        {
            AddCategoryViewModel addCategoryViewModel = new AddCategoryViewModel();
            return View(addCategoryViewModel);
        }

        [HttpPost]
        public IActionResult Add(AddCategoryViewModel addCategoryViewModel)
        {
            if (ModelState.IsValid)
            {
                // Add the new category to categories
                CheeseCategory newCategory = new CheeseCategory
             
                {
                    Name = addCategoryViewModel.Name
                   
                };
                context.Categories.Add(newCategory);
                //must save changes made to database
                context.SaveChanges();

                return Redirect("/Category");
            }

            return View(addCategoryViewModel);
        }

        
        }

    }



