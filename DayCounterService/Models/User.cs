using System;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Driver;

namespace DayCounterService
{
	public class User
	{
		public User()
		{

		}
		[BsonId]
		public MongoDB.Bson.ObjectId _id { get; set; }


		public string Id
		{
			get { return _id.ToString(); }
			set
			{
				_id = ObjectId.Parse(value);
			}
		}

		public String Name { get; set; }
		public int Age { get; set; }
	}
}
