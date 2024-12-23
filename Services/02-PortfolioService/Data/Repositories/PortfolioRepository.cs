using Microsoft.EntityFrameworkCore;
using PortfolioService.Data.Entities;
using Shared.Persistence;


namespace PortfolioService.Data.Repositories
{
    public class PortfolioRepository : Repository<Portfolio>, IPortfolioRepository
    {
        public PortfolioRepository(PortfolioDbContext context) : base(context) { }
    }
}
