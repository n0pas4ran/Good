using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;



namespace Good.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class Good : Controller
    {
        static List<Product> entries = new List<Product>();
        // GET all: api/Goods
        [HttpGet]
        [Authorize]
        public string Read()
        {
            string output = "Nothing has been added";
            if (entries.Count > 0)
            {
                output = "";
                foreach (var ent in entries)
                {
                    output += ent.ToString() + "\n";
                }
            }
            return output;
        }

        
        // GET api/Brands/{id}
        [HttpGet("{id}")]
        [Authorize]
        public string Read(int id)
        {
            string outPut = "Not Found";
            if ((id < entries.Count) && (id >= 0))
            {
                outPut = JsonConvert.SerializeObject(entries[id]);
            }
            Console.WriteLine(String.Format("Запрошена запись № {0} : {1}", id, outPut));
            return outPut;
        }

        [HttpPost]
        public IActionResult Create([FromBody] Product ent)
        {
            if (ent == null)
            {
                return BadRequest();
            }
            WebRequest web = WebRequest.Create("https://localhost:5001/api/Brands");
            WebResponse response = web.GetResponse();
            int flag = 0;
            Entry json = new Entry();
            using(Stream stream = response.GetResponseStream())
            {
                using(StreamReader reader = new StreamReader(stream))
                {
                    string line = "";
                    while ((line = reader.ReadLine()) != null)
                    {
                        json = JsonConvert.DeserializeObject<Entry>(line);
                        if (json.name == ent.bName)
                        {
                            break;
                        }
                    }

                    foreach (var size in json.size)
                    {
                        if (size.brSize == ent.size)
                        {
                            flag = 1;
                            break;
                        }
                    }

                }
            }

            if (flag==0)
            {
                return BadRequest();
            }

            entries.Add(ent);
            Console.WriteLine(String.Format("Всего записей {0}, послано: {1}", entries.Count, ent.ToString()));

            return new OkResult();

        }

        //Change /api/Brands/{id}
        [HttpPut("{id}")]
        public IActionResult Change(int id, [FromBody] Product ent)
        {
            if ((id < 0) || (id > entries.Count) || (ent == null))
            {
                return BadRequest();
            }
            entries.RemoveAt(id);
            entries.Insert(id, ent);
            Console.WriteLine(String.Format("Запись {0} успешно изменена на {1}", id, ent.ToString()));
            return new OkResult();

        }

        // DELETE api/Brands/{id}
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            if ((id < 0) || (id > entries.Count))
            {
                return BadRequest();
            }
            entries.RemoveAt(id);
            Console.WriteLine("Запись " + id + " успешно удалена.");
            return new OkResult();
        }
    }
}
