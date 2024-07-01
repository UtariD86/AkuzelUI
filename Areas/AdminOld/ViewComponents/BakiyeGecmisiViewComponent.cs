using Microsoft.AspNetCore.Mvc;

namespace AkuzelUI.Areas.Admin.ViewComponents
{
	public class BakiyeGecmisiViewComponent : ViewComponent
	{
		public IViewComponentResult Invoke(dynamic arguments)
		{

			return View();
		}
	}
}
