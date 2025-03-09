using Microsoft.EntityFrameworkCore;

namespace CochainAPI.Model
{
    [PrimaryKey(nameof(Id))]
    public abstract class Base
    {
        public Base() { }
        public Guid Id { get; set; }
        public string? Name { get; set; }
    }
}
