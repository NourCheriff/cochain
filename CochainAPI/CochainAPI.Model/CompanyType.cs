
using Microsoft.EntityFrameworkCore;

namespace CochainAPI.Model
{
    [PrimaryKey(nameof(Id))]
    public class CompanyType
    {
        public Guid Id { get; set; }
        public string Type { get; set; }
    }
}
