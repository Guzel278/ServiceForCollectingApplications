using System;
using Microsoft.EntityFrameworkCore;
using ServiceForCollectingApplications.Models;

namespace ServiceForCollectingApplications.DataAccess
{
    public class ConferenceDbContext : DbContext
    {
        public ConferenceDbContext(DbContextOptions<ConferenceDbContext> options)
            : base(options)
        {
        }

        public DbSet<Participants> Participants { get; set; }
        public DbSet<Proposals> Proposals { get; set; }
    }
}

