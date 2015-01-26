using ProductsCatalog.Models;
using System.Collections.Generic;
using System.Data.Entity.Migrations;

namespace ProductsCatalog.Data.Migrations
{
    public sealed class Configuration : DbMigrationsConfiguration<ApplicationDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
            AutomaticMigrationDataLossAllowed = true;
        }

        protected override void Seed(ApplicationDbContext context)
        {
            context.Categories.AddOrUpdate(
              category => category.Name,
              new Category 
              {
                  Name = "Folding knife",
                  Description = "Folding knives. They are small, they are deadly, but also they are cool.",
                  Products = new List<Product>()
                    {
                        new Product
                            {   
                                Name = "Folding knife with very cool handle in the shape of the head of an eagle",
                                Description= "One very cool folding knife with a handle in the shape of the head of an eagle."+ 
                                "A very important element is the wheel painted on the handle - it gives negligence and contrasts with the "+
                                "brutal masculinity, which is symbolized by the eagle.",
                                ImageName = "Folding-knife-with-eagle-head.jpg"
                            },
                        new Product
                            {
                                Name = "Folding survival knife",
                                Description = "Folding survival knife. Can help you even in the most extreme situations. Combines a "+
                                "knife ... a small knife and a flint. With it in your luggage you can take the most dangerous march.",
                                ImageName = "survaival-folding-knife.jpg"
                            }
                    }
              },
              new Category
              {
                  Name = "Dagger Knife",
                  Description = "Dagger Knifes. They are not so small, they are deadly, but also they are cool.",
                  Products = new List<Product>()
                    {
                        new Product
                            {
                                Name = "Rafaelo's dagger",
                                Description= "This is the dagger, which has been used from Raphael from the Ninja Turtles to fight the evil"+
                                "forces leaded by Crank, Shrioder and his subordinates - the two degenerates.",
                                ImageName = "rafaelo-dagger.jpg"
                            }
                    }
              }
            );

            base.Seed(context);
        }
    }
}
