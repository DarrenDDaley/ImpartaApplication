using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TaskList.API.Database;
using TaskList.API.Models;

namespace TaskList.API.Controllers
{

    [ValidateModel]
    [Route("api/[controller]")]
    public class TasksController : Controller
    {
        private readonly ITaskDbContext<TaskItem> context;

        public TasksController(ITaskDbContext<TaskItem> context)
        {
            this.context = context ?? throw new ArgumentNullException(nameof(context)); 
        }

        [HttpGet]
        public async Task<IEnumerable<TaskItem>> Get()
        {
            return await context.Tasks.ToListAsync();
        }

        [HttpPost("add")]
        public async Task<IActionResult> Post([FromBody]TaskItemAPI task)
        {
            var taskItem = new TaskItem()
            {
                Done = task.Done,
                Id = Guid.NewGuid(),
                Description = task.Description
            };

            context.Add(taskItem);
            await context.SaveChangesAsync();

            return Ok(taskItem.Id);
        }

        [HttpPut("edit/{id}")]
        public async Task<IActionResult> Put(Guid id, [FromBody]TaskItemAPI task)
        {
            if (!context.Tasks.Any(t => t.Id == id)) {
                return NotFound();
            }

            var taskItem = new TaskItem()
            {
                Done = task.Done,
                Id = Guid.NewGuid(),
                Description = task.Description
            };

            context.Update(taskItem);
            await context.SaveChangesAsync();
            return Ok();
        }

        [HttpPut("done/{id}")]
        public async Task<IActionResult> Put(Guid id, bool done)
        {
            var taskItem = await context.Tasks.SingleOrDefaultAsync(t => t.Id == id);

            if (taskItem == null) {
                return NotFound();
            }

            taskItem.Done = done;

            context.Update(taskItem);
            await context.SaveChangesAsync();
            return Ok();
        }

        [HttpDelete("delete/{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var taskItem = await context.Tasks.SingleOrDefaultAsync(t => t.Id == id);

            if(taskItem == null) {
                return NotFound();
            }

            context.Remove(taskItem);
            await context.SaveChangesAsync();
            return Ok();
        }
    }
}