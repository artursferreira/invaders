/**
 * Criado em 15/01/2018
 * 
 * Ricardo Lara
 **/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/**
 * Destroys every objects outside the game's boundaries controlled by the box collider 'Boundary'.
 * 
 * Otherwise, shots and hazards keep going indefinitely, using resources. 
 */ 
public class DestroyByBoundary : MonoBehaviour {

	/**
	 * When any other collider leaves the boundary's trigger volume, it will be destroyed.
	 */ 
	void OnTriggerExit(Collider other)
	{
		Destroy(other.gameObject);
	}
}
