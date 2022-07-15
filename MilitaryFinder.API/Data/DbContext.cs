using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using MilitaryFinder.API.Domain;
using System;
using System.Collections.Generic;
using System.Text;

namespace MilitaryFinder.API.Data
{
    public class DataContext : IdentityDbContext
    {
        public DataContext(DbContextOptions<DataContext> options)
            : base(options)
        {
        }

        public DbSet<FighterAircraft> FighterAircraft { get; set; }
    }
}
