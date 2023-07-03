namespace FF4FreeEnterprisePR.Inventory
{
	class EntityJSON
	{
		public int id { get; set; }
		public Layer[] layers { get; set; }
		public string name { get; set; }
		public float opacity { get; set; }
		public string type { get; set; }
		public bool visible { get; set; }
		public int x { get; set; }
		public int y { get; set; }
		public float width { get; set; }
		public float height { get; set; }

		public class Layer
		{
			public string draworder { get; set; }
			public int id { get; set; }
			public string name { get; set; }
			public Object[] objects { get; set; }
			public float opacity { get; set; }
			public string type { get; set; }
			public bool visible { get; set; }
			public float x { get; set; }
			public float y { get; set; }
		}

		public class Object
		{
			public int gid { get; set; }
			public float height { get; set; }
			public int id { get; set; }
			public string name { get; set; }
			public Property1[] properties { get; set; }
			public int rotation { get; set; }
			public string type { get; set; }
			public bool visible { get; set; }
			public float width { get; set; }
			public float x { get; set; }
			public float y { get; set; }
		}

		public class Property1
		{
			public string name { get; set; }
			public string type { get; set; }
			public object value { get; set; }
		}

	}
}
