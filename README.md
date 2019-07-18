# Actors DataBase
A simple to use in memory DB made for Actors Framework.

## How to Install
Add ```"com.pixeye.ecsdb": "https://github.com/dimmpixeye/ecs.database.git",``` into your project manifest.json file.
If you use Assembly Defininition (AD) files then don't forget to add ```Pixeye.Framework.DataBase``` to your AD source.

## How to use
Make a data class and Components helper for getting data from entity.
```csharp
using System.Runtime.CompilerServices;
using Pixeye.Framework;

namespace Pixeye.Source
{
	[System.Serializable]
	sealed class DataMotion
	{
		public float speedWalk;
		public float speedRun;
		public float speedRush;
	}

	#region HELPERS   

	static partial class Components
	{
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		internal static DataMotion DataMotion(in this ent entity)
		{
			return DBHelper.source[entity.id].nodes[Data.Indexer<DataMotion>.ID] as DataMotion;
		}
	}

	#endregion
}
```

Make a data base class where you define and load data.
```csharp	
// make a static class and name is like DataBase. It doesn't really matter.
	static class DataBase
	{

                 // Play Setup method before scene starts. All your data loading stuff goes here.
                [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
	        static void Setup()
	         {
	              // Load Pawns
		        Pawns.LoadGunner();
	                Pawns.LoadCivil();
		        Pawns.LoadEngineer();
                 }
     
                        // I prefer to split data types of different domains into nested classes.
		internal static class Pawns
		{               
                        // Data holders
			public static Data Gunner;
			public static Data Civil;
			public static Data Engineer;
			public static Data Digger;
			public static Data Mutant;
			public static Data Stalker;


			//===============================//
			// Pawn Engineer
			//===============================//
                        // Just a method to fetch data
			public static void LoadEngineer()
			{
				var data = Engineer = new Data();

				// Data Motion
				var dataMotion = data.Add<DataMotion>();

				dataMotion.speedWalk = 35;
				dataMotion.speedRun  = 80;
				dataMotion.speedRush = 100;

			}
  ```

Use it in your project!
```csharp
               public static void NewPawnEngineer()
		{
			ent entity = Entity.Create();
                        // Bind entity with data from DB
			entity.Set(DataBase.Pawns.Engineer);

			var cAI = entity.Set<ComponentAI>();
                        // recieve variables from Data Components
			cAI.speed = entity.DataMotion().speedWalk;
		}

```


