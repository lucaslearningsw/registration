using Microsoft.AspNetCore.Mvc;
using resgistration.App.ViewModels;

namespace resgistration.App.Controllers
{
    public class HomeController : Controller
    {

       
        public IActionResult Index()
        {
            return View();
        }
        

        [Route("error/{id:length(3,3)}")]
        public IActionResult Errors(int id)
        {
            var modelErro = new ErroViewModel();

            if(id == 500)
            {
                modelErro.Message = "Acorreu um erro! Tente novamente mais tarde ou contate nosso suporte";
                modelErro.Title = "Ocorreu um ERRO";
                modelErro.ErrorCodee = id;

            }

            else if (id == 404)
            {
                modelErro.Message = "A página que está procurando não existe";
                modelErro.Title = "Ops! Página não encontrada";
                modelErro.ErrorCodee = id;

            }
            else if (id == 403)
            {
                modelErro.Message = "Você não tem permissão para fazer isso";
                modelErro.Title = "Acesso negado";
                modelErro.ErrorCodee = id;

            }
            else 
            {
                return StatusCode(404);
            }

            return View("Error", modelErro);


        }

    }
}
