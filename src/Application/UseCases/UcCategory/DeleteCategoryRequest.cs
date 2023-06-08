namespace Application.UseCases.UcCategory
{
    public class DeleteCategoryRequest : BaseRequest
    {
        public Guid CategoryId { get; }
        public DeleteCategoryRequest(Guid categoryId)
        {
            this.CategoryId = categoryId;

        }
    }
}