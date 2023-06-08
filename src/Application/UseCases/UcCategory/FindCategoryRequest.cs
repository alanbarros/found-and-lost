namespace Application.UseCases.UcCategory
{
    public class FindCategoryRequest : BaseRequest
    {
        public FindCategoryRequest(string categoryName)
        {
            this.CategoryName = categoryName;

        }

        public string CategoryName { get; set; }
    }
}