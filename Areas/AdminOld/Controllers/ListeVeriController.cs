
using AkuzelUI.Areas.AdminOld.Models;
using AkuzelUI.Dtos;
using AkuzelUI.Dtos.ResponseDtos;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;
using System.Security.Claims;

namespace AkuzelUI.Areas.AdminOld.Controllers
{
    [Area("AdminOld")]
    public class ListeVeriController : Controller
    {

        //Uri baseAddress = new Uri("http://localhost:60805/api");
        private readonly HttpClient _httpClient;

        public ListeVeriController(IHttpClientFactory httpClientFactory)
        {
            _httpClient = httpClientFactory.CreateClient("MyHttpClient");
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> AddOrUpdate(Guid? id)
        {
            var model = new ListeVeriEditViewModel();

            var enumResponse = _httpClient.GetAsync(_httpClient.BaseAddress + "/enum/GetEnumList?typeId=" + "1").Result;
            if(enumResponse.IsSuccessStatusCode)
            {
                var enumContent = await enumResponse.Content.ReadAsStringAsync();
                var enumList = JsonConvert.DeserializeObject<MyResponse<SelectListDto>>(enumContent).Items;

                // Populate TypeList with SelectListItem objects
                model.TypeList = new SelectList(enumList, "Value", "Text");
            }
            if (id.HasValue )
            {
                var response = _httpClient.GetAsync(_httpClient.BaseAddress + "/listeveris/" + id.Value).Result;
                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync();
                    var entity = JsonConvert.DeserializeObject<ListeVerisResponse>(content);

                    model.Id = entity.Id;
                    model.EkId = entity.EkId.Value;
                    model.UstId = entity.UstId.Value;
                    model.Aciklama = entity.Aciklama;
                    model.Deger = entity.Deger;
                    model.EkDeger = entity.EkDeger;
                    model.Derinlik = entity.Derinlik;
                    model.TypeId = entity.Type;
                    model.Type = model.TypeList.Where(x => x.Value.Equals(entity.Type.ToString())).Select(x => x.Value).FirstOrDefault();

                }
                //    var main = (await _mainService.GetById(id.Value)).Data ?? new Category();
                //    model.Id = main.Id;
                //    model.SinifDuzeyiId = main.SinifDuzeyiId;
                //    model.Content = main.Content;
                //    model.Name = main.Name;
                //    model.Description = main.Description;
            }
            return View(model);
        }

        //[HttpPost]
        //public async Task<IActionResult> AddOrUpdate(CategoryEditViewModel model)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        var result = await _mainService.GetById(model.Id.HasValue ? model.Id.Value : 0);
        //        if (result.ResultStatus == ResultStatus.Success && result.Data.Id < 0)
        //        {
        //            return Json(new { Result = result });
        //        }
        //        result.Data.SinifDuzeyiId = model.SinifDuzeyiId;
        //        result.Data.Content = model.Content;
        //        result.Data.Name = model.Name;
        //        result.Data.Description = model.Description;
        //        result.Data.Picture = "~/images/avatar/default.jpg";
        //        if (result.Data.Id == 0)
        //        {
        //            result.Data.IsDeleted = false;
        //            result.Data.UpdateDate = DateTime.Now;
        //            var userId = HttpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        //            result.Data.UpdateUserId = userId != null && int.TryParse(userId, out int _userId) ? _userId : 0;
        //        }
        //        var addOrUpdateResult = result.Data.Id > 0 ? await _mainService.UpdateOrDelete(result.Data) : await _mainService.InsertAsync(result.Data);

        //        return Json(new { Result = addOrUpdateResult });
        //    }
        //    return Json(new { Result = new Result(ResultStatus.Error, Messages.PageIsNotFound) });
        //}

        //public JsonResult GetAllToGrid()
        //{
        //    var data = _mainService.FetchAllDtos().Data;
        //    return Json(data, new Newtonsoft.Json.JsonSerializerSettings());
        //}

        //public async Task<JsonResult> Delete(int? id)
        //{
        //    if (id.HasValue)
        //    {
        //        var result = await _mainService.GetById(id.Value);
        //        if (result.ResultStatus == ResultStatus.Error)
        //        {
        //            return Json(new { Result = result });
        //        }
        //        result.Data.IsDeleted = true;
        //        var updateResut = await _mainService.UpdateOrDelete(result.Data);
        //        return Json(new { Result = updateResut });
        //    }
        //    return Json(new { Result = new Result(ResultStatus.Error, Messages.ResultIsNotFound) });
        //}

        //public SelectList GetAsSelectList()
        //{
        //    var result = _mainService.FetchAllDtos();

        //    if (result.ResultStatus == ResultStatus.Success)
        //    {
        //        var categoryList = result.Data;
        //        var selectListItems = categoryList
        //            .Select(category => new SelectListItem
        //            {
        //                Text = category.Name,
        //                Value = category.Id.ToString()
        //            })
        //            .ToList();
        //        var selectList = new SelectList(selectListItems, "Value", "Text");
        //        return selectList;
        //    }
        //    else
        //    {
        //        return null;
        //    }
        //}
    }
}
