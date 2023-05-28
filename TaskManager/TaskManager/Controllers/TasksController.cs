using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TaskManager.DAL;
using TaskManager.DAL.Entities;
using DbUpdateException = Microsoft.EntityFrameworkCore.DbUpdateException;

namespace TaskManager.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TasksController : ControllerBase
    {
        private readonly DataBaseContext _context;
        public TasksController(DataBaseContext context)
        {
            _context = context;
        }

        [HttpGet, ActionName("Get")]
        [Route("Get")]
        public async Task<ActionResult<IEnumerable<TaskManager.DAL.Entities.Task>>> GetTasks()
        {
            var tasks = await _context.Tasks.ToListAsync(); // Select * From Tasks

            if (tasks == null) return NotFound();

            return tasks;
        }

        [HttpGet, ActionName("Get")]
        [Route("Get/{id}")]
        public async Task<ActionResult<TaskManager.DAL.Entities.Task>> GetTaskById(Guid? id)
        {
            var task = await _context.Tasks.FirstOrDefaultAsync(c => c.Id == id);

            if (task == null) return NotFound();

            return Ok(task);
        }

        [HttpPost, ActionName("Create")]
        [Route("Create")]
        public async Task<ActionResult> CreateTask(TaskManager.DAL.Entities.Task Task, Guid listTaskId, Guid userId)
        {
            try
            {
                Task.Id = Guid.NewGuid();
                Task.CreatedDate = DateTime.Now;

                Task.IdList = listTaskId;
                Task.List = await _context.ListTasks.FirstOrDefaultAsync(c => c.Id == listTaskId);
                Task.IdUser = userId;
                Task.User = await _context.Users.FirstOrDefaultAsync(c => c.Id == userId);

                _context.Tasks.Add(Task);
                await _context.SaveChangesAsync(); //  Insert Into...
            }
            catch (DbUpdateException dbUpdateException)
            {
                if (dbUpdateException.InnerException.Message.Contains("duplicate"))
                    return Conflict(String.Format("{0} ya existe", Task.Description));
            }
            catch (Exception ex)
            {
                return Conflict(ex.Message);
            }

            return Ok(Task);
        }

        [HttpPut, ActionName("Edit")]
        [Route("Edit")]
        public async Task<ActionResult> EditTask(Guid id, DAL.Entities.Task Task)
        {
            try
            {
                if (id != Task.Id) return NotFound("Task not found");               
                
                Task.ModifiedDate = DateTime.Now;
                Task.StartDate = Task.StartDate;
                Task.FinishDate = Task.FinishDate;
                Task.Status = Task.Status;
                Task.Notes = Task.Notes;

                _context.Tasks.Update(Task);
                await _context.SaveChangesAsync(); //  update...
            }
            catch (DbUpdateException dbUpdateException)
            {
                if (dbUpdateException.InnerException.Message.Contains("duplicate"))
                    return Conflict(String.Format("{0} ya existe", Task.Description));
            }
            catch (Exception ex)
            {
                return Conflict(ex.Message);
            }

            return Ok(Task);
        }

        [HttpDelete, ActionName("Delete")]
        [Route("Delete/{id}")]
        public async Task<ActionResult> DeleteTask(Guid? id)
        {
            if (_context.Tasks == null) return Problem("Entity set 'DataBaseContext.Tasks' is null.");
            var task = await _context.Tasks.FirstOrDefaultAsync(c => c.Id == id);

            if (task == null) return NotFound("Task not found");

            _context.Tasks.Remove(task);
            await _context.SaveChangesAsync(); // Delete in SQL

            return Ok(String.Format("El proyecto {0} fue eliminado!", task.Description));
        }


    }
}
