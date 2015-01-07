using UnityEngine;
using System.Collections;

using X_UniTMX;
using Pathfinding;

public class PathingBrain : MonoBehaviour
{
	#region Data
	
	
	private Seeker seeker;
	
	public Path cachedPath { get; private set; }

	private Tile target;
	
	private int currentWaypoint = 0;
	public int distanceTraveled {
		get { return currentWaypoint; }
	}

	private int maxDist = 0;
	
	
	#endregion

	#region Init


	#endregion

	#region AI


	public void RequestPathTarget(GameCharacter client, Tile target, OnPathDelegate callback = null) {
		GameMap.Instance.UpdatePathing(client, target.currUnit);

		Reset();
		this.target = target;

		maxDist = client.move.adjustedMoveRange;

		//Construct a path with start and end points
		ABTilePath p = ABTilePath.Construct (client.currTile, target, null);

		seeker = client.GetComponent<Seeker>();
		if(callback != null) seeker.pathCallback += callback;
		seeker.pathCallback += OnPathComplete;
		seeker.StartPath(p);
	}
	
	public void OnPathComplete(Path p) {
		if(!p.error) {
			cachedPath = p;
			currentWaypoint = 0;
		}
	}
	
	/// <summary>
	/// Returns the current node and increments the waypoint counter.
	/// </summary>
	public GridNode NextNode() {
		return cachedPath.path[++currentWaypoint] as GridNode;
	}
	
	public bool EndOfPath() {
		// Keep in mind count - 1 is the last one in the path, so we have to do count - 2
		// to get it one before the final
		return currentWaypoint >= cachedPath.path.Count - 2 || currentWaypoint >= maxDist;
	}
	
	public void Reset() {
		cachedPath = null;
	}

	public bool IsPathLoaded() {
		return cachedPath != null;
	}
	
	public void OnDisable() {
		seeker.pathCallback -= OnPathComplete;
	}
	
	
	#endregion
}

