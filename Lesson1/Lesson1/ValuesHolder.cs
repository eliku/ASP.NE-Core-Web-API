using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Lesson1
{
    public class ValuesHolder
    {
        public List<WeatherForecast> Values = new List<WeatherForecast>();

        public void Add(WeatherForecast value)
        {
            Values.Add(value);
        }


        public IList<WeatherForecast> Get()
        {
            return Values;
        }
    }
}
