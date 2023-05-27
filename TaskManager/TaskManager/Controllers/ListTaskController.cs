using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TaskManager.DAL;
using TaskManager.DAL.Entities;
using DbUpdateException = Microsoft.EntityFrameworkCore.DbUpdateException;

namespace TaskManager.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ListTaskController : ControllerBase
    {
        private readonly DataBaseContext _context;
        public ListTaskController(DataBaseContext context)
        {
            _context = context;
        }

        //get method to list
        [HttpGet, ActionName("Get")]
        [Route("GetListTasksById/{ListTaskId}")]

        public async Task<ActionResult<ListTask>> GetListTasksById(Guid? ListTaskId)
        {
            var listTask = await _context.ListTasks.FirstOrDefaultAsync(s => s.Id == ListTaskId);

            if (listTask == null) return NotFound();

            return Ok(listTask);
        }

        //  get method to query by project id
        [HttpGet, ActionName("Get")]
        [Route("GetListTaskByCountryId")]
        public async Task<ActionResult<IEnumerable<ListTask>>> GetListTaskByCountryId(Guid? projectId)
        {
            var listTask = await _context.ListTasks
                .Where(s => s.IdProyecto == projectId)
                .ToListAsync();

            if (listTask == null) return NotFound();

            return listTask;
        }

        //post method to create a task list
        [HttpPost, ActionName("Create")]
        [Route("Create")]
        public async Task<ActionResult> CreateListTask(ListTask listTask, Guid projectId)
        {
            try
            {
                listTask.Id = Guid.NewGuid();
                listTask.IdProyecto = projectId;
                listTask.Project = await _context.Projects.FirstOrDefaultAsync(c => c.Id == projectId);


                _context.ListTasks.Add(listTask);
                await _context.SaveChangesAsync(); //  Insert Into...
            }
            catch (DbUpdateException dbUpdateException)
            {
                if (dbUpdateException.InnerException.Message.Contains("duplicate"))
                    return Conflict(String.Format("{0} ya existe {1}", listTask.Name));
            }
            catch (Exception ex)
            {
                return Conflict(ex.Message);
            }

            return Ok(listTask);
        }

        //put method to edit a task list
        [HttpPut, ActionName("Edit")]
        [Route("Edit")]
        public async Task<ActionResult> EditListTask(Guid id, ListTask listTask)
        {
            try
            {
                if (id != listTask.Id) return NotFound("List task not found");

               

                _context.ListTasks.Update(listTask);
                await _context.SaveChangesAsync(); //  update...
            }
            catch (DbUpdateException dbUpdateException)
            {
                if (dbUpdateException.InnerException.Message.Contains("duplicate"))
                    return Conflict(String.Format("{0} ya existe", listTask.Name));
            }
            catch (Exception ex)
            {
                return Conflict(ex.Message);
            }

            return Ok(listTask);
        }

        // delete method to delete a list
        [HttpDelete, ActionName("Delete")]
        [Route("Delete/{id}")]
        public async Task<ActionResult> DeleteListTask(Guid? id)
        {
            if (_context.ListTasks == null) return Problem("Entity set 'DataBaseContext.ListTask' is null.");
            var listTask = await _context.ListTasks.FirstOrDefaultAsync(c => c.Id == id);

            if (listTask == null) return NotFound("List task not found");

            _context.ListTasks.Remove(listTask);
            await _context.SaveChangesAsync(); // Delete in SQL

            return Ok(String.Format("La lista {0} fue eliminada!", listTask.Name));
        }

    }
}
