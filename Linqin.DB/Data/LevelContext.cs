using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Linqin.DB.Models;

    public class LevelContext : DbContext
    {
        public LevelContext (DbContextOptions<LevelContext> options)
            : base(options)
        {
        }

        public DbSet<Linqin.DB.Models.Level> Level { get; set; } = default!;
    }
