using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;

namespace CrudwithMongo.Api.Models;

public class Employee
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string Id { get; set; } = string.Empty;

    public string Name { get; set; } = string.Empty;
    public string Department { get; set; } = string.Empty;
    public double Salary { get; set; }
}