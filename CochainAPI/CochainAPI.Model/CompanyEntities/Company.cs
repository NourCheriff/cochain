namespace CochainAPI.Model.CompanyEntities
{
    public abstract class Company : Base
    {
        public string? Email { get; set; }
        public string? Phone { get; set; }
        public Guid CompanyTypeId { get; set; }
        public CompanyType? CompanyType { get; set; }
    }
}
