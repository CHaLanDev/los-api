using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace los_api.Controllers
{
    [Route("api/[controller]")]
    public class StockController : Controller
    {
        private readonly ApiContext _context;
        public StockController(ApiContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Get()
        {
            Stock[] stocks = await _context.Stocks.ToArrayAsync();
            var response = stocks.Select(u => new
            {
                Id = u.Id,
                ProducId = u.ProductId,
                Amount = u.Amount
            });
            return Ok(response);
        }

        [HttpPost]
        public async Task<IActionResult> Create(Stock stock)
        {
            try
            {
                if (stock != null)
                {
                    if (await _context.Stocks.FindAsync(stock.Id) == null)
                    {
                        await _context.AddAsync(stock);
                        _context.SaveChanges();
                    }
                }
                return Ok(stock);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        [HttpPost]
        public async Task<IActionResult> Update(Stock stock)
        {
            try
            {
                var getStock = await _context.Stocks.FindAsync(stock.Id);
                if (getStock != null)
                {
                    _context.Stocks.Update(stock);
                    _context.SaveChanges();
                }
                return Ok(stock);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        [HttpDelete]
        public async Task<IActionResult> Delete(int stockId)
        {
            try
            {
                var getStock = await _context.Stocks.FindAsync(stockId);
                if (getStock != null)
                {
                    _context.Stocks.Remove(getStock);
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
