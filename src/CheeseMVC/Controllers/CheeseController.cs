using System;
using System.Collections.Generic;
using System.Linq;
using CheeseMVC.Data;
using CheeseMVC.Models;
using CheeseMVC.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CheeseMVC.Controllers
{
    public class CheeseController : Controller
    {
        //private field of type DBcontext used to interface with database
        private CheeseDbContext context;

        //constructor that takes instance of DKcontext
        public CheeseController(CheeseDbContext dbContext)
        {
            //set field to  be equal to Db context that is passed in to it
            context = dbContext;
        }

        // GET: /<controller>/
        public IActionResult Index()

        //retrieve all cheeses stored in db - db object are  not lists - turned into list with ToList command
        {
            IList<Cheese> cheeses = context.Cheeses.Include(c => c.Category).ToList();

            //view receiving list
            return View(cheeses);

        }

            public IActionResult Add()
            {
                AddCheeseViewModel addCheeseViewModel =
                    new AddCheeseViewModel(context.Categories.ToList());
                return View(addCheeseViewModel);
            }
        

        [HttpPost]
        public IActionResult Add(AddCheeseViewModel addCheeseViewModel)
        {
            CheeseCategory newCheeseCategory =
                context.Categories.SingleOrDefault(c => c.ID == addCheeseViewModel.CategoryID);

            if (ModelState.IsValid)
            {
               // CheeseCategory newCheeseCategory =
                  //  context.Categories.Single(c => c.ID == addCheeseViewModel.CategoryID);
                // Add the new cheese to my existing cheeses
                Cheese newCheese = new Cheese
                {
                    Name = addCheeseViewModel.Name,
                    Description = addCheeseViewModel.Description,
                    Category = newCheeseCategory
                };

                context.Cheeses.Add(newCheese);
                //must save changes made to database
                context.SaveChanges();

                return Redirect("/Cheese");
            }

            return View(addCheeseViewModel);
        }
        public IActionResult Remove()
        {
            ViewBag.title = "Remove Cheeses";
            ViewBag.cheeses = context.Cheeses.ToList();
            return View();
        }
        [HttpPost]
        public IActionResult Remove(int[] cheeseIds)
        {
            foreach (int cheeseId in cheeseIds)
            {
                Cheese theCheese = context.Cheeses.Single(c => c.ID == cheeseId);
                context.Cheeses.Remove(theCheese);
            }

            context.SaveChanges();

            return Redirect("/");
        }
        
         public IActionResult Category(int id)
        {
        
            if (id==0)
           {
         
           return Redirect ("/Category");
         }
         
         CheeseCategory theCheese= context.Categories
          
              .Single(cat=> cat.ID==id);
          
           ViewBag.title ="Cheese in category:  " + theCheese.Name;

            return View("Index", theCheese.Cheeses);
          }
    }
}