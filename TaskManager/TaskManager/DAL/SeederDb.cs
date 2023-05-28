using TaskApp.DAL.Entities;
using TaskManager.DAL;
using TaskManager.DAL.Entities;

namespace TaskManager.DAL
{
    public class SeederDb
    {
        private readonly DataBaseContext _context;
        public SeederDb(DataBaseContext context)
        {
            _context = context;
        }

        public async System.Threading.Tasks.Task SeederAsync()
        {
            await _context.Database.EnsureCreatedAsync(); //Esta línea me ayuda a crear mi BD de forma automática
            await PopulateGroupsAsync();

            await _context.SaveChangesAsync();
        }
        private async System.Threading.Tasks.Task PopulateGroupsAsync()
        {
            if (!_context.Groups.Any())
            {
                _context.Groups.Add(
                    new Group
                    {
                        CreatedDate = DateTime.Now,
                        Name = "Default",
                        Users = new List<User>
                        {
                            new User
                            {
                                UserName = "administrador",
                                Password = "admin",
                                CreatedDate = DateTime.Now
                            }
                        }
                    });
            }
        }
    }
}
