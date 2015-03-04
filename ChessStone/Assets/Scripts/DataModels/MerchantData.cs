using UnityEngine;
using System.Linq;
using System.Collections;
using System.Collections.Generic;

using SQLite4Unity3d;

public class MerchantData
{
	#region Data
	
	
	[PrimaryKey]
	public int id { get; set; }

	public string name { get; set; }

	
	#endregion

	#region Cached Data


	private List<MerchantItemData> _items = new List<MerchantItemData>();
	public List<MerchantItemData> items {
		get { return _items; }
	}

	public void AddItem(MerchantItemData item) {
		_items.Add(item);
	}

	public int numItems {
		get { return _items.Count; }
	}


	#endregion

	#region Interaction


	public IEnumerable<MerchantItemData> TakeSet(int setIndex, int setCount) {
		return _items.Skip (setIndex * setCount).Take(setCount);
	}


	#endregion
}