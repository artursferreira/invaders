/**
 * Criado em 15/01/2018
 * 
 * Ricardo Lara
 **/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/**
 * Rotates the asteroid randomically
 */
public class RandomRotator : MonoBehaviour {

	// Maximum tumble value
	public float tumble;

	// Rigidbody related to this object
	private Rigidbody rb;

	void Start() {

		// Get the reference to the rigidbody object related to this object (Asteroid)
		rb = GetComponent<Rigidbody> ();

		/*
		 * Set the rigidbody's angular velocity
		 * 
		 * Random.insideUnitSphere is a random Vector3 that indicates a vector that
		 * begins in the center of a sphere, and points to a random x, y, z coordinate between 0 and 1.
		 * 
		 * The bigger the vector is, faster will be the rotation. 
		 * 
		 * For example:
		 * 
		 * A Vector3 (0.5, 0, 0) will rotate in the X axis. 
		 * A Vector3 (0.9, 0, 0) will rotate in the X axis too, but faster. 
		 * A Vector3 (0.2, 0.4, 0.1) will rotate in the three X axis.
		 * 
		 * Tumble is a multiplication factor.
		 */
		rb.angularVelocity = Random.insideUnitSphere * tumble;
	}
}
