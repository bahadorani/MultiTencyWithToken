using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Sample.Application.Services;
using Sample.Domain.Models;
using Sample.Persistence.Context;

namespace Sample.Api.Controllers
{
    
    [ApiController]
    public class ValuesController : ControllerBase
    {
        private IUserService _userService;
        private IProductService _productService;
        private readonly MultiTenantContext _context;

        public ValuesController(IUserService userService, IDbContextFactory dbContextFactory, IHttpContextAccessor httpContextAccessor)
        {
            var tenant = httpContextAccessor.HttpContext.Items["CurrentTenant"] as Tenant;
            _context = dbContextFactory.CreateDbContext(tenant);
        
            _userService = userService;
        }

        [HttpGet]
        [Route("api/[controller]/getusers")]
        public IActionResult GetUser()
        { 
            var result = _userService.GetUsers();
            return Ok(result);
        }
        [HttpGet]
        [Route("api/[controller]/getproduct")]
        public IActionResult GetProduct()
        {
            _productService = new ProductService(_context);
            var result = _productService.GetProduct();
            return Ok(result);
        }
    }
}
