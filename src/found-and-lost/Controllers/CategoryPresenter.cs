using Application.Boundaries;
using Domain.Entities;
using Microsoft.AspNetCore.Mvc;

namespace found_and_lost.Controllers
{
    public class CategoryPresenter : IOutputPort<Category>
    {
        public IActionResult ViewModel { get; private set; } = new BadRequestResult();

        public void Fail()
        {
            ViewModel = new BadRequestResult();
        }

        public void Standard(Category output)
        {
            ViewModel = new OkObjectResult(output);
        }
    }
}