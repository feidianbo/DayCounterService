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
		//数据库连接字符串
		const string strconn = "mongodb://127.0.0.1:27017";
		//数据库名称
		const string dbName = "testdb";
		////创建数据库链接
		//MongoServer server = MongoDB.Driver.MongoServer.Create(strconn);
		////获得数据库test
		//MongoDatabase db = server.GetDatabase(dbName);

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
			var collection = database.GetCollection<User>("Users");

			collection.InsertOne(new User() { Name = "FDB", Age = 30 });

			var list = collection.Find(u => u.Name == "FDB").ToList();

			Query();

			return View(list);
		}

		public void Query()
		{
			//MongoDatabaseSettings settings = new MongoDatabaseSettings();
			MongoServerSettings settings = new MongoServerSettings();
			//settings.Server = new MongoServerAddress("localhost");

			//创建数据库链接
			MongoServer server = new MongoServer(settings);
			//获得数据库test
			MongoDatabase db = server.GetDatabase("demo");
			//获取Users集合
			MongoCollection col = db.GetCollection("Users");
			//定义获取“Name”值为“test”的查询条件
			var query = new QueryDocument { { "Name", "FDB" } };

			//查询全部集合里的数据
			var result1 = col.FindAllAs<User>();
			//查询指定查询条件的第一条数据，查询条件可缺省。
			var result2 = col.FindOneAs<User>();
			//查询指定查询条件的全部数据
			var result3 = col.FindAs<User>(query);
		}

		public void Insert()
		{
			//创建数据库链接
			MongoServerSettings settings = new MongoServerSettings();
			//settings.Server = new MongoServerAddress("localhost");

			//创建数据库链接
			MongoServer server = new MongoServer(settings);
			//获得数据库test
			MongoDatabase db = server.GetDatabase(dbName);
			User user = new User();
			user.Name = "test";
			user.Age = 30;
			//获得Users集合,如果数据库中没有，先新建一个
			MongoCollection col = db.GetCollection("Users");
			//执行插入操作
			col.Insert<User>(user);
		}

		public void Update()
		{
			//创建数据库链接
			MongoServerSettings settings = new MongoServerSettings();
			//settings.Server = new MongoServerAddress("localhost");

			//创建数据库链接
			MongoServer server = new MongoServer(settings);
			//获得数据库test
			MongoDatabase db = server.GetDatabase(dbName);
			//获取Users集合
			MongoCollection col = db.GetCollection("Users");
			//定义获取“Name”值为“test”的查询条件
			var query = new QueryDocument { { "Name", "FDB" } };
			//定义更新文档
			var update = new UpdateDocument { { "$set", new QueryDocument { { "Age", 30 } } } };
			//执行更新操作
			col.Update(query, update);
		}

		public void Delete()
		{
			//创建数据库链接
			MongoServerSettings settings = new MongoServerSettings();
			//settings.Server = new MongoServerAddress("localhost");

			//创建数据库链接
			MongoServer server = new MongoServer(settings);
			//获得数据库test
			MongoDatabase db = server.GetDatabase(dbName);
			//获取Users集合
			MongoCollection col = db.GetCollection("Users");
			//定义获取“Name”值为“test”的查询条件
			var query = new QueryDocument { { "Name", "FDB" } };
			//执行删除操作
			col.Remove(query);
		}
	}
}
