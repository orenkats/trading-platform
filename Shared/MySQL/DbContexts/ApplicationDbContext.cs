using Microsoft.EntityFrameworkCore;
using Shared.Persistence;

namespace Shared.MySQL
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

    }
}
