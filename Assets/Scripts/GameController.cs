/**
 * Criado em 15/01/2018
 * 
 * Ricardo Lara
 **/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


/**
 * Controls the game.
 * 
 * - Spawns hazards.
 */
public class GameController : MonoBehaviour {

	// Reference to the hazards asteroids
	public GameObject[] asteroids;

	// Reference to the enemy ships
	public GameObject enemyShip; 

	// Probability to spawn a enemy ship
	public int enemySpawnProbability;

	// Spawn values of the hazards.
	public Vector3 spawnValues;

	// Number of hazards
	public int hazardCount;

	// Time before realease next hazard
	public float spawnWait;

	// Waits before the hazards in the same wave
	public float startWait;

	// Wait between the waves
	public float waveWait;

	// The UI Score Text
	public Text scoreText;

	// The UI Restart Game Text
	// Restart text its now a button
	// public Text restartGameText;

	// Restart Button
	public GameObject restartButton;

	// The UI Game Over Text
	public Text gameOverText;

	// Holds the score
	private int score;

	// Flag for the game over
	private bool gameOver;

	// Flag for restart game
	//private bool restartGame;

	/**
	 * Called at very first frame that this game object is enabled
	 */
	void Start(){

		// Initializes the score
		score = 0;

		// Updates the score
		UpdateScore();

		// Initialize the gameOver flag
		gameOver = false;

        // Initialize the restartGame flag
        //restartGame = false;

        // Initialize the restartGameText
        //restartGameText.text = "";

        // Do not show restart button
      
		restartButton.SetActive(false);
       

		// Initialize the gameOverText
		gameOverText.text = "Se vira nos 30";

		// Must be called for the duration of the game
		StartCoroutine (SpawnWaves ());
	}

//	void Update(){
//
//		/**
//		 * Before each frame, verify if must ask for restart game
//		 */
//		if (restartGame) {
//
//			// If the appropriate key is pressed
//			if (Input.GetKeyDown (KeyCode.R)) {
//
//				// Reload the scene
//				SceneManager.LoadScene(SceneManager.GetActiveScene().name);
//			}
//		}
//	}
		
	/**
	 * Spawn the waves.
	 * 
	 * IEnumerator and yield turn the functions in coroutines.
	 * Each 'yeld' relase the resume the execution for the next frame.
	 * Including a 'WaitForSeconds' makes the execution time dependent, not frame dependent.
	 */
	IEnumerator SpawnWaves(){

		// Wait before start the hazards.
		yield return new WaitForSeconds (startWait);

		// Infinite loop
		while(true){
						
			// For the number of hazards on this wave
			for (int i = 0; i < hazardCount; i++) {

				/*
				 * X must be a ramdom x position inside game area, from -x until x
				 * Y is fixed as 0
				 * Z must be ousite the game area, above it.
				 */
				Vector3 spawnPosition = new Vector3 (Random.Range (-spawnValues.x, spawnValues.x), spawnValues.y, spawnValues.z);
				Quaternion spawnRotate = Quaternion.identity; // Quaternion.identify means 'no rotate at all'.

				/*
				 * Spawn enemy ship or asteroid
				 */
				int drawnNumber = Random.Range (0, 100);

				// If a random number between 0 and 100 its between 0 and probability, spawn enemy ship
				if (drawnNumber > 0 && drawnNumber < enemySpawnProbability) {
					// Spawn a enemy ship
					/*
					 * It is better the asteroids and enemy ship go at the same velocity, 
					 * otherwise we must write controls like this below, to avoid asteroids
					 * and ship collide.
					 */ 
					//yield return new WaitForSeconds (spawnWait);
					Instantiate (enemyShip, spawnPosition, spawnRotate);	
					//yield return new WaitForSeconds (spawnWait*5);
			
				// Spawn asteroid
				} else {				
					// Choose a random asteroid from 3 possible types
					GameObject asteroid = asteroids[Random.Range(0, asteroids.Length)];
					Instantiate (asteroid, spawnPosition, spawnRotate);				
				}

				/*
				 * This way, each 'yield' will be resume the iteration for the next frame.
				 * Thus, besides being frame dependent, all the execution will last 10 frames, this is very fast.
				 */ 
				// yield return null;   

				// Wait before next hazard or loop iteration for the 'spawnWait', frame independentely.
				yield return new WaitForSeconds (spawnWait);
			}

			// Wait before next wave
			yield return new WaitForSeconds (waveWait);

			/*
			 * Verifies if the game is over 
			 * The restartGameText and restarGame must be defined.
			 * The infinit loop must be stopped.
			 */
			if (gameOver) {
				//restartGameText.text = "Press 'R' to Restart";
				restartButton.SetActive(true);
				//restartGame = true;
				break;
			}
		}
	}

	/**
	 * Updates the UI score
	 */
	void UpdateScore(){

		scoreText.text = "Pontuação: " + score;
	}

	/**
	 * Enables other classes to updates the score (like hazards itselves)
	 */  
	public void AddScore(int scoreIncrement){

		score = score + scoreIncrement;
		UpdateScore ();
	}

	/**
	 * When called ends the game.
	 * The gameOverText and gameOver must be defined.
	 */
	public void GameOver(){

		gameOverText.text = "ERRROUUU";
		gameOver = true;
	}

	/**
	 * Restarts the game if the restart button is pressed
	 */ 
	public void RestartGame(){

		// Reload the scene
		SceneManager.LoadScene(SceneManager.GetActiveScene().name);
	}
}
