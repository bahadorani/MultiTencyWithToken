﻿using Microsoft.EntityFrameworkCore;
using Sample.Domain.Models;
namespace Sample.Persistence.Context
{
    public class MultiTenantContext :DbContext
    {
        public MultiTenantContext(DbContextOptions<MultiTenantContext> options)
            : base(options)
        {
        }

        public DbSet<Product> Products { get; set; } = default!;
    }
}
