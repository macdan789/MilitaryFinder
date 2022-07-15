using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using MilitaryFinder.API.Domain;
using System;
using System.Collections.Generic;
using System.Text;

namespace MilitaryFinder.API.Data
{
    //To apply our changes to database we need:
    //  1) Add migrations based on updated code => Add-Migration Name-Of-Migration
    //  2) Update our database to run these migrations live => Update-Database
    public class DataContext : IdentityDbContext
    {
        public DataContext(DbContextOptions<DataContext> options)
            : base(options)
        {
        }

        public DbSet<FighterAircraft> FighterAircraft { get; set; }
    }
}
