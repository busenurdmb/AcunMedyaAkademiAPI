using AcunMedyaAkademiWebUI.DTOs;
using AcunMedyaAkademiWebUI.DTOs.Product;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Text;

namespace AcunMedyaAkademiWebUI.Controllers
{
    public class ProductController : Controller
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

        public ProductController(IHttpClientFactory httpClientFactory)
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
                var values = JsonConvert.DeserializeObject<List<ResultProductdto>>(jsondata);
                //deserliaze -> jsondan - text string çevirme
                //seriliaze->textten -json
                return View(values);
            }
            return View();
        }

        [HttpGet]
        public IActionResult CreateProduct()
        {
            return View();
        }


        [HttpPost]
        public async Task<IActionResult> CreateProduct(CreateProductDto model)
        {
            var client = _httpClientFactory.CreateClient();
            var jsondata = JsonConvert.SerializeObject(model);
            var content = new StringContent(jsondata, Encoding.UTF8, "application/json");


            var response = await client.PostAsync("https://localhost:7276/api/Categories", content);

            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction("Index");
            }
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> UpdateProduct(int id)
        {
            var client = _httpClientFactory.CreateClient();
            var response = await client.GetAsync("https://localhost:7276/api/Categories/" + id);
            var jsondata = await response.Content.ReadAsStringAsync();
            var values = JsonConvert.DeserializeObject<UpdateProductDto>(jsondata);
            return View(values);
        }

        [HttpPost]
        public async Task<IActionResult> UpdateProduct(int id, UpdateProductDto model)
        {
            var client = _httpClientFactory.CreateClient();
            var jsondata = JsonConvert.SerializeObject(model);
            var content = new StringContent(jsondata, Encoding.UTF8, "application/json");

            var response = await client.PutAsync($"https://localhost:7276/api/Categories/{id}", content);

            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction("Index");
            }
            return View(model);
        }

        public async Task<IActionResult> DeleteProduct(int id)
        {
            var client = _httpClientFactory.CreateClient();

            var response = await client.DeleteAsync("https://localhost:7276/api/Categories/" + id);

            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction("Index");
            }
            return View();
        }

    }
}
