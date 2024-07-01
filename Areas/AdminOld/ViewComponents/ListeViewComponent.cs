using Microsoft.AspNetCore.Mvc;

namespace AkuzelUI.Areas.Admin.ViewComponents
{
	public class ListeViewComponent : ViewComponent
	{
		public IViewComponentResult Invoke(dynamic arguments)
		{

			return View();
		}
	}
}
