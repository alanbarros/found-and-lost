namespace Domain.Entities;

public class Category : BaseDomain
{
    public string Name { get; set; }
    public string Description { get; set; }

    public Category(Guid id, string name, string description)
        : base(id)
    {
        Name = name;
        Description = description;
    }
}