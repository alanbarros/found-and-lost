using AutoMapper;

namespace Infrastructure.Data.Entities;

public class Category : DefaultDbEntity<Domain.Entities.Category>
{
    protected Category()
    {

    }

    public Category(string name, string description, Category? parent = null)
    {
        Name = name;
        Description = description;
        Parent = parent;
        ParentId = parent?.Id;
    }

    public string Name { get; protected set; } = null!;
    public string Description { get; protected set; } = null!;
    public Guid? ParentId { get; protected set; }
    public virtual Category? Parent { get; set; }

    public virtual List<Category> SubCategories { get; set; } = new();

    public override void Update(Domain.Entities.Category domain)
    {
        this.Description = domain.Description;
    }

    public override Domain.Entities.Category MapDomain(ResolutionContext context)
    {
        var domain = new Domain.Entities.Category(Id, Name, Description, Parent is null ? null :
                    new Domain.Entities.Category(Parent.Id, Parent.Name, Parent.Description),
                    new List<Domain.Entities.Category>());

        return domain;
    }
}