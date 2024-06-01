using Data.Contexts;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;

namespace UserProvider.Functions
{
    public class DeleteUser(ILogger<DeleteUser> logger, DataContext context)
    {
        private readonly ILogger<DeleteUser> _logger = logger;
        private readonly DataContext _context = context;

        [Function("DeleteUser")]
        public async Task<IActionResult> Run([HttpTrigger(AuthorizationLevel.Function, "delete", Route = "user/{id}")] HttpRequest req, string id)
        {
            var user = await _context.Users.FindAsync(id);
            if (user == null)
            {
                return new NotFoundResult();
            }

            _context.Users.Remove(user);
            await _context.SaveChangesAsync();

            return new OkObjectResult(new { Status = 200, Message = "User deleted successfully." });
        }
    }
}
