namespace Application.UseCases.UcCategory
{
    public class UpdateCategoryRequest : BaseRequest
    {
        public string Description { get; set; }
        public Guid IdCategory { get; set; }

        public UpdateCategoryRequest(Guid idCategory, string description)
        {
            this.IdCategory = idCategory;
            this.Description = description;
        }

        public Domain.Entities.Category Category =>
            new Domain.Entities.Category(IdCategory, string.Empty, Description);
    }
}