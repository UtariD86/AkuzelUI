using Microsoft.AspNetCore.Mvc;

namespace AkuzelUI.Areas.Admin.ViewComponents
{
	public class UserViewComponent : ViewComponent
	{
		public IViewComponentResult Invoke(dynamic arguments)
		{

			return View();
		}
	}
}
