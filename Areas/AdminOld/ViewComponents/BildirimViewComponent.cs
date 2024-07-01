using Microsoft.AspNetCore.Mvc;

namespace AkuzelUI.Areas.Admin.ViewComponents
{
	public class BildirimViewComponent : ViewComponent
	{
		public IViewComponentResult Invoke(dynamic arguments)
		{

			return View();
		}
	}
}
