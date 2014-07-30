using UnityEngine;
using System.Collections;

[System.Serializable]
public class ReposicionaObjeto {
	public GameObject objeto;
	public Transform newObjectPosition;
}
public class TriggerChangeObjects : MonoBehaviour {

	public GameObject[] objetosExibir;
	public GameObject[] objetosEsconder;
	public ReposicionaObjeto[] objetosReposicionar;
	
	void OnTriggerEnter(Collider other) {
		
		foreach(ReposicionaObjeto r in objetosReposicionar) {
			if(r!=null) r.objeto.transform.position = r.newObjectPosition.position;	
		}

		foreach(GameObject g in objetosEsconder) {
			if(g!=null) g.SetActive(false);	
		}
		
		foreach(GameObject g in objetosExibir) {
			if(g!=null) g.SetActive(true);	
		}
	}

}
