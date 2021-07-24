using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TicketsApi.Models;

namespace TicketsApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class QueueController : ControllerBase
    {
        private readonly DbContextOptions<TicketsDBContext> _contextOptions;
        public QueueController(DbContextOptions<TicketsDBContext> contextOptions)
        {
            _contextOptions = contextOptions;
        }

        // GET: api/<QueueController>
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                using (var _context = new TicketsDBContext(_contextOptions))
                {
                    var queues = await _context.Queue.ToListAsync();
                    if (queues == null) return NotFound();
                    return Ok(queues);
                }
            }
            catch (Exception ex)
            {
                return Ok(ex);
            }
        }
        // POST: api/<QueueController>
        [HttpPost]
        public async Task<IActionResult> Create(Queue queue)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            try
            {
                using (var _context = new TicketsDBContext(_contextOptions))
                {
                    _context.Queue.Add(queue);
                    await _context.SaveChanges();
                    return Ok(queue.QueueId);
                }
            }
            catch (Exception ex)
            {
                return Ok(ex);
            }
        }
    }
}
