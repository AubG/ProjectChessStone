using UnityEngine;
using System.Collections;

namespace X_UniTMX_Example {
	public class TriggerShowObject : MonoBehaviour {

		public GameObject objectToShow = null;
		public string[] tagsCanCollide = {"Player"};
		
		void OnTriggerEnter(Collider other) {
			foreach(string currentTag in tagsCanCollide) {
				if(other.tag.Equals(currentTag)) {
					objectToShow.SetActive(true);
					Character2DController jogador = (Character2DController)FindObjectOfType(typeof(Character2DController));
					jogador.SetStateAnimation(EnumPlayerState.STAY);
					return;
				}
			}
		}
	}
}