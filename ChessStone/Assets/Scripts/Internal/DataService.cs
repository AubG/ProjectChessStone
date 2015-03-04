using UnityEngine;
using System.Linq.Expressions;
using System.Collections;
using System.IO;
using SQLite4Unity3d;
using System.Collections.Generic;

public class DataService
{
	private ISQLiteConnection _connection;
	
	public DataService(string databaseName) {
		Factory factory = new Factory();

		string dbPath = "";
		string loadDBPath = Path.Combine(Application.streamingAssetsPath, databaseName);
#if !UNITY_EDITOR && UNITY_ANDROID
		dbPath = Path.Combine(Application.persistentDataPath, databaseName);
		WWW loadDB = new WWW(loadDBPath);  // this is the path to your StreamingAssets in android
		while (!loadDB.isDone) { }  // CAREFUL here, for safety reasons you shouldn't let this while loop unattended, place a timer and error check
		File.WriteAllBytes(dbPath, loadDB.bytes);
#else
		dbPath = loadDBPath;
#endif

		_connection = factory.Create(dbPath);
	}

	/*
	public void CreateDB(){
		_connection.DropTable<Person> ();
		_connection.CreateTable<Person> ();
		
		_connection.InsertAll (new[]{
			new Person{
				Id = 1,
				Name = "Tom",
				Surname = "Perez",
				Age = 56
			},
			new Person{
				Id = 2,
				Name = "Fred",
				Surname = "Arthurson",
				Age = 16
			},
			new Person{
				Id = 3,
				Name = "John",
				Surname = "Doe",
				Age = 25
			},
			new Person{
				Id = 4,
				Name = "Roberto",
				Surname = "Huertas",
				Age = 37
			},
		});
	}
	*/
	public IEnumerable<T> GetItems<T>() where T : new() {
		return _connection.Table<T>();
	}

	public T GetItemMatching<T>(Expression<System.Func<T, bool>> predicate) where T : new() {
		return _connection.Table<T>().Where(predicate).FirstOrDefault();
	}
	
	public IEnumerable<T> GetItemsMatching<T>(Expression<System.Func<T, bool>> predicate) where T : new() {
		return _connection.Table<T>().Where(predicate);
	}

	public void InsertItem(object item) {
		_connection.Insert (item);
	}
}
