namespace WebAPI.Controllers
{
	using System.Collections.Generic;
	using System.Web.Http;

	public class Param777
	{
		public int x;
		public int y;

	}
	public class ValuesController : ApiController
	{
		private ISomeRepo someRepo;


		public ValuesController(ISomeRepo someRepo)
		{
			this.someRepo = someRepo;			
		}

		// GET api/values
		public IEnumerable<Param777> Get()
		{
			return new Param777[] { new Param777() { x = 1, y = 2 }, new Param777() { x = 3, y = 4 }, };
		}

		// GET api/values/5
		public string Get(int id)
		{
			return "value";
		}

		// POST api/values
		public void Post(Param777 myParam1)
		{
			var r = "Was posted value = " + myParam1.x + myParam1.y;
		}

		// PUT api/values/5
		public string Put(int id, [FromBody]string value)
		{
			return "Was put value = " + value;
		}

		// DELETE api/values/5
		public void Delete(int id)
		{
		}
	}
}