using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using DisplayItems.Models;

namespace DisplayItems.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LogsController : ControllerBase
    {
        private static List<Log> dataBaseInstance = new LocalDB().dbInstance;

        [HttpGet]
        public ActionResult<IEnumerable<Log>> Get()
        {
            return Ok(dataBaseInstance);
        }

        [HttpGet("{id}")]
        [ProducesResponseType(typeof(Log), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<Log> GetById(int id)
        {
            var log = dataBaseInstance.FirstOrDefault(l => l.Id == id);
            if (log == null) return NotFound();

            return Ok(log);
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        public ActionResult<Log> Create(Log log)
        {
            log.Id = GenerateUniqueId();
            Console.WriteLine("He wali line aahe");
            Console.WriteLine(dataBaseInstance.Count);

            log.Created = DateTime.Now;
            dataBaseInstance.Add(log);
            Console.WriteLine(dataBaseInstance.Count);
            Console.WriteLine(dataBaseInstance);
            return CreatedAtAction(nameof(GetById), new { id = log.Id }, log);
        }

        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult Update(int id, Log log)
        {
            var existingLog = dataBaseInstance.FirstOrDefault(l => l.Id == id);
            if (existingLog == null) return NotFound();

            // Update the properties of existingLog with the values from 'log'.
            existingLog.Title = log.Title;
            existingLog.LogType = log.LogType;
            existingLog.Description = log.Description;

            return NoContent();
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult Delete(int id)
        {
            var existingLog = dataBaseInstance.FirstOrDefault(l => l.Id == id);
            if (existingLog == null) return NotFound();
            dataBaseInstance.Remove(existingLog);
            return NoContent();
        }

        private int GenerateUniqueId()
        {
            return dataBaseInstance.Count + 1;
        }
    }
}
