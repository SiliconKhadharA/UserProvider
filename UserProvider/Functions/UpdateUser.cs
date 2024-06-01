using Data.Contexts;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace UserProvider.Functions
{
    public class UpdateUser(ILogger<UpdateUser> logger, DataContext context)
    {
        private readonly ILogger<UpdateUser> _logger = logger;
        private readonly DataContext _context = context;

        [Function("UpdateUser")]
        public async Task<IActionResult> Run([HttpTrigger(AuthorizationLevel.Function, "put", Route = "user/update")] HttpRequest req)
        {
            var requestBody = await new StreamReader(req.Body).ReadToEndAsync();
            var updateUserRequest = JsonConvert.DeserializeObject<UpdateUserRequest>(requestBody);

            if (updateUserRequest == null || string.IsNullOrWhiteSpace(updateUserRequest.Id))
            {
                return new BadRequestObjectResult(new { Status = 400, Message = "Invalid request." });
            }

            var user = await _context.Users.FindAsync(updateUserRequest.Id);
            if (user == null)
            {
                return new NotFoundResult();
            }

            user.Email = updateUserRequest.Email;
            user.FirstName = updateUserRequest.FirstName!;
            user.LastName = updateUserRequest.LastName!;
            user.PhoneNumber = updateUserRequest.PhoneNumber;

            await _context.SaveChangesAsync();

            return new OkObjectResult(new { Status = 200, Message = "User updated successfully." });
        }
    }

    public class UpdateUserRequest
    {
        public string? Id { get; set; }
        public string? Email { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? PhoneNumber { get; set; }
    }
}
