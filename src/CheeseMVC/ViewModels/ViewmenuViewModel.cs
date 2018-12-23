using System;
using System.Collections.Generic;
using System.Linq;
using CheeseMVC.Models;

namespace CheeseMVC.ViewModels
{
    public class ViewmenuViewModel
    {
        public IList<CheeseMenu> Items { get; set; }
        public Menu Menu { get; set; }
    }
}
