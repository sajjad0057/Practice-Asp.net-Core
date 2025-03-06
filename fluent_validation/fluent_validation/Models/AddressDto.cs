namespace fluent_validation.Models;

public class AddressDto
{
    public int Id { get; set; } // Unique Identifier
    public string Street { get; set; }
    public string City { get; set; }
    public string State { get; set; }
    public string ZipCode { get; set; }
}