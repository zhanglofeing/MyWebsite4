using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using MyWebsite4.Models;
using MyWebsite4.Services;
using Newtonsoft.Json;

namespace MyWebsite4.Controllers
{
    [Route("api/[controller]")]
    public class ValuesController : Controller
    {
        private ICacheService _CacheService;
        public ValuesController(ICacheService cacheService)
        {
            _CacheService = cacheService;
        }

        // GET api/values
        [HttpGet]
        public IEnumerable<string> Get()
        {
            string value = "ccc";
            TestClass model = new TestClass()
            {
                Name = value,
                Order = 1,
                Left = new TestClass() { Name = value + "Left", Order = 1 },
                Right = new TestClass()
                {
                    Name = value + "Right",
                    Right = new TestClass()
                    {
                        Name = value + "Right2",
                        Order = 0
                    },
                    Order = 2
                }
            };

            _CacheService.Add("abc", "abcdef");
            _CacheService.Add("test", model);

            return new string[] { "value1", "value2" };
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public string Get(string id)
        {
            object result = _CacheService.Get(id);
            if (result == null) return string.Empty;
            if (result is TestClass)
            {
                TestClass v = _CacheService.Get<TestClass>(id);

                return JsonConvert.SerializeObject(v);
            }
            return result.ToString();
        }

        // POST api/values
        [HttpPost]
        public void Post([FromBody]string value)
        {
            TestClass model = new TestClass()
            {
                Name = value,
                Order = 1,
                Left = new TestClass() { Name = value + "Left", Order = 1 },
                Right = new TestClass()
                {
                    Name = value + "Right",
                    Right = new TestClass()
                    {
                        Name = value + "Right2",
                        Order = 0
                    },
                    Order = 2
                }
            };

            _CacheService.Add("test", model);
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(string id, [FromBody]string value)
        {
            _CacheService.Add(id, value);
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(string id)
        {
            _CacheService.Remove(id);
        }
    }
}
