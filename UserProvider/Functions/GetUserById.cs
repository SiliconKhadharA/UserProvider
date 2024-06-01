using Data.Contexts;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;

namespace UserProvider.Functions
{
    public class GetUserById(ILogger<GetUserById> logger, DataContext context)
    {
        private readonly ILogger<GetUserById> _logger = logger;
        private readonly DataContext _context = context;

        [Function("GetUserById")]
        public async Task<IActionResult> Run([HttpTrigger(AuthorizationLevel.Function, "get", Route = "user/{id}")] HttpRequest req, string id)
        {
            var user = await _context.Users.FindAsync(id);
            if (user == null)
            {
                return new NotFoundResult();
            }
            return new OkObjectResult(user);
        }
    }
}
