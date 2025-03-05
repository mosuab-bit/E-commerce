using API.Controllers;
using API.DTOs;
using API.Entities;
using API.Extensions;
using API.Extentions;
using API.RequestHelpers;
using API.Services;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.Data
{
    
    public class ProductsController : BaseApiController
    {
        private readonly StoreContext context;
        private readonly IMapper mapper;
        private readonly ImageService imageService;

        public ProductsController(StoreContext context,IMapper mapper,ImageService imageService)
        {
            this.context = context;
            this.mapper = mapper;
            this.imageService = imageService;
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

        [Authorize(Roles = "Admin")]
        public async Task<ActionResult<Product>> CreateProduct (CreateProductDto productDto)
        {
                var product = mapper.Map<Product>(productDto);

                if(productDto.File!=null)
                {
                    var imageResult = await imageService.AddImageAsync(productDto.File);
                    if(imageResult.Error !=null)
                    {
                        return BadRequest(imageResult.Error.Message);
                    }

                    product.PictureUrl = imageResult.SecureUrl.AbsoluteUri;
                    product.PublicId = imageResult.PublicId;
                }
                context.Products.Add(product);
                
                var result = await context.SaveChangesAsync() > 0;
                if(result)return CreatedAtAction(nameof(GetProduct),new {Id = product.Id},product);

                return BadRequest("Problem creating new product");
        }

        [Authorize(Roles = "Admin")]
        [HttpPut]
        public async Task<ActionResult> UpdateProduct(UpdateProductDto updateProductDto)
        {
            var product = await context.Products.FindAsync(updateProductDto.Id);
            if(product == null)return NotFound();
            mapper.Map(updateProductDto,product);

            if(updateProductDto.File!=null)
            {
                var imageResult = await imageService.AddImageAsync(updateProductDto.File);
                if(imageResult.Error!=null)return BadRequest(imageResult.Error.Message);
                
                if(!string.IsNullOrEmpty(product.PublicId))
                await imageService.DeleteImageAsync(product.PublicId);

                product.PictureUrl = imageResult.SecureUrl.AbsoluteUri;
                    product.PublicId = imageResult.PublicId;
            }
            var result = await context.SaveChangesAsync() > 0;
            if(result)return NoContent();
            
            return BadRequest("Problem updating product");
        }

        [Authorize(Roles = "Admin")]
        [HttpDelete("{id:int}")]
        public async Task<ActionResult> DeleteProduct(int id)
        {
            var product = await context.Products.FindAsync(id);
            if(product == null)return NotFound();
            
             if(!string.IsNullOrEmpty(product.PublicId))
                await imageService.DeleteImageAsync(product.PublicId);
                
            context.Products.Remove(product);

            var result = await context.SaveChangesAsync() > 0;
            if(result)return Ok();

            return BadRequest("Problem deleting the product");
        }

    }
}
