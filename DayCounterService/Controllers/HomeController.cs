using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Mvc.Ajax;
using MongoDB.Bson;
using MongoDB.Driver;

namespace DayCounterService.Controllers
{
	public class HomeController : Controller
	{
		public ActionResult Index()
		{
			var mvcName = typeof(Controller).Assembly.GetName();
			var isMono = Type.GetType("Mono.Runtime") != null;

			ViewData["Version"] = mvcName.Version.Major + "." + mvcName.Version.Minor;
			ViewData["Runtime"] = isMono ? "Mono" : ".NET";

			return View();
		}

		public ActionResult List()
		{
			var client = new MongoClient("mongodb://localhost:27017");
			var database = client.GetDatabase("demo");
			var collection = database.GetCollection<User>("user");

			collection.InsertOne(new User() { Name = "FDB", Age = 30 });

			var list = collection.Find(u => u.Name == "FDB").ToList();

			return View(list);
		}
	}


}
