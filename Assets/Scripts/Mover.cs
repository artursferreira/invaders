/**
 * Criado em 13/01/2018
 * 
 * Ricardo Lara
 **/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/**
 * Moves the shots
 */ 
public class Mover : MonoBehaviour {

	// RigidBody of this object
	private Rigidbody rb;

	// Bolt's speed
	public float speed;

	void Start() {

		// Get the rigidbody
		rb = GetComponent<Rigidbody>();

		// Once it's created, it neeeds to move forward (Z axis as known as forward)
		rb.velocity = transform.forward * speed;
	}
}
