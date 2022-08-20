using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.DTO;
using API.data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.Controllers
{
    public class TaskController : BaseController
    {
        private readonly DataContext _context;

        public TaskController(DataContext context)
        {
            _context = context;
        }

        [HttpPost("AddTask")]
        public async Task<ActionResult<entities.Task>>
        AddTask(AddTaskDTO AddTaskDTO)
        {
            entities.Task task =
                new entities.Task {
                    UserId = AddTaskDTO.UserId,
                    Title = AddTaskDTO.Title,
                    Description = AddTaskDTO.Description,
                    Date = AddTaskDTO.Date
                };
            _context.Tasks.Add (task);
            await _context.SaveChangesAsync();
            return _context.Tasks.Find(task.Id);
        }

        [HttpGet("{UserId}", Name = "GetTasksByUserId")]
        public IQueryable<entities.Task> GetTasksByUserId(int UserId)
        {
            return _context
                .Tasks
                .FromSqlRaw
                <entities.Task
                >($"Select * FROM Tasks WHERE Tasks.UserId={UserId}");
        }

        [HttpGet("{UserId}/{Date}", Name = "GetTasksByUserIdAndDate")]

        public IQueryable<entities.Task> GetTasksByUserIdAndDate(int UserId, int Date){
            return _context
                .Tasks
                .FromSqlRaw
                <entities.Task
                >($"Select * FROM Tasks WHERE Tasks.UserId={UserId} AND Tasks.Date={Date}");
        }
    }
}
