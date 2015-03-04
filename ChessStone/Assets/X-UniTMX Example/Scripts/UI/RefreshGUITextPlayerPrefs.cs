using UnityEngine;
using System.Collections;

public enum TypeRefreshPlayerPrefs {INT,STRING,FLOAT};

public class RefreshGUITextPlayerPrefs : MonoBehaviour {

	public TypeRefreshPlayerPrefs typeRefresh = TypeRefreshPlayerPrefs.INT;
	public float timeRefresh = 0.5f;
	public string keyPlayerPrefs;
	public string defaultValueString;
	public int defaultValueInt;
	public float defaultValueFloat;
	private float countTime = 0.0f;

	// Update is called once per frame
	void Update () {
		countTime += Time.deltaTime;
		if(countTime >= timeRefresh) {
			countTime = 0.0f;
			switch(typeRefresh) {
			case TypeRefreshPlayerPrefs.INT:
				GetComponent<GUIText>().text = "" + PlayerPrefs.GetInt(keyPlayerPrefs,defaultValueInt);
				break;
			case TypeRefreshPlayerPrefs.STRING:
				GetComponent<GUIText>().text = PlayerPrefs.GetString(keyPlayerPrefs,defaultValueString);
				break;
			case TypeRefreshPlayerPrefs.FLOAT:
				GetComponent<GUIText>().text = "" + PlayerPrefs.GetFloat(keyPlayerPrefs,defaultValueFloat);
				break;
			}
		}
	}
}
