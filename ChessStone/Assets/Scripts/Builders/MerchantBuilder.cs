using UnityEngine;
using System.Linq;
using System.Collections;
using System.Collections.Generic;

using X_UniTMX;

public class MerchantBuilder : PersistentSingleton<MerchantBuilder>
{
	private Dictionary<int, MerchantData> _mappedMerchantData = new Dictionary<int, MerchantData>();
	private Dictionary<int, MerchantItemData> _mappedMerchantItemData = new Dictionary<int, MerchantItemData>();

	public MerchantData LoadMerchant(int id) {
		if(!_mappedMerchantData.ContainsKey(id)) {
			DataService ds = new DataService("merchants.bytes");

			MerchantData loaded = ds.GetItemMatching<MerchantData>(x => x.id == id);

			if(loaded != null) {
				_mappedMerchantData.Add(loaded.id, loaded);
				IEnumerable<MerchantItemData> items = ds.GetItemsMatching<MerchantItemData>(x => x.merchantId == id);

				foreach(MerchantItemData item in items) {
					if(!_mappedMerchantItemData.ContainsKey(item.id)) _mappedMerchantItemData.Add(item.id, item);
					loaded.AddItem(item);
				}

				return loaded;
			}
		} else {
			return _mappedMerchantData[id];
		}

		return null;
	}

	public MerchantData GetMerchantData(int id) {
		return _mappedMerchantData[id];
	}
}

