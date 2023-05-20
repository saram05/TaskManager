using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TaskManager.DAL;
using System.Data.Entity.Infrastructure;
using DbUpdateException = System.Data.Entity.Infrastructure.DbUpdateException;
using TaskApp.DAL.Entities;

namespace TaskManager.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly DataBaseContext _context;

        public UsersController(DataBaseContext context)
        {
            _context = context;
        }

        [HttpGet, ActionName("Get")]
        [Route("GetUsersById/{userId}")]
        public async Task<ActionResult<User>> GetUserById(Guid? userId)
        {
            var user = await _context.Users.FirstOrDefaultAsync(s => s.Id == userId);

            if (user == null) return NotFound();

            return Ok(user);
        }

        [HttpGet, ActionName("Get")]
        [Route("GetUserByGroupId")]
        public async Task<ActionResult<IEnumerable<User>>> GetUsersByGroupId(Guid? GroupId)
        {
            var users = await _context.Users
                .Where(s => s.GroupId == GroupId)
                .ToListAsync();

            if (users == null) return NotFound();

            return users;
        }

        [HttpPost, ActionName("Create")]
        [Route("CreateUser")]
        public async Task<ActionResult> CreateUser(User user, Guid GroupId)
        {
            try
            {
                user.Id = Guid.NewGuid();
                user.CreatedDate = DateTime.Now;
                user.GroupId = GroupId;
                user.Group = await _context.Groups.FirstOrDefaultAsync(c => c.Id == GroupId);
                user.ModifiedDate = null;

                _context.Users.Add(user);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException dbUpdateException)
            {
                if (dbUpdateException.InnerException.Message.Contains("duplicate"))
                    return Conflict(String.Format("{0} ya existe en {1}", user.UserName, user.Group.Name));
            }
            catch (Exception ex)
            {
                return Conflict(ex.Message);
            }

            return Ok(User);
        }

        [HttpPut, ActionName("Edit")]
        [Route("EditUser/{UserId}")]
        public async Task<ActionResult> EditUser(Guid UserId, User User)
        {
            try
            {
                if (UserId != User.Id) return NotFound("User not found");

                User.ModifiedDate = DateTime.Now;

                _context.Users.Update(User);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException dbUpdateException)
            {
                if (dbUpdateException.InnerException.Message.Contains("duplicate"))
                    return Conflict(String.Format("{0} ya existe en {1}", User.UserName, User.Group.Name));
            }
            catch (Exception ex)
            {
                return Conflict(ex.Message);
            }

            return Ok(User);
        }

        [HttpDelete, ActionName("Delete")]
        [Route("DeleteUser/{UserId}")]
        public async Task<ActionResult> DeleteUser(Guid? UserId)
        {
            if (_context.Users == null) return Problem("Entity set 'DataBaseContext.User' is null.");
            var User = await _context.Users.FirstOrDefaultAsync(s => s.Id == UserId);

            if (User == null) return NotFound("User not found");

            _context.Users.Remove(User);
            await _context.SaveChangesAsync(); //Hace las veces del Delete en SQL

            return Ok(String.Format("El usuario {0} fue eliminado!", User.UserName));
        }
    }
}
