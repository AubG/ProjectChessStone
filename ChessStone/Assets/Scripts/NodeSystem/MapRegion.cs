using UnityEngine;
using System.Collections;

public class MapRegion : MonoBehaviour
{
	[SerializeField]
	private string _displayName;
	public string displayName { get { return _displayName; } }

	[SerializeField]
	private TextAsset _nodeMapTMX;
	public TextAsset nodeMapTMX { get { return _nodeMapTMX; } }

	[SerializeField]
	private SpriteRenderer _highlightRenderer;

	public void Highlight(bool flag) {
		_highlightRenderer.enabled = flag;
	}
}