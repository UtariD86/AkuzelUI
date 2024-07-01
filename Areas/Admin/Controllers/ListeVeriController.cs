using AkuzelUI.Areas.Admin.Models;
using AkuzelUI.Dtos.ResponseDtos;
using AkuzelUI.Dtos;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;
using System.Net.Http;
using System.Text;
using System.Net.Http.Headers;

namespace AkuzelUI.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ListeVeriController : Controller
    {
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
            if (enumResponse.IsSuccessStatusCode)
            {
                var enumContent = await enumResponse.Content.ReadAsStringAsync();
                var enumList = JsonConvert.DeserializeObject<MyResponse<SelectListDto>>(enumContent).Items;

                // Populate TypeList with SelectListItem objects
                model.TypeList = new SelectList(enumList, "Value", "Text");
            }
            if (id.HasValue)
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
            }
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> AddOrUpdate(ListeVeriEditViewModel model)
        {
            _httpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", "Bearer " + Guid.NewGuid().ToString());
            if (!ModelState.IsValid)
            {
                // Model validation failed, return the view with errors
                return View(model);
            }

            // Prepare the data to be sent to the API
            var postData = new
            {
                Id = model.Id,
                EkId = model.EkId,
                UstId = model.UstId,
                Aciklama = model.Aciklama,
                Deger = model.Deger,
                EkDeger = model.EkDeger,
                Derinlik = model.Derinlik,
                Type = model.TypeId
            };

            // Convert the data to JSON format
            var jsonContent = JsonConvert.SerializeObject(postData);
            var stringContent = new StringContent(jsonContent, Encoding.UTF8, "application/json");

            // Determine the API endpoint URL based on whether it's an add or update operation
            string apiUrl = model.Id == null ? "http://localhost:60805/api/listeveris" : $"http://localhost:60805/api/listeveris/{model.Id}";

            // Send the HTTP POST request to the appropriate API endpoint
            var response = await _httpClient.PostAsync(apiUrl, stringContent);

            if (response.IsSuccessStatusCode)
            {
                // Successfully added or updated, redirect to appropriate action
                return RedirectToAction("Index");
            }
            else
            {
                // Failed to add or update, handle the error appropriately
                ModelState.AddModelError(string.Empty, "Failed to add or update the data. Please try again.");
                return View(model);
            }
        }


    }
}
