﻿using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using TelephoneDirectory.Guide.Entities;

namespace TelephoneDirectory.Guide.Data;

public sealed class GuideDbContext : DbContext, IGuideDbContext
{
    public GuideDbContext(DbContextOptions<GuideDbContext> options)
        : base(options)
    {
    }

    public DbSet<Person> Persons { get; set; }
    public DbSet<Contact> Contacts { get; set; }

    public async Task<int> SaveChangesAsync()
    {
        return await base.SaveChangesAsync();
    }
}
