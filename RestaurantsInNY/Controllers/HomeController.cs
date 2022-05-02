using Microsoft.AspNetCore.Mvc;
using RestaurantsInNY.Models;
using System.Diagnostics;
using Newtonsoft.Json;
using RestaurantsInNY.DataAccess;

namespace RestaurantsInNY.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        RestaurantDBContext rdbContext = new RestaurantDBContext();
        
       /* public HomeController(RestaurantDBContext context)
        {
            rdbContext = context;
        }*/

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

       /* public IActionResult Index()
        {
            return View();
        }*/

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        public ViewResult Index()

        {
            HttpClient httpClient;

            string BASE_URL = "https://data.cityofnewyork.us/resource/qgc5-ecnb.json";

            httpClient = new HttpClient();

            httpClient.DefaultRequestHeaders.Accept.Clear();

            httpClient.DefaultRequestHeaders.Accept.Add(

                new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));

            string NATIONAL_PARK_API_PATH = BASE_URL;

            string parksData = "";

            List<APIModel> parks = null;

            //httpClient.BaseAddress = new Uri(NATIONAL_PARK_API_PATH);

            httpClient.BaseAddress = new Uri(NATIONAL_PARK_API_PATH);



            try

            {

                HttpResponseMessage response = httpClient.GetAsync(NATIONAL_PARK_API_PATH)

                                                        .GetAwaiter().GetResult();

                ;
                if (response.IsSuccessStatusCode)

                {

                    parksData = response.Content.ReadAsStringAsync().GetAwaiter().GetResult();

                }

                if (!parksData.Equals(""))

                {

                    // JsonConvert is part of the NewtonSoft.Json Nuget package

                    parks = JsonConvert.DeserializeObject<APIModel[]>(parksData).ToList();
                    Console.WriteLine(parks[0].calories);

                }

                
                
                Restaurant r = new Restaurant();
                Menu m = new Menu();
                Nutrition n = new Nutrition();
                Random rand = new Random();
                foreach(var park in parks)
                {
                    r.Restaurant_ID = Int32.Parse(park.restaurant_id);
                    r.Restaurant_Name = park.restaurant;
                    r.Restaurant_Location = "New York";

                    m.Item_Name= park.item_name;
                    m.Item_Category = park.food_category;
                    m.Menu_Item_ID = Int32.Parse(park.menu_item_id);

                    n.Nutrition_ID = rand.Next();
                    if (park.sugar == null) park.sugar = "0";
                    n.Sugar = Int32.Parse(park.sugar);
                    if(park.total_fat == null) park.total_fat = "0";
                    n.Total_Fat = Int32.Parse(park.total_fat);
                    if(park.calories==null) park.calories = "0";
                    n.Calorie = Int32.Parse(park.calories);
                    if(park.protein == null) park.protein = "0";
                    n.Protein = Int32.Parse(park.protein);
                    rdbContext.Nutrition.Add(n);
                    rdbContext.SaveChangesAsync();

                }



                
                
            }

            catch (Exception e)

            {
                Console.WriteLine(e.Message);

            }

            return View(parks);

        }
    }
}