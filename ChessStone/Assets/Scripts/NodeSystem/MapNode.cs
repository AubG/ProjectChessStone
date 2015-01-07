using UnityEngine;
using System.Collections;

public class MapNode : MonoBehaviour
{
	public string displayName { get; set; }
	public string displayDomain { get; set; }

	private int _stageId = -1;
	public int stageId {
		get { return _stageId; }
		set {_stageId = value; }
	}

	private int _merchantId = -1;
	public int merchantId {
		get { return _merchantId; }
		set {_merchantId = value; }
	}
}