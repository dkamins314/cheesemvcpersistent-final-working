using CheeseMVC.Models;
using Microsoft.EntityFrameworkCore;

namespace CheeseMVC.Data
{
    public class CheeseDbContext : DbContext
    {
        //properties that we want to persist
        public DbSet<Cheese> Cheeses { get; set; }

        public DbSet<CheeseCategory> Categories { get; set; }

        public DbSet<Menu> Menus { get; set; }

        public DbSet<CheeseMenu> CheeseMenus { get; set; }

        // public object Category { get; internal set; }

        //db constructor
        public CheeseDbContext(DbContextOptions<CheeseDbContext> options)
            // extends base constructor
            : base(options)

        { }  
            protected override void OnModelCreating(ModelBuilder modelBuilder)
          {
            modelBuilder.Entity<CheeseMenu>()
             .HasKey(c=> new {c.CheeseID, c.MenuID});
            }

    }
}
//setting up composite primary key associated with CheeseMenu class and Menu class - 

// overriding modelBuilder will allow customization of object relational model and determine 
//primary key for cheese menu class
//this is allowing cheeseMenu primary key to be a composite key comprised of CheeseID+MenuID
