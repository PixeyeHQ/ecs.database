using System;

namespace Pixeye.Actors
{
	public sealed class Data
	{
		public static int lastDataComponentID;
		public object[] nodes = new object[6];

		public T Add<T>() where T : new()
		{
			if (Indexer<T>.ID == -1)
				Indexer<T>.ID = ++lastDataComponentID;

			if (Indexer<T>.ID >= nodes.Length)
				Array.Resize(ref nodes, Indexer<T>.ID << 1);

			var o = new T();
			nodes[Indexer<T>.ID] = o;
			return o;
		}
		public T Add<T>(T obj)
		{
			if (Indexer<T>.ID == -1)
				Indexer<T>.ID = ++lastDataComponentID;

			if (Indexer<T>.ID >= nodes.Length)
				Array.Resize(ref nodes, Indexer<T>.ID << 1);

			nodes[Indexer<T>.ID] = obj;
			return obj;
		}

		public static class Indexer<T>
		{
			public static int ID = -1;
		}
	}

	public static class DBHelper
	{
		public static Data[] source = new Data[Entity.Settings.SizeEntities];

		public static Data Set(in this ent entity, Data obj)
		{
			if (source.Length <= entity.id)
				Array.Resize(ref source, entity.id << 1);

			source[entity.id] = obj;
			return obj;
		}
		
		public static Data DataFrom(in this ent entity, ent entityFrom)
		{
			if (source.Length <= entity.id)
				Array.Resize(ref source, entity.id << 1);

			source[entity.id] = source[entityFrom.id];
			return source[entity.id];
		}
		
	}
}