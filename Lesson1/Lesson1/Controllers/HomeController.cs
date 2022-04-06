using Lesson1.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace Lesson1.Controllers
{
    public class HomeController : Controller
    {
        private readonly ValuesHolder _holder;

        public HomeController(ValuesHolder holder)
        {
            _holder = holder;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        public IActionResult Create([FromQuery] WeatherForecast input)
        {
            _holder.Add(input);
            return Ok();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        public IActionResult Read()
        {
            return Ok(_holder.Get());
        }

        public IActionResult Update([FromQuery] WeatherForecast stringsToUpdate,
        [FromQuery] WeatherForecast newValue)
        {
            for (int i = 0; i < _holder.Values.Count; i++)
            {
                if (_holder.Values[i].Date == stringsToUpdate.Date)
                    _holder.Values[i].TemperatureC = newValue.TemperatureC;
            }

            return Ok();
        }

        public IActionResult Delete([FromQuery] WeatherForecast stringsToDelete)
        {
            for (int i = 0; i < _holder.Values.Count; i++)
            {
                if (_holder.Values[i].Date == stringsToDelete.Date)
                    _holder.Values[i].TemperatureC = 0;
            }

            return Ok();
        }
    }
}
