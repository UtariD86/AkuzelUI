using Microsoft.AspNetCore.Mvc;
using System.Linq.Expressions;

namespace AkuzelUI.Areas.Admin.Controllers
{
	[Area("Admin")]
	public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }


        public IActionResult GetViewComponent(string component)
        {
            
            return ViewComponent(component/*, new { listingType = listingType, refresh = refresh, Courses = courses }*/);
        }
    }
}
