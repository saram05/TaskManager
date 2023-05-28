using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TaskManager.DAL;
using TaskManager.DAL.Entities;
using DbUpdateException = Microsoft.EntityFrameworkCore.DbUpdateException;

namespace TaskManager.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProjectsController : ControllerBase
    {
        private readonly DataBaseContext _context;
        public ProjectsController(DataBaseContext context)
        {
            _context = context;
        }

        [HttpGet, ActionName("Get")]
        [Route("Get")]
        public async Task<ActionResult<IEnumerable<Project>>> GetProjects()
        {
            var projects = await _context.Projects.ToListAsync(); // Select * From Projects

            if (projects == null) return NotFound();

            return projects;
        }

        [HttpGet, ActionName("Get")]
        [Route("Get/{id}")]
        public async Task<ActionResult<Project>> GetProjectById(Guid? id)
        {
            var project = await _context.Projects.FirstOrDefaultAsync(c => c.Id == id);

            if (project == null) return NotFound();

            return Ok(project);
        }

        [HttpPost, ActionName("Create")]
        [Route("Create")]
        public async Task<ActionResult> CreateProject(Project project)
        {
            try
            {
                project.Id = Guid.NewGuid();
                project.CreatedDate = DateTime.Now;

                _context.Projects.Add(project);
                await _context.SaveChangesAsync(); //  Insert Into...
            }
            catch (DbUpdateException dbUpdateException)
            {
                if (dbUpdateException.InnerException.Message.Contains("duplicate"))
                    return Conflict(String.Format("{0} ya existe", project.Name));
            }
            catch (Exception ex)
            {
                return Conflict(ex.Message);
            }

            return Ok(project );
        }

        [HttpPut, ActionName("Edit")]
        [Route("Edit")]
        public async Task<ActionResult> EditProject(Guid id, Project project)
        {
            try
            {
                if (id != project.Id) return NotFound("Project not found");               
                
                project.ModifiedDate = DateTime.Now;

                _context.Projects.Update(project);
                await _context.SaveChangesAsync(); //  update...
            }
            catch (DbUpdateException dbUpdateException)
            {
                if (dbUpdateException.InnerException.Message.Contains("duplicate"))
                    return Conflict(String.Format("{0} ya existe", project.Name));
            }
            catch (Exception ex)
            {
                return Conflict(ex.Message);
            }

            return Ok(project);
        }

        [HttpDelete, ActionName("Delete")]
        [Route("Delete/{id}")]
        public async Task<ActionResult> DeleteProject(Guid? id)
        {
            if (_context.Projects == null) return Problem("Entity set 'DataBaseContext.Projects' is null.");
            var project = await _context.Projects.FirstOrDefaultAsync(c => c.Id == id);

            if (project == null) return NotFound("Project not found");

            _context.Projects.Remove(project);
            await _context.SaveChangesAsync(); // Delete in SQL

            return Ok(String.Format("El proyecto {0} fue eliminado!", project.Name));
        }


    }
}
