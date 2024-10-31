using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace Config_Examples.Controllers
{
    public class HomeController : Controller
    {
        private readonly IConfiguration _configuration;
        private readonly weatherapioptions _options;

        public HomeController(IConfiguration configuration,IOptions<weatherapioptions> options)
        {
            _configuration = configuration;
            _options = options.Value;
        }
        [Route("/")]
        public IActionResult Index()
        {
            ViewBag.key= _configuration["MyValue"];
            ViewBag.Apikey = _configuration.GetValue<string>("MyApiKey","this is my api key");

            var weathersession = _configuration.GetSection("weatherApi");
            ViewBag.WeatherApiKey = weathersession.GetValue<string>("key");
            //if default value not passed then if not key then return null.
            ViewBag.WeatherApiSecrectKey = weathersession.GetValue<string>("Secret_Key");

            var weatherapioption = _configuration.GetSection("weatherApi").Get<weatherapioptions>();
            ViewBag.WeatherApiKey2 = weatherapioption.key;
            ViewBag.WeatherApiSecrectKey2= weatherapioption.Secret_Key;
            // but in above case we are creating object of  weatherapioptions inside controller
            // so again its a dependency ,so we can do it by DI by injecting IOptions
            ViewBag.WeatherApiKey3 = _options.key;
            ViewBag.WeatherApiSecrectKey3 = _options.Secret_Key;
            return View();
        }
    }
}
