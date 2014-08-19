using UnityEngine;

public enum DamageType {
	True,
	Slash,
	Crush,
	Stab
}

public struct DamageInfo {
	public float amount { get; private set; }
	public DamageType type { get; private set; }

	public DamageInfo(float amount, DamageType type = DamageType.True) {
		this.amount = amount;
		this.type = type;
	}
}