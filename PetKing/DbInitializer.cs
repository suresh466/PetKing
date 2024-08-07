using System;
using System.Linq;
using PetKing.Models;

namespace PetKing
{
    public static class DbInitializer
    {
        public static void Initialize(PetKingContext context)
        {
            context.Database.EnsureCreated();

            // Look for any products.
            if (context.Products.Any())
            {
                return;   // DB has been seeded
            }

            var categories = new Category[]
            {
                new Category { Name = "Food" },
                new Category { Name = "Toys" },
                new Category { Name = "Accessories" }
            };
            foreach (Category c in categories)
            {
                context.Categories.Add(c);
            }
            context.SaveChanges();

            var products = new Product[]
            {
                new Product { Name = "Dog Food", Description = "Premium dog food", Price = 29.99M, CategoryID = 1, ImageURL = "dogfood.jpg" },
                new Product { Name = "Cat Food", Description = "Delicious cat food", Price = 24.99M, CategoryID = 1, ImageURL = "catfood.jpg" },
                new Product { Name = "Chew Toy", Description = "Durable chew toy for dogs", Price = 9.99M, CategoryID = 2, ImageURL = "chewtoy.jpg" },
                new Product { Name = "Cat Wand", Description = "Interactive cat wand", Price = 7.99M, CategoryID = 2, ImageURL = "catwand.jpg" },
                new Product { Name = "Dog Collar", Description = "Adjustable dog collar", Price = 14.99M, CategoryID = 3, ImageURL = "dogcollar.jpg" },
                new Product { Name = "Cat Bed", Description = "Cozy cat bed", Price = 34.99M, CategoryID = 3, ImageURL = "catbed.jpg" }
            };
            foreach (Product p in products)
            {
                context.Products.Add(p);
            }
            context.SaveChanges();
        }
    }
}