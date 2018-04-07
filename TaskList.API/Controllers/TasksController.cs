using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using TaskList.API.Models;

namespace TaskList.API.Controllers
{

    [ValidateModel]
    [Route("api/[controller]")]
    public class TasksController : Controller
    {
        public TasksController()
        {

        }

        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        [HttpPost]
        public void Post([FromBody]TaskApiModel task)
        {
        }

        [HttpPut("{id}")]
        public void Put(Guid id, [FromBody]TaskApiModel task)
        {
        }


        [HttpDelete("{id}")]
        public void Delete(Guid id)
        {
        }
    }
}