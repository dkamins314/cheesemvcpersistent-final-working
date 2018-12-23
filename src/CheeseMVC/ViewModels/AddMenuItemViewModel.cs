using CheeseMVC.Models;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace CheeseMVC.ViewModels
{
    public class AddMenuItemViewModel
    {
        public Menu Menu { get; set; }
        public List<SelectListItem> Cheeses { get; set; } 

        public int MenuID { get; set; }
        public int CheeseID { get; set; }

        public AddMenuItemViewModel() { }

        public AddMenuItemViewModel(Menu menu, IEnumerable<Cheese> cheeses)
        {
             Menu = menu;
                   Cheeses = new List<SelectListItem>();

                    foreach(var item in cheeses)
                   {
                         Cheeses.Add(new SelectListItem
                       {
                           Text = item.Name,
                           Value = item.ID.ToString()
                           
                       });
                   }
                  
        }
    }
    

} 
