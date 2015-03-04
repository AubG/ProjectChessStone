using UnityEngine;
using System.Collections;

public class RegionMap : MonoBehaviour
{
	[SerializeField]
	private SpriteRenderer mapRenderer;

	public void Show(bool flag) {
		mapRenderer.enabled = flag;
	}
}