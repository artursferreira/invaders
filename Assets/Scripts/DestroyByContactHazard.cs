/**
 * Criado em 15/01/2018
 * 
 * Ricardo Lara
 **/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/**
 * Destroy the hazard
 */
public class DestroyByContactHazard : MonoBehaviour {

	// Explosion effect
	public GameObject explosion;

	// Score value for destroying me
	public int scoreValue;

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
		 * 
		 * I like the idea a  enemy bolt can destroy other enemy. Otherwise, use the second part of if.
		 */
		if (other.CompareTag("Boundary") /*|| other.CompareTag("Enemy Bolt")*/) {   // other.CompareTag is more performatic than other.tag == 
			return;  
		}

		/*
		 * Instantiate the asteroid explosion, at this transform's position and rotation.
		 * The explosion will occur if a bolt or a player ship enters this trigger.
		 */
		Instantiate(explosion, transform.position, transform.rotation);

		/*
		 * Nao da para criar uma variavel publica do GameController, usar aqui, e no Inspector, vincular
		 * o GameController a esta variavel publica, pois este script esta' vinculado a um prefab. Prefabs
		 * podem ser usados em qualquer scene, nao da para criar uma relacao de um objeto de uma cena a um 
		 * Prefab. 
		 * 
		 * Neste caso, cada instancia do Prefab (que contem, cada uma, este script), precisa achar a referencia
		 * 'a instancia do GameController object, e, dentro dele, achar o GameController script. Isto no metodo Start().
		 */ 
		gameController.AddScore (scoreValue);

		// Destroy the other object, unless it is the player (that destoys itself)
		if(other.tag != "Player"){
		    Destroy (other.gameObject);  // destroys the other object
		}
		Destroy (gameObject);        // destroys this object
	}
}
