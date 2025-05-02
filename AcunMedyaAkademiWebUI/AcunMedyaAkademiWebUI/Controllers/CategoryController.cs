using AcunMedyaAkademiWebUI.DTOs;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Text;

namespace AcunMedyaAkademiWebUI.Controllers
{
    public class CategoryController : Controller
    {
        //Htttp -> internetteki cihazların birbiriyle konuşması 
        //Get ->verial  listeleme oku 
        //Post->veri ekleme 
        //Put_>Güncelleme
        //Delete->Silme
        //client- server

        //Api farklı sistemleirn birbiriyle veri alışverişi yapmasını sağlar

        //httpclient API ye http istekleri göndermek için kullanılır 
        //*her çağrıda yeni bağlantı açıyor ->Performans sorunu
        //IHttpClientFactory  merkezden yönetir aynı bağlantıları tekrar tekrar kullanılmmasını sağlar
        // daha doğru ve performanslı kullanmak 

        private readonly IHttpClientFactory _httpClientFactory;

        public CategoryController(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public async Task<IActionResult> Index()
        {
            var client = _httpClientFactory.CreateClient(); //httpclientconfigurasyonu döner clientle ilgili temel özellikleri
            var response = await client.GetAsync("https://localhost:7276/api/Categories");
            if (response.IsSuccessStatusCode)
            {
                var jsondata = await response.Content.ReadAsStringAsync();
                var values = JsonConvert.DeserializeObject<List<ResultCategorydto>>(jsondata);
                //deserliaze -> jsondan - text string çevirme
                //seriliaze->textten -json
                return View(values);
            }
            return View();
        }

        [HttpGet]
        public IActionResult CreateCategory()
        {
            return View();
        }


        [HttpPost]
        public async Task<IActionResult> CreateCategory(CreateCategoryDto model)
        {
            var client= _httpClientFactory.CreateClient();
            var jsondata=JsonConvert.SerializeObject(model);
            var content=new StringContent(jsondata,Encoding.UTF8,"application/json");


            var response = await client.PostAsync("https://localhost:7276/api/Categories", content);

            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction("Index");
            }
            return View();
        }



    }
}
