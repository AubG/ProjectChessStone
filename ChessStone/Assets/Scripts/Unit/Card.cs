﻿using UnityEngine;
using System.Collections;

public class Card : MonoBehaviour {

	public int cost;
	public string img_path;
	public string tag;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void run(){
		Debug.Log (tag + " has been cast");
	}
}