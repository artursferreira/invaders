/**
 * Criado em 02/02/2018
 * 
 * Ricardo Lara
 **/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/**
 * Destroy the player
 */
public class DestroyByContactPlayer : MonoBehaviour {

	// Player's explosion effect
	public GameObject playerExplosion;

	// Referece to the GameController Script object
	private GameController gameController;  // if cannot set in the inspector, don't show in the inspector (make it private).

	void Start() {

		// Get the reference to the GameController object
		GameObject gameControllerObject = GameObject.FindWithTag("GameController");

		if (gameControllerObject != null) {

			// Inside the GameController object, find the component GameController script
			gameController = gameControllerObject.GetComponent<GameController>();

		} else {
			Debug.Log ("Cannot find GameController object.");
		}
	}

	/*
	 * If other collider enters this trigger volume, it will be destroyed.
	 * 
	 * This cause the bolts disapper as soon as they touch me, instead of passing through them. 
	 * This object itself must be destroyed too.
	 */ 
	void OnTriggerEnter(Collider other){  // if the bolts touch 

		/*
		 * This object is inside the volume of 'Boundary'. Thus, before the first frame, 
		 * the boundary touches this trigger, and both ('Boundary' and this object) would be destroyed.
		 */
		if (other.tag == "Boundary") {
			return;  
		}

		/*
		 * Instantiate the asteroid explosion, at this transform's position and rotation.
		 * The explosion will occur if a bolt or a player ship enters this trigger.
		 */
		Instantiate(playerExplosion, transform.position, transform.rotation);

		/*
		 * Notify the Game Controller that the game is over
		 */
		gameController.GameOver ();

		Destroy (other.gameObject);  // destroys the bolts
		Destroy (gameObject);        // destroys this object
	}
}
