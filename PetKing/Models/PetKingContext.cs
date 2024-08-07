﻿using Microsoft.EntityFrameworkCore;
using PetKing.Models;

namespace PetKing
{
    public class PetKingContext : DbContext
    {
        public PetKingContext(DbContextOptions<PetKingContext> options)
            : base(options)
        {
        }

        public DbSet<Product> Products { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderItem> OrderItems { get; set; }
    }
}