/**
 * Criado em 02/02/2018
 * 
 * Ricardo Lara
 **/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scroll : MonoBehaviour {

	public float speed;
		
	// Update is called once per frame
	void Update () {

		Vector2 offset = new Vector2 (0, Time.time * speed);
		GetComponent<MeshRenderer> ().material.mainTextureOffset = offset;	
	}
}
