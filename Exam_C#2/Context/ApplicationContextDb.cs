﻿using Exam_C_2.Models;
using Microsoft.EntityFrameworkCore;

namespace Exam_C_2.Context
{
    public class ApplicationContextDb : DbContext
    {
        public ApplicationContextDb(DbContextOptions<ApplicationContextDb> options): base(options) { }

        public DbSet<Customer> Customers { get; set; }
        public DbSet<Rental> Rentals { get; set; }
        public DbSet<RentalDetail> RentalDetails { get; set; }
        public DbSet<ComicBook> ComicBooks { get; set; }
    }
}
