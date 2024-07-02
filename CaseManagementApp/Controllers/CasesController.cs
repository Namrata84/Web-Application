// Controllers/CasesController.cs
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace CaseManagementAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CasesController : ControllerBase
    {
        [HttpGet]
        public IEnumerable<Case> Get()
        {
            return Database.GetAllCases();
        }

        [HttpPost]
        public void Post([FromBody] Case c)
        {
            Database.CreateCase(c.Name, c.Length, c.Width, c.Height, c.Weight);
        }

        [HttpPut("{id}")]
        public void Put(int id, [FromBody] Case c)
        {
            Database.UpdateCase(id, c.Name, c.Length, c.Width, c.Height, c.Weight);
        }

        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            Database.DeleteCase(id);
        }
    }
}
