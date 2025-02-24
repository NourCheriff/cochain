
using Microsoft.EntityFrameworkCore;

namespace CochainAPI.Model.Authentication
{
    [PrimaryKey(nameof(Id))]
    public class UserTemporaryPassword
    {
        public Guid Id { get; set; }
        public string UserId { get; set; }
        public string Password { get; set; }
        public DateTime ExpirationDate { get; set; }
        public bool IsUsed { get; set; }
    }
}
