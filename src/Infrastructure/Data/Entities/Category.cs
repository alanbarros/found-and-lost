namespace Infrastructure.Data.Entities;

public class Category : DefaultDbEntity<Domain.Entities.Category>
{
    public string Name { get; set; }
    public string Description { get; set; }
    private Guid? _parentId;
    public Guid? ParentId { get => _parentId == default(Guid) ? Id : ParentId; set => _parentId = value ?? null; }
    public virtual Category Parent { get; set; }

    public virtual List<Category> SubCategories { get; set; }

    public override void Update(Domain.Entities.Category domain)
    {
        this.Description = domain.Description;
    }
}