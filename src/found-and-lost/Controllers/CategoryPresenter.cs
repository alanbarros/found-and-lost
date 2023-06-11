using Application.Boundaries;
using Domain.Entities;
using Microsoft.AspNetCore.Mvc;

namespace found_and_lost.Controllers
{
    public class CategoryPresenter :
        IOutputPort<Category>,
        IOutputPort<Category, Exception>,
        IOutputPort<string, Exception>,
        IOutputPort<PaginationOutput<Category>>
    {
        public IActionResult ViewModel { get; private set; } = new BadRequestResult();

        public void Fail()
        {
            ViewModel = new BadRequestResult();
        }

        public void Fail(Exception exception)
        {
            ViewModel = new BadRequestObjectResult(exception.Message);
        }

        public void NotFound()
        {
            ViewModel = new NotFoundResult();
        }

        public void Standard(Category output)
        {
            ViewModel = new OkObjectResult(output);
        }

        public void Standard(string output)
        {
            ViewModel = new OkObjectResult(output);
        }

        public void Standard(PaginationOutput<Category> output)
        {
            ViewModel = new OkObjectResult(output);
        }
    }
}