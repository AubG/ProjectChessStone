using UnityEngine;
using System.Collections;

public delegate void StageObjectiveDelegate();

public abstract class StageObjective
{
	protected bool complete;

	public abstract void Init();

	public abstract bool IsComplete();
}