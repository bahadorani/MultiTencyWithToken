using Sample.Domain.Models;
using Sample.Persistence.Context;

namespace Sample.Application.Services
{
    public class ProductService : IProductService
    {
        private MultiTenantContext _context;

        public ProductService(MultiTenantContext context)
        {
            _context = context;
            SeedData();
        }

        public List<Product> GetProduct()
        {
            return _context.Products.ToList();
        }

        private void SeedData()
        {
            if(_context.Products.Count()==0)
            {
                var products = new Product()
                {
                    Caption = "P1"
                };

                _context.Products.Add(products);
                _context.SaveChanges();
            }
        }
    }
}
