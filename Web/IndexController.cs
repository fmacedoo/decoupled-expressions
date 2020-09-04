namespace Web
{
    using Microsoft.AspNetCore.Mvc;

    [Route("/")]
    public class IndexController : Controller
    {
        IService _service;

        public IndexController(IService service)
        {
            _service = service;
        }

        [HttpGet]
        public ActionResult Get()
        {
            var awesomeModels = _service.GetAwesome();
            var notAwesomeModels = _service.GetNotAwesome();

            return Json(new {
                awesomeModels,
                notAwesomeModels
            });
        }
    }
}