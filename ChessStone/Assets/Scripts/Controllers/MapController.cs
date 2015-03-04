using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public sealed class MapController : MonoBehaviour
{
	#region State Data


	private enum MapLevel {
		Nodes,
		Regions
	}

	public enum State {
		None,
		Neutral,
		End
	}
	
	private MapLevel mapLevel;

	public State currState { get; set; }


	#endregion

	#region Game Data


	[SerializeField]
	private RegionMap regionMap;

	[SerializeField]
	private NodeMap nodeMap;


	#endregion

	#region Controller Data


	[SerializeField]
	private MerchantController merchantController;


	#endregion

	#region Input Data


	[SerializeField]
	private RegionSelector regionSelector;

	[SerializeField]
	private NodeSelector nodeSelector;


	#endregion

	#region Initialization


	void Awake() {
		Begin();
	}

	public void Begin() {
		currState = State.Neutral;
		mapLevel = MapLevel.Regions;
		Camera.main.orthographicSize = 4f;

		nodeSelector.clickCallback = OnNodeClick;
		regionSelector.clickCallback = OnRegionClick;

		regionSelector.Begin();
	}

	public void End() {
		currState = State.End;
		nodeSelector.End();
		regionSelector.End();
	}


	#endregion

	#region Helpers
	

	public void ZoomIn(MapRegion focusRegion) {
		Camera.main.orthographicSize = 6.5f;
		mapLevel = MapLevel.Nodes;

		regionMap.Show(false);
		nodeMap.LoadRegion(focusRegion);

		regionSelector.End();
		nodeSelector.Begin();
	}

	public void ZoomOut() {
		Camera.main.orthographicSize = 4f;
		mapLevel = MapLevel.Regions;

		regionMap.Show(true);

		nodeSelector.End();
		regionSelector.Begin();
	}

	#region Event Handlers
	

	private void OnRegionClick(MapRegion r) {
		ZoomIn(r);
	}

	private void OnNodeClick(MapNode n) {
		if(n.stageId != -1) {
			PlayerData.Instance.lastLevel = Application.loadedLevel;
			PlayerData.Instance.currStage = n.stageId;
			Application.LoadLevel("Main");
		} else if(n.merchantId != -1) {
			merchantController.Begin(MerchantBuilder.Instance.LoadMerchant(n.merchantId));
		}
	}
	
	
	#endregion

	
	#endregion
}