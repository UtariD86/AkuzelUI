using Microsoft.AspNetCore.Mvc;

namespace AkuzelUI.Areas.Admin.ViewComponents
{
	public class MedyaViewComponent : ViewComponent
	{
		public IViewComponentResult Invoke(dynamic arguments)
		{

			return View();
		}
	}
}
