using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using WebAPPPlayground.Models;

namespace WebAPPPlayground.Data
{
    public class WebAPPPlaygroundContext : DbContext
    {
        public WebAPPPlaygroundContext (DbContextOptions<WebAPPPlaygroundContext> options)
            : base(options)
        {
        }

        public DbSet<WebAPPPlayground.Models.Movie> Movie { get; set; } = default!;
    }
}
