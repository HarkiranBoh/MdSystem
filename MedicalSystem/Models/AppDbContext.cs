using MedicalSystem.Authentication;
using MedicalSystem.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MedicalSystem.Models
{
    public class AppDbContext : IdentityDbContext<ApplicationUser>
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {

        }
        public DbSet<Equipment> Equipment { get; set; }
        public DbSet<Feedback> Feedbacks { get; set; }
        public DbSet<ShoppingCartItem> ShoppingCartItems { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderDetail> OrderDetails { get; set; }
        public DbSet<Image> Image { get; set; }
        public DbSet<Hospital> Hospital { get; set; }
        public DbSet<HospitalUsers> HospitalUser { get; set; }
        public DbSet<Stock> StockSupplier { get; set; }
    }


}
