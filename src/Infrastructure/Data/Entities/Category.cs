namespace Infrastructure.Data.Entities;

public class Category : DefaultDbEntity<Domain.Entities.Category>
{
    public string Name { get; set; }
    public string Description { get; set; }

    public override void Update(Domain.Entities.Category domain)
    {
        this.Description = domain.Description;
    }
}