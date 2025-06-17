using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Product_WebAPI_App.Data;
using Product_WebAPI_App.Models;
using System.Collections.Generic;
using System.Linq;

namespace Product_WebAPI_App.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductController : ControllerBase
    {
        private readonly ProductDbContext _context;
        public ProductController(ProductDbContext context)
        {
            _context = context;
        }

        // GET: api/Product
        [HttpGet]
        public ActionResult<IEnumerable<Product>> GetProducts()
        {
            return Ok(_context.Products.ToList());
        }

        // GET: api/Product/5
        [HttpGet("{id}")]
        public ActionResult<Product> GetProduct(int id)
        {
            var product = _context.Products.FirstOrDefault(p => p.Id == id);
            if (product == null)
            {
                return NotFound();
            }
            return Ok(product);
        }

        // POST: api/Product
        [HttpPost]
        public ActionResult<Product> CreateProduct([FromBody] Product product)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                _context.Products.Add(product);
                _context.SaveChanges();
                return CreatedAtAction(nameof(GetProduct), new { id = product.Id }, product);
            }
            catch (DbUpdateException ex)
            {
                // Handle database-related errors (e.g., unique constraint violations)
                return StatusCode(StatusCodes.Status500InternalServerError,
                    new
                    {
                        error = "A database error occurred while creating the product.",
                        details = ex.InnerException?.Message
                    });
            }
            catch (Exception ex)
            {
                // Handle any other unexpected errors
                return StatusCode(StatusCodes.Status500InternalServerError,
                    new
                    {
                        error = "An unexpected error occurred.",
                        details = ex.Message
                    });
            }
        }

        // PUT: api/Product/5
        [HttpPut("{id}")]
        public ActionResult UpdateProduct(int id, [FromBody] Product updatedProduct)
        {
            var product = _context.Products.FirstOrDefault(p => p.Id == id);
            if (product == null)
            {
                return NotFound();
            }

            product.Name = updatedProduct.Name;
            product.Price = updatedProduct.Price;
            product.Category = updatedProduct.Category;
            return NoContent();
        }

        // DELETE: api/Product/5
        [HttpDelete("{id}")]
        public ActionResult DeleteProduct(int id)
        {
            var product = _context.Products.FirstOrDefault(p => p.Id == id);
            if (product == null)
            {
                return NotFound();
            }

            _context.Products.Remove(product);
            return NoContent();
        }
    }
}