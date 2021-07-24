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
    public class ClientController : ControllerBase
    {
        private readonly DbContextOptions<TicketsDBContext> _contextOptions;
        public ClientController(DbContextOptions<TicketsDBContext> contextOptions)
        {
            _contextOptions = contextOptions;
        }

        // GET: api/<ClientController>
        [HttpGet]
        public async Task<IActionResult> GetAll()
        { 
            try
            {
                using (var _context = new TicketsDBContext(_contextOptions))
                {
                    var clients = await _context.Client.ToListAsync();
                    if (clients == null) return NotFound();
                    return Ok(clients);
                }
            }
            catch (Exception ex)
            {
                return Ok(ex);
            }
        }

        // GET api/<ClientController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        /// <summary>
        /// Metodo POST, recibe el request para crear un nuevo cliente
        /// </summary>
        /// <param name="client"></param>
        /// <response code="200">Registro exitoso</response>
        /// <response code="400">Registro exitoso con error</response>
        // POST: api/<ClientController>
        [HttpPost]
        public async Task<IActionResult> Create(Client client)
        {
            if (!ModelState.IsValid) 
            {
                return BadRequest();
            }
            try
            {
                using (var _context = new TicketsDBContext(_contextOptions))
                {
                    _context.Client.Add(client);
                    await _context.SaveChanges();
                    return Ok(client.ClientId);
                }
            }
            catch (Exception ex)
            {
                return Ok(ex);
            }
        }

        // PUT api/<ClientController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<ClientController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
