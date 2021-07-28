using Microsoft.AspNetCore.Cors;
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

        /// <summary>
        /// Metodo GET, Deveulve objeto JSON de Queue con Clientes
        /// </summary>
        /// <response code="200">Respuesta exitosa</response>
        /// <response code="400">Respuesta con error</response>
        // GET: api/<QueueController>
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                using (var _context = new TicketsDBContext(_contextOptions))
                {
                    var queues = await _context.Queue.ToListAsync();
                    foreach (var item in queues)
                    {
                        item.Client = await _context.Client.Where(c => c.QueueId == item.QueueId).ToListAsync();
                    }
                    if (queues == null) return NotFound();
                    return Ok(queues);
                }
            }
            catch (Exception ex)
            {
                return Ok(ex);
            }
        }
        /// <summary>
        /// Metodo POST, recibe el request para crear un nuevo Queue
        /// </summary>
        /// <param name="queue"></param>
        /// <response code="200">Respuesta exitosa</response>
        /// <response code="400">Respuesta con error</response>
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
