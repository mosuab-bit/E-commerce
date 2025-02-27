using API.Controllers;
using API.Entities;
using API.Extensions;
using API.Extentions;
using API.RequestHelpers;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SQLitePCL;

namespace API.Data
{
    
    public class ProductsController : BaseApiController
    {
        private readonly StoreContext context;

        public ProductsController(StoreContext context)
        {
            this.context = context;
        }

        [HttpGet]
        public async Task<ActionResult<List<Product>>> GetAllProducts(
          [FromQuery]ProductParams productParams
        )
        {
            var query = context.Products
            .Sort(productParams.OrderBy)
            .Search(productParams.SearchTerm)
            .Filter(productParams.Brands,productParams.Types)
            .AsQueryable();
            

            var products = await PagedList<Product>.ToPagedList(query,
            productParams.PageNumber,productParams.PageSize);
           
           Response.AddPaginationHeader(products.Metadata);
            
           return products;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Product>> GetProduct(int id)
        {
            var product = await context.Products.FindAsync(id);
            if(product==null)return NotFound();
            return product;
        }
        
        [HttpGet("filters")]

        public async Task<IActionResult> GetFilters()
        {
            var brands = await context.Products.Select(x=>x.Brand).Distinct().ToListAsync();
            var types = await context.Products.Select(x=>x.Type).Distinct().ToListAsync();

            return Ok(new{brands,types});
        }

    }
}
