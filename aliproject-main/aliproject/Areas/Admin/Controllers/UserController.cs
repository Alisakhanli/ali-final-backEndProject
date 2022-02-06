using AliProject.Data;
using AliProject.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace AliProject.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class UserController : Controller
    {
        public AppDbContext context;

        public UserController(AppDbContext _context)
        {
            context = _context;
        }

        public async Task<IActionResult> Update(string id)
        {
            var user = await context.Users.Where(u => u.Id == id).FirstOrDefaultAsync();

            return View(user);
        }

        [HttpPost]
        public async Task<IActionResult> Update(string id, User user)
        {
            var User = await context.Users.Where(u => u.Id == id).FirstOrDefaultAsync();
            User.FullName = user.FullName;

            await context.SaveChangesAsync();

            return RedirectToAction("Index", "Dashboard", new
            {
                area = "admin"
            });
        }

        public async Task<IActionResult> Delete(string id)
        {
            var user = await context.Users.Where(u => u.Id == id).FirstOrDefaultAsync();

            context.Users.Remove(user);
            await context.SaveChangesAsync();
            return RedirectToAction("Index", "Dashboard", new
            {
                area = "admin"
            });
        }
    }
}
