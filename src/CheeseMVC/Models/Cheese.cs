using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace CheeseMVC.Models
{
    public class Cheese
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public CheeseCategory Category { get; set; }
        public int ID { get; set; }
        public int CategoryID { get; set; }

        public List<CheeseMenu> CheeseMenus { get; set; }

       
        
    }
}

//  internal static void Add(SelectListItem selectListItem)

//  throw new NotImplementedException();
//internal public static implicit operator Cheese(Cheese);
         
      //   throw   new NotImplementedException();