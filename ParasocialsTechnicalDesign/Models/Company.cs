namespace ParasocialsPOSAPI.Models;

public class Company
{
    public Guid SupplierId { get; set; }
    public string CompanyName { get; set; }
    public CompanyServiceType ServiceType { get; set; }
    public string Address { get; set; }
    public string ContactInformation { get; set; }
    public CompanyRelationshipType RelationshipType { get; set; }
}
