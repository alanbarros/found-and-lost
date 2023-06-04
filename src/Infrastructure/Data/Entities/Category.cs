namespace Infrastructure.Data.Entities;

public class Category : DefaultDbEntity
{
    public string Name { get; set; }
    public string Description { get; set; }
}