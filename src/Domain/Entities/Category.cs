namespace Domain.Entities;

public class Category : BaseDomain
{
    public string Name { get; set; }
    public string Description { get; set; }

    public Category? Parent { get; set; }

    public List<Category> SubCategories { get; set; }

    public Category(Guid id, string name, string description)
        : base(id)
    {
        Name = name;
        Description = description;
        SubCategories = new List<Category>();
    }

    public Category(Guid id, string name, string description, Category? parent, List<Category> subCategories) : base(id)
    {
        Name = name;
        Description = description;
        Parent = parent;
        SubCategories = subCategories;
    }

    /// <summary>
    /// Limpa o Parent e as Subcategories
    /// </summary>
    public void ClearParentAndSubcategories()
    {
        SubCategories = new List<Category>();
        Parent = null;
    }

    public void ClearParent()
    {
        Parent = null;
    }

    /// <summary>
    /// Limpa o Parent e as Subcategories das Subcategories
    /// </summary>
    public void ClearSubcategoriesParentsAndSubCategories()
    {
        SubCategories?.ForEach(c => c.ClearParentAndSubcategories());
    }
}