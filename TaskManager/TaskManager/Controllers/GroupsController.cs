using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TaskManager.DAL;
using System.Data.Entity.Infrastructure;
using DbUpdateException = System.Data.Entity.Infrastructure.DbUpdateException;

namespace TaskManager.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GroupsController : ControllerBase
    {
        private readonly DataBaseContext _context;

        public GroupsController(DataBaseContext context)
        {
            _context = context;
        }

        [HttpGet, ActionName("Get")]
        [Route("Get")]
        public async Task<ActionResult<IEnumerable<DAL.Entities.Group>>> GetGroups()
        {
            var groups = await _context.Groups.ToListAsync(); // Select * From Groups

            if (groups == null) return NotFound();

            return groups;
        }

        [HttpGet, ActionName("Get")]
        [Route("Get/{id}")]
        public async Task<ActionResult<DAL.Entities.Group>> GetGroupById(Guid? id)
        {
            var group = await _context.Groups.FirstOrDefaultAsync(c => c.Id == id);

            if (group == null) return NotFound();

            return Ok(group);
        }

        [HttpPost, ActionName("Create")]
        [Route("Create")]
        public async Task<ActionResult> CreateGroup(DAL.Entities.Group group)
        {
            try
            {
                group.Id = Guid.NewGuid();
                group.CreatedDate = DateTime.Now;

                _context.Groups.Add(group);
                await _context.SaveChangesAsync(); // Aquí es donde se hace el Insert Into...
            }
            catch (DbUpdateException dbUpdateException)
            {
                if (dbUpdateException.InnerException.Message.Contains("duplicate"))
                    return Conflict(String.Format("{0} ya existe", group.Name));
            }
            catch (Exception ex)
            {
                return Conflict(ex.Message);
            }

            return Ok(group);
        }

        [HttpPut, ActionName("Edit")]
        [Route("Edit/{id}")]
        public async Task<ActionResult> EditGroup(Guid? id, DAL.Entities.Group group)
        {
            try
            {
                if (id != group.Id) return NotFound("Group not found");

                group.ModifiedDate = DateTime.Now;

                _context.Groups.Update(group);
                await _context.SaveChangesAsync(); // Aquí es donde se hace el Update...
            }
            catch (DbUpdateException dbUpdateException)
            {
                if (dbUpdateException.InnerException.Message.Contains("duplicate"))
                    return Conflict(String.Format("{0} ya existe", group.Name));
            }
            catch (Exception ex)
            {
                return Conflict(ex.Message);
            }

            return Ok(group);
        }

        [HttpDelete, ActionName("Delete")]
        [Route("Delete/{id}")]
        public async Task<ActionResult> DeleteGroup(Guid? id)
        {
            if (_context.Groups == null) return Problem("Entity set 'DataBaseContext.Groups' is null.");
            var group = await _context.Groups.FirstOrDefaultAsync(c => c.Id == id);

            if (group == null) return NotFound("Group not found");

            _context.Groups.Remove(group);
            await _context.SaveChangesAsync(); //Hace las veces del Delete en SQL

            return Ok(String.Format("El grupo {0} fue eliminado!", group.Name));
        }
    }
}
