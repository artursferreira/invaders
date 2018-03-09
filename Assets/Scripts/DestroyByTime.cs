/**
 * Criado em 16/01/2018
 * 
 * Ricardo Lara
 **/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * Destroys objects (like explosions) by time, where destroying by boundary or contact is not enough.
 */
public class DestroyByTime : MonoBehaviour {

	// Life time before destroying a gameObject.
	public float lifeTime;

	void Start(){

		Destroy (gameObject, lifeTime);
	}
}
