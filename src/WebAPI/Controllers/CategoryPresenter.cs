using Application.Boundaries;
using Domain;
using Domain.Entities;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
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