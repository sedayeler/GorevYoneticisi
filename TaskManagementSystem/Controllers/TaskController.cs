using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TaskManagementSystem.Context;
using TaskManagementSystem.Models.DTOs;
using TaskManagementSystem.Models;

namespace TaskManagementSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TaskController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;
        public TaskController(ApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        //Create
        [HttpPost]
        public async Task<ActionResult> CreateTask([FromBody] TaskCreateDto dto)
        {
            if (dto != null)
            {
                UserTask newTask = _mapper.Map<UserTask>(dto);
                await _context.Tasks.AddAsync(newTask);
                await _context.SaveChangesAsync();

                return Ok("Task Created Successfully!");
            }
            else
            {
                return BadRequest("Try Again!");
            }
        }

        //Get
        [HttpGet]
        public async Task<IEnumerable<TaskGetDto>> GetTask(int userId)
        {
            var tasks = await _context.Tasks
                .Where(t => t.UserId == userId)
                .ToListAsync();
            var convertedTasks = _mapper.Map<IEnumerable<TaskGetDto>>(tasks);

            return convertedTasks;
        }

        //Update
        [HttpPut]
        public async Task<ActionResult> UpdateTask(int taskId, TaskUpdateDto dto)
        {
            if (taskId != null)
            {
                var taskToUpdate = await _context.Tasks.FindAsync(taskId);
                taskToUpdate.Name = dto.Name;
                taskToUpdate.Description = dto.Description;
                taskToUpdate.IsCompleted = dto.IsCompleted;
                _context.SaveChangesAsync();

                return Ok("Task Updated Successfully!");
            }
            else
            {
                return BadRequest($"Task Id {taskId} not found.");
            }
        }

        //Delete
        [HttpDelete]
        public async Task<ActionResult> DeleteTask(int userId, int taskId)
        {
            var taskExists = await _context.Tasks.AnyAsync(t => t.UserId == userId && t.Id == taskId);
            if (taskExists != null)
            {
                var taskToDelete = await _context.Tasks.FindAsync(taskId);
                if (taskToDelete.UserId == userId)
                {
                    _context.Tasks.Remove(taskToDelete);
                    await _context.SaveChangesAsync();

                    return Ok($"Task with ID {taskId} for user with ID {userId} deleted successfully.");
                }
                else
                {
                    return BadRequest("The task with the given ID does not belong to the user with the given ID.");
                }
            }
            else
            {
                return BadRequest($"Task with ID {taskId} for user with ID {userId} not found.");
            }
        }
    }
}

