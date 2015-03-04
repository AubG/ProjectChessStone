using UnityEngine;
using System.Linq;
using System.Collections;
using System.Collections.Generic;

public delegate void OnNodeVoidDelegate(MapNode t);

public sealed class NodeSelector : MonoBehaviour
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
	

	public OnNodeVoidDelegate clickCallback;
	
	private MapNode focusedNode;

	private Collider2D[] hits = new Collider2D[3];


	#endregion

	#region Initialization


	public void Begin() {
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

			for(; i < hits.Length; i++) if(hits[i] != null && hits[i].tag == "MapNode") break;

			if(i != hits.Length) {
				MapNode temp = hits[i].GetComponent<MapNode>();
				
				if(temp != focusedNode) {
					// highlight
					focusedNode = temp;
				}
				
				if(Input.GetButtonDown("Fire1")) {
					clickCallback(temp);
				}
			}
		}
	}

	
	#endregion
}