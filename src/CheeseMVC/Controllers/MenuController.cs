using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using CheeseMVC.ViewModels;
using CheeseMVC.Models;
using CheeseMVC.Data;

namespace CheeseMVC.Controllers
{
    public class MenuController : Controller
    {
        private readonly CheeseDbContext context;

        public MenuController(CheeseDbContext dbContext)
        {
            context = dbContext;
        }
        // GET: //<controllers>/
        public ActionResult Index()
        {
            List<Menu> menus = context.Menus.ToList();
            return View(menus);
        }

        // GET: Menu/Details/5
        public ActionResult Add()
        {
            AddMenuViewModel addMenuViewModel = new AddMenuViewModel();
            return View(addMenuViewModel);
        }

        [HttpPost]
        public IActionResult Add(AddMenuViewModel addMenuViewModel)
        {
            if (ModelState.IsValid)
            {
                Menu newMenu = new Menu
                {
                    Name = addMenuViewModel.Name
                };
                context.Menus.Add(newMenu);
                context.SaveChanges();

                return Redirect("/Menu/ViewMenu/" + newMenu.ID);
            }
            return View(addMenuViewModel);
        }

        public IActionResult ViewMenu(int id)
        {
            List<CheeseMenu> items = context
                   .CheeseMenus
                   .Include(item => item.Cheese)
                   .Where(cm => cm.MenuID == id)
                   .ToList();

            Menu menu = context.Menus.Single(m => m.ID == id);

            ViewmenuViewModel viewModel = new ViewmenuViewModel
            {
                Menu = menu,
                Items = items
            };

            return View(viewModel);

        }

        public IActionResult AddMenuItem(int id)
        {
            Menu menu = context.Menus.Single(m => m.ID == id);
            List<Cheese> cheeses = context.Cheeses.ToList();
            return View(new AddMenuItemViewModel(menu, cheeses));
        }


        [HttpPost]
        public IActionResult AddMenuItem(AddMenuItemViewModel addMenuItemViewModel)
        {
            if (ModelState.IsValid)
            {
                var cheeseID = addMenuItemViewModel.CheeseID;
                var menuID = addMenuItemViewModel.MenuID;

                IList<CheeseMenu> existingItems = context.CheeseMenus
                    .Where(cm => cm.CheeseID == cheeseID)
                    .Where(cm => cm.MenuID == menuID).ToList();

                if (existingItems.Count == 0)
                {
                    CheeseMenu newCheesemenu = new CheeseMenu
                    {
                        CheeseID = cheeseID, 
                        MenuID = menuID
                    };
                    context.CheeseMenus.Add(newCheesemenu);
                    context.SaveChanges();
                     return Redirect(string.Format ("/Menu/ViewMenu/ {0}" , newCheesemenu.MenuID));
                    
                    }
                 return Redirect(string.Format("/Menu/ViewMenu/{0}", addMenuItemViewModel.MenuID));
                }
                    return Redirect("/Menu/Index/");
        }  
                     
        }
    }
   

                
