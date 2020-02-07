using IamSafeApi.Models;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web.Http;
using static IamSafeApi.Helper.ApiHelper;

namespace IamSafeApi.Controllers
{
    public class ValuesController : ApiController
    {
        // GET api/values
        public IEnumerable<PersonTable> Get(Person person)
        {
            kurtulEntities2 db = new kurtulEntities2();

            return db.PersonTable.AsEnumerable();
        }

        // GET api/values/5
        public string Get(int id)
        {
            return "value";
        }

        // POST api/values
        public PersonTable Post([FromBody]PersonTable value)
        {
            kurtulEntities2 db = new kurtulEntities2();
            db.PersonTable.Add(value);
            db.SaveChanges();

            return value;
        }

        // PUT api/values/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/values/5
        public void Delete(int id)
        {
        }

        [HttpPost]
        [Route("api/getPerson")]
        public IHttpActionResult GetPerson(Person person)
        {
            using (kurtulEntities2 db = new kurtulEntities2())
            {
                
                Person casualty = (from p in db.PersonTable
                                   where p.Name == person.Name && p.Surname == person.Surname && p.MotherName == person.MotherName && p.FatherName == person.FatherName
                                   select new Person()
                                   {
                                       ID = p.ID,
                                       Name = p.Name,
                                       Surname = p.Surname,
                                       FatherName = p.FatherName,
                                       MotherName = p.MotherName,
                                       Latitude = p.Latitude,
                                       Longitude = p.Longitude
                                   }).First();
                

                return new GenerateActionResult<Person>(Request, HttpStatusCode.OK, casualty);
                

            }

        }
        [HttpPost]
        [Route("api/NeedHelp")]
        public IHttpActionResult NeedHelp(NeedHelpTable person)
        {
            using (kurtulEntities2 db = new kurtulEntities2())
            {
                db.NeedHelpTable.Add(person);
                db.SaveChanges();
                return new GenerateActionResult<NeedHelpTable>(Request, HttpStatusCode.OK, person);
            }
        }
        

        
    }
}
