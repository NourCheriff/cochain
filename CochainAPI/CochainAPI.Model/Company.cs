
using Microsoft.EntityFrameworkCore;

namespace CochainAPI.Model
{
    [PrimaryKey(nameof(Id))]
    public class Company
    {
        public Guid Id { get; set; }
        public string CompanyName { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public Guid CompanyTypeId { get; set; }
        public CompanyType CompanyType { get; set; }
    }
}
