using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace los_api.Controllers
{
    [Route("api/[controller]")]
    public class ProductController : Controller
    {
        private readonly ApiContext _context;
        public ProductController(ApiContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Get()
        {
            Product[] product = await _context.Products.Include(u => u.Stocks).ToArrayAsync();
            var response = product.Select(u => new
            {
                Id = u.Id,
                Name = u.Name,
                ImageURL = u.ImageURL,
                Price = u.Price,
                Stocks = u.Stocks.Select(p => p.ProductId)
            });
            return Ok(response);
        }
        [HttpPost]
        public async Task<IActionResult> Create(Product product)
        {
            try
            {
                if (product != null)
                {
                    if (await _context.Products.FindAsync(product.Id) == null)
                    {
                        await _context.AddAsync(product);
                        _context.SaveChanges();
                    }
                }
                return Ok(product);
            }
            catch(Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        [HttpPost]
        public async Task<IActionResult> Update(Product product)
        {
            try
            {
                var getProduct = await _context.Products.FindAsync(product.Id);
                if (getProduct != null)
                {
                    _context.Products.Update(product);
                    _context.SaveChanges();
                }
                return Ok(product);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        [HttpDelete]
        public async Task<IActionResult> Delete(int productId)
        {
            try
            {
                var getProduct = await _context.Products.FindAsync(productId);
                if(getProduct != null)
                {
                    _context.Products.Remove(getProduct);
                    _context.SaveChanges();
                    return Ok(true);
                }
                return Ok(false);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }


    }
}
