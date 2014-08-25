using UnityEngine;
using System.Collections;

public class CameraController : MonoBehaviour {

	public float PixelsPerUnit = 32.0f;
	public Vector3 CamPos = Vector3.zero;
	float ortographicSize;
	public float Scale = 1.0f;

	// Use this for initialization
	void Start () {
		CamPos = Camera.main.transform.position;
		ortographicSize = Camera.main.orthographicSize;
		Scale = Screen.height / 320.0f;
	}
	
	// Update is called once per frame
	void Update () {
		ortographicSize = Screen.height / 2.0f / PixelsPerUnit / Scale;

		if (Input.GetKey(KeyCode.W))
		{
			CamPos.y += 1 / PixelsPerUnit * 10;
		}
		if (Input.GetKey(KeyCode.S))
		{
			CamPos.y -= 1 / PixelsPerUnit * 10;
		}
		if (Input.GetKey(KeyCode.A))
		{
			CamPos.x -= 1 / PixelsPerUnit * 10;
		}
		if (Input.GetKey(KeyCode.D))
		{
			CamPos.x += 1 / PixelsPerUnit * 10;
		}
		Camera.main.transform.position = CamPos;
		Camera.main.orthographicSize = ortographicSize;
	}

	void OnGUI()
	{
		GUI.Label(new Rect(0.95f * Screen.width, 0.01f * Screen.height, 0.1f * Screen.width, 0.1f * Screen.height), "Scale:");
		Scale = GUI.VerticalSlider(new Rect(0.95f * Screen.width, 0.05f * Screen.height, 0.01f * Screen.width, 0.1f * Screen.height), Scale, 10.0f, 0.1f);
	}
}
