using UnityEngine;
using System.Linq;
using System.Collections;
using System.Collections.Generic;

public delegate void OnRegionVoidDelegate(MapRegion t);

public sealed class RegionSelector : MonoBehaviour
{
	#region State Data


	public enum State {
		None,
		Neutral,
		End
	}
	
	public State currState { get; private set; }


	#endregion

	#region Game Data
	

	public OnRegionVoidDelegate clickCallback;
	
	private MapRegion focusedRegion;

	private Collider2D[] hits = new Collider2D[3];


	#endregion

	#region Initialization
	
	public void Begin()
	{
		currState = State.Neutral;
		StartCoroutine(UpdateSelection());
	}

	public void End() {
		currState = State.End;
	}


	#endregion

	#region Update Methods


	private IEnumerator UpdateSelection()
	{
		while(currState != State.End) {
			PollSelection();
			
			yield return null;
		}
	}


	#endregion

	#region Helpers


	private void PollSelection() {
		Vector2 worldPoint = Camera.main.ScreenToWorldPoint(Input.mousePosition);

		if(Physics2D.OverlapPointNonAlloc(worldPoint, hits) > 0) {
			int i = 0;

			for(; i < hits.Length; i++) if(hits[i] != null && hits[i].tag == "MapRegion") break;

			if(i != hits.Length) {
				MapRegion temp = hits[i].GetComponent<MapRegion>();
					
				if(temp != focusedRegion) {
					if(focusedRegion != null) focusedRegion.Highlight(false);
					focusedRegion = temp;
					focusedRegion.Highlight(true);
				}
					
				if(Input.GetButtonDown("Fire1")) {
					clickCallback(temp);
				}
			}
		} else if(focusedRegion != null) {
			focusedRegion.Highlight(false);
			focusedRegion = null;
		}
	}

	
	#endregion
}