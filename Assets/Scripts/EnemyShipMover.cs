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
public class EnemyShipMover : MonoBehaviour {

	// RigidBody of this object
	private Rigidbody rb;

	// Move Forward
	public float forward;

	// Agility
	public Vector2 agility;

	// Move Towards Players Ship
	private float towards;

	// Player object reference
	private GameObject playerObject;

	// Tilt of the enemy
	public float tilt;

	// Range of random time moving forward
	public Vector2 rangeTimeForward;

	// Range of random time moving towards
	public Vector2 rangeTimeTowards;

	// Boundary class reference
	public Boundary boundary;

	void Start() {
		
		// Get the rigidbody
		rb = GetComponent<Rigidbody>();

		// Get the reference to the Player object
		playerObject = GameObject.FindWithTag("Player");

		//towards = 0.0f;

		StartCoroutine ("Move");
	}

	/**
	 * Called after each physic step
	 **/
	IEnumerator Move () {
		
		// Loop infinito
		while (true) {
			
			// Move forward random time in range
		  	towards = 0.0f;    // Apenas alimenta a variavel 'towards' para ser usada em FixedUpdate, todo frame
			yield return new WaitForSeconds (Random.Range(rangeTimeForward.x, rangeTimeForward.y));

			/*
		     * If the rb and player object still exists
		     */
			if (rb != null && playerObject != null) {

				// Move towards random time in range
				if (rb.position.x > playerObject.transform.position.x) {				
					towards = Random.Range (agility.x, agility.y) * (-1);
					//towards = Mathf.MoveTowards (rb.velocity.x, Random.Range(agility.x, agility.y) * (-1), Time.deltaTime * 100);
				} else if (rb.position.x < playerObject.transform.position.x) {
					towards = Random.Range (agility.x, agility.y) * (+1);
					//towards = Mathf.MoveTowards (rb.velocity.x, Random.Range(agility.x, agility.y) * (+1), Time.deltaTime * 100);
				} else {
					towards = 0.0f;
				}
			}
			yield return new WaitForSeconds (Random.Range(rangeTimeTowards.x, rangeTimeTowards.y));
		}
	}

	/**
	 * Called after each physic step
	 **/
	void FixedUpdate ()
	{
		/*
		 * If the player ship exists
		 */
		if (playerObject != null) {
			
			// Created the vector that holds the player's movement
			Vector3 movement = new Vector3 (towards, 0.0f, forward);

			/*
		     * Aplly into rigidbody as the velocity
		     */  
			rb.velocity = movement;

			/*
		     * Before render the next frame, will check the position, to avoid the player go outside the game area.
		     * 
		     * The position will be set inside game area if the position would be outside the game area. 
		     */
			rb.position = new Vector3 (
				Mathf.Clamp(rb.position.x, boundary.xMin, boundary.xMax), 
				0.0f, 
				Mathf.Clamp(rb.position.z, boundary.zMin, boundary.zMax)
			);

			/*
		     * Tilts the player while moviment left to right, Z axis.
		     * 
		     * The rotation is on the Z axis, but the movement is along X axis.
		     * 
		     * The '-tilt' is to tilt in the correct direction. 
		     */
			rb.rotation = Quaternion.Euler (0.0f, 0.0f, rb.velocity.x * -tilt);
		}
	}

////////////////////////////////////////////////////////////////////////////////////////////////
// Esse eu fiz e estava usando, mas nao usa IEnumerator. Adaptei como ensina no tutorial, 
// com o loop do IEnumerator apenas alimentando variaveis usadas no FixedUpdate e esperando
// Um tempo antes de mudar de novo, adaptando o codigo abaixo nele, a logica.
// 
// O codigo abaixo dele eu havia feito, com uma forma diferente de aproximacao da nave inimiga.
// 
// No projeto, em 'Complete_Game', estah a forma com que foi feito no tutorial.
////////////////////////////////////////////////////////////////////////////////////////////////
//	/**
//	 * Called after each physic step
//	 **/
//	void FixedUpdate ()
//	{
//		/*
//		 * If the player ship exists
//		if (playerObject != null) {
//
//			// Go towards player ship from time to time
//
//			if (Time.time > nextRouteCorrection) {
//
//				nextRouteCorrection = Time.time + routeCorrectionRate;
//
//				// Get this x, compare to the player's ship x.
//				if (rb.position.x > playerObject.transform.position.x) {
//					towards = 0.1f * agility * (-1);
//				} else if (rb.position.x < playerObject.transform.position.x) {
//					towards = 0.1f * agility * (+1);
//				} else {
//					towards = 0;
//				}
//			}
//
//			// Created the vector that holds the player's movement
//			Vector3 movement = new Vector3 (towards, 0.0f, forward);
//
//			/*
//			 * Aplly the input into rigidbody as the velocity
//			 * 
//			 * Input is between 0 and 1, at the maximum 1 unity, so multiply the movement per speed.
//			 */  
//			rb.velocity = movement * speed;
//
//			/*
//			 * Tilts the player while moviment left to right, Z axis.
//			 * 
//			 * The rotation is on the Z axis, but the movement is along X axis.
//			 * 
//			 * The '-tilt' is to tilt in the correct direction. 
//			 */
//			rb.rotation = Quaternion.Euler (0.0f, 0.0f, rb.velocity.x * -tilt);
//		}
//	}

//	/**
//	 * Called after each physic step
//	 **/
//	void FixedUpdate ()
//	{
//
//		/*
//		 * If the player ship exists
//		 */
//		if (playerObject != null) {
//			
//			/* 
//		     * Go towards Player Ship.
//		     * 
//		     * The near is, slower must be.
//		     * 
//		     * A conta eh um fator de correcao, pra ela nao ficar mto rapida proxima do jogador.
//		     * De longe acelera um pouquinho mais, de perto quase nao tem aceleracao.
//		     */
//			distance = Vector3.Distance (playerObject.transform.position, rb.transform.position);
//			rb.transform.position = Vector3.MoveTowards (transform.position, playerObject.transform.position, speed * (1 + (distance/30)));
//
//			/*
//	         * Tilts the player while moviment left to right, Z axis.
//	         * 
//	         * The rotation is on the Z axis, but the movement is along X axis.
//	         * 
//	         * The '-tilt' is to tilt in the correct direction. 
//	         */
//			rb.rotation = Quaternion.Euler (0.0f, 0.0f, rb.velocity.x * -tilt);
//
//			/**
//		     * - shot is a prefab 'bolt', related in the editor.
//		     * - shotSpawn is a 'Shot Spawn', a transform child of the player.
//		     */
//			if (Time.time > nextFire) {
//				nextFire = Time.time + fireRate;
//
//				/**
//			     * We don't need to keep their reference, the shots can take care of themselves after shooting them.
//			     */
//				/** GameObject clone = Instantiate(shot, shotSpawn.position, shotSpawn.rotation) as GameObject; */
//				Instantiate (shot, shotSpawn.position, shotSpawn.rotation);
//
//				/*
//		    	 * Add the shot's sound effect. 
//			     * 
//			     * As all the code in update will be executed before the frame, it doesn't matter where inside this
//			     * block of code will be written the call to the sound effect.
//			     */
//				audioSource.Play ();
//			}
//		}
//	}


}
