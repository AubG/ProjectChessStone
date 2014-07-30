using UnityEngine;
using System.Collections;

namespace X_UniTMX_Example {
	public class CameraFolow2D : MonoBehaviour
	{
	    public float smoothMovement = 3.0f;        
		private CharacterController playerRefence = null;

		Vector3 GetPlayerPosition ()
		{
			return new Vector3 (playerRefence.transform.position.x, // get X from player
								playerRefence.transform.position.y, // get Yfrom player
								transform.position.z);  // get X from camera
		}

		void TryToFindPlayer ()
		{
			// Obtem a instancia do objeto do jogador
			playerRefence = FindObjectOfType<CharacterController> ();
			if(playerRefence != null) {
				// Set current position camera to player
				transform.position = GetPlayerPosition ();
			}
		}

	    void Start ()
	    {
	        TryToFindPlayer ();
	    }

	    void Update ()
	    {
			if(playerRefence == null) {
				TryToFindPlayer ();
				if(playerRefence == null) return;
			}

			Vector3 newPosition =  GetPlayerPosition ();
	        
	        // Interpolate camera position to player
			transform.position = Vector3.Lerp(transform.position, newPosition, smoothMovement * Time.deltaTime);
	    }
	    
	}
}