using UnityEngine;
using System.Linq;
using System.Collections;
using System.Collections.Generic;

public class NodeBuilder : PersistentSingleton<NodeBuilder>
{

	//public SimpleSQL.SimpleSQLManager dbManager;

	public Dictionary<int, NodeDomainData> _mappedDomainData = new Dictionary<int, NodeDomainData>();
	public Dictionary<int, int> _mappedNodeCounts = new Dictionary<int, int>();

	[SerializeField]
	private GameObject basePrefab;

	public override void Awake() {
		base.Awake();

		Load();
	}

	private void Load() {
		/*List<CharacterData> _characters = new List<CharacterData> (from w dbManager.Table<Weapon> ()
		                  select w);*/
		
		List<NodeDomainData> _domains = new List<NodeDomainData> {
			new NodeDomainData() {
				id = 0,
				name = "Merchant Guild",
				description = "A neutral tech-savvy group devoted to acquiring and selling state-of-the-art software",
				spritePath = "Sprites/Nodes/healdrunks"
			},
			new NodeDomainData() {
				id = 1,
				name = "Diana Anderson Exchange",
				description = "An elite corporation that provides protected cloud storage for its clients at exorbitant rates.",
				spritePath = "Sprites/Nodes/plasmaballl"
			},
			new NodeDomainData() {
				id = 2,
				name = "Warner Syndicate",
				description = "A band of ex-government intelligence agents with an almost religious hatred of other hackers.",
				spritePath = "Sprites/Nodes/plasmaballl"
			}
		};
		
		foreach(NodeDomainData c in _domains) {
			_mappedDomainData.Add(c.id, c);
			_mappedNodeCounts.Add(c.id, 0);
		}
	}

	public MapNode BuildNode(int domainId, string name, Vector2 pos) {
		NodeDomainData domainData = GetDomainData(domainId);
		
		if(domainData == null) return null;

		GameObject spawnObject = Instantiate(basePrefab, new Vector3(pos.x, pos.y, -4), Quaternion.identity) as GameObject;
		MapNode spawnNode = spawnObject.GetComponent<MapNode>();
		
		// translate node by setting attributes
		spawnNode.displayName = name;
		spawnNode.displayDomain = domainData.name;
		spawnNode.name = _mappedNodeCounts[domainId] + "-" + spawnNode.displayDomain.ToLower();
		
		// prefab specific data
		SpriteRenderer r = spawnNode.GetComponent<SpriteRenderer>();
		r.sprite = domainData.sprite;
		
		// attributes

		_mappedNodeCounts[domainId]++;
		
		return spawnNode;
	}

	public NodeDomainData GetDomainData(int id) {
		return _mappedDomainData[id];
	}
}