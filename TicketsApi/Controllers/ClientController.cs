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

        /// <summary>
        /// Metodo GET, Deveulve objeto JSON de todos los clientes
        /// </summary>
        /// <response code="200">Respuesta exitosa</response>
        /// <response code="400">Respuesta con error</response>
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
        /// <summary>
        /// Metodo GET, Deveulve objeto JSON de solo un cliente
        /// </summary>
        /// <param name="Id"></param>
        /// <response code="200">Respuesta exitosa</response>
        /// <response code="400">Respuesta con error</response>
        // GET api/<ClientController>/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetForId(int Id)
        {
            try
            {
                using (var _context = new TicketsDBContext(_contextOptions))
                {
                    var client = await _context.Client.SingleOrDefaultAsync(c => c.ClientId == Id);
                    if (client == null) return NotFound();
                    return Ok(client);
                }
            }
            catch (Exception ex)
            {
                return Ok(ex);
            }
        }

        /// <summary>
        /// Metodo POST, recibe el request para crear un nuevo cliente
        /// </summary>
        /// <param name="client"></param>
        /// <response code="200">Registro exitoso</response>
        /// <response code="400">Registro con error</response>
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
                    client.Status = 1;

                    if (_context.Client.Count() == 0)
                    {
                        client.QueueId = 1;
                    }else if (_context.Client.Count() == 1)
                    {
                        client.QueueId = 2;
                    }else if (_context.Client.Count() > 1 && _context.Client.Count() <= 2)
                    {
                        client.QueueId = 1;
                    }else if (_context.Client.Count() > 2)
                    {
                        DateTime now = DateTime.Now;

                        byte queueMinuteCola1 = _context.Queue.FirstOrDefaultAsync(q => q.QueueId == 1).Result.Duration;
                        byte queueMinuteCola2 = _context.Queue.FirstOrDefaultAsync(q => q.QueueId == 2).Result.Duration;

                        DateTime minuteCola1 = DateTime.Now.AddMinutes(Convert.ToDouble(queueMinuteCola1));
                        DateTime minuteCola2 = DateTime.Now.AddMinutes(Convert.ToDouble(queueMinuteCola2));

                        var clientAttendedCola1 = _context.Client.Where(c => c.QueueId == 1).ToArray();
                        var clientAttendedCola2 = _context.Client.Where(c => c.QueueId == 2).ToArray();

                        TimeSpan differenceCola1 = minuteCola1.Subtract(clientAttendedCola1[0].Creation);
                        TimeSpan differenceCola2 = minuteCola2.Subtract(clientAttendedCola2[0].Creation);

                        double totalMinutesCola1 = differenceCola1.TotalMinutes;
                        double totalMinutesCola2 = differenceCola2.TotalMinutes;

                        if (clientAttendedCola1.Count() > clientAttendedCola2.Count())
                        {
                            if (totalMinutesCola1 > totalMinutesCola2)
                            {
                                client.QueueId = 1;
                            }
                            else
                            {
                                client.QueueId = 2;
                            }
                        }else if (clientAttendedCola2.Count() > clientAttendedCola1.Count())
                        {
                            if (totalMinutesCola2 > totalMinutesCola1)
                            {
                                client.QueueId = 2;
                            }
                            else
                            {
                                client.QueueId = 1;
                            }
                        }
                        else
                        {
                            if (totalMinutesCola1 < totalMinutesCola2)
                            {
                                client.QueueId = 1;
                            }
                            else
                            {
                                client.QueueId = 2;
                            }
                        }
                    }

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
    }
}
