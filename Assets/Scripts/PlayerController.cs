/**
 * Criado em 12/01/2018 - 
 * 
 * Ricardo Lara
 * 
 * Eu escrevi de cabeca todos os comentarios em ingles.
 **/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/**
 * This class was created to keep the properties in the unity editor clean. 
 * In order to show the properties properly, the class must be serializable.
 * Moreover, this class can be used in other places.
 **/
[System.Serializable]
public class Boundary {

	// Boundaries of the game area
	public float xMin, xMax, zMin, zMax;
}

/**
 * Controls the player
 */ 
public class PlayerController : MonoBehaviour {

	// Rigidbody related to this object
    private Rigidbody rb;

	// Speed of the player
	public float speed;

	// Tilt of the player
	public float tilt;

	// Boundary class reference
	public Boundary boundary;

	// Shot
	public GameObject shot;

	// Shot Spawn is a location, a Transform.
	public Transform shotSpawn;

	// Fire rate
	public float fireRate;

	// Next fire
	private float nextFire;

	// Audio source of this game object 
	private AudioSource audioSource ;

	// Holds the quaternion that do the offset to considerer initial position of smartphone as base.
	private Quaternion calibrationQuaternion;

	// The class that holds touchPad controls
	public SimpleTouchPad touchPad;

	// The class that holds touchAreaButton 
	public SimpleTouchAreaButton touchButton;

	/**
	 * Start
	 **/
	void Start () {

		// Calibration must be the first line, before unity accessing the player rigid body.
		CalibrateAccelerometer ();

		// Get the reference to the rigidbody object related to this object (Player)
		rb = GetComponent<Rigidbody> ();

		// Get the reference to the audio source object related to this object (Player)
		audioSource = GetComponent<AudioSource >();

		/*
		 * Tambem funciona obter o SimpleTouchPad desta forma,
		 * se 'touchPad' for private.
		 */ 
//		// Get the reference to the Movement Zone object
//		GameObject movementZone = GameObject.FindWithTag("Movement Zone");
//
//		if (movementZone != null) {
//
//			//Debug.Log("Achou o touch control.");
//			// Inside the movementZone object, find the component SimpleTouchPad script
//			touchPad = movementZone.GetComponent<SimpleTouchPad>();
//
//		} else {
//			Debug.Log ("Cannot find Movement Zone object.");
//		}
	}

	/**
	 * Called just before a frame, every frame
	 */ 
	void Update ()
	{
		// Debug.Log("Update time :" + Time.deltaTime);

		/**
		 * Shooting dont require physics and we don't wait for a fixed update.
		 * - shot is a prefab 'bolt', related in the editor.
		 * - shotSpawn is a 'Shot Spawn', a transform child of the player.
		 */
		// Firing using the mouse button
		//if (Input.GetButton("Fire1") && Time.time > nextFire)

		// Fire using the touch zone Fire Zone
		if ( touchButton.CanFire() &&  Time.time > nextFire)
		{
			nextFire = Time.time + fireRate;

			/**
			 * We don't need to keep their reference, the shots can take care of themselves after shooting them.
			 */
			/** GameObject clone = Instantiate(shot, shotSpawn.position, shotSpawn.rotation) as GameObject; */
			Instantiate(shot, shotSpawn.position, shotSpawn.rotation);

			/*
			 * Add the shot's sound effect. 
			 * 
			 * As all the code in update will be executed before the frame, it doesn't matter where inside this
			 * block of code will be written the call to the sound effect.
			 */
			audioSource.Play ();
		}
	}

	/**
	 * Called after each physic step
	 **/
	void FixedUpdate ()
	{
		//Debug.Log("FixedUpdate time :" + Time.deltaTime);

///////////////////////////////////////////////////////////////
//      
//		/*
//		 * Move the player using keyboard 
//       */
//
//		// Get the player's input
//		float moveHorizontal = Input.GetAxis ("Horizontal");
//		float moveVertical = Input.GetAxis ("Vertical");
//
//		// Created the vector that holds the player's movement
//		Vector3 movement = new Vector3 (moveHorizontal, 0.0f, moveVertical);
///////////////////////////////////////////////////////////////

///////////////////////////////////////////////////////////////
//
//		/*
//		 * Move the player using accelerometer
//		 */
//		// Get the accelerometer input with no changes
//		Vector3 accelerationRaw = Input.acceleration;
//		Vector3 acceleration = FixAcceleration(accelerationRaw);
//		//Debug.Log (acceleration.x + "," + acceleration.y + "," + acceleration.z);
//
//		/*
//		 * Created the vector that holds the player's movement.
//		 * 
//		 * In the device, there is x (horizontal) e y (vertical). But in unity, 
//		 * our project is x (horizontal) e z (vertical). Thus, z, in the project, 
//		 * must be y in the device.
//		 */
//		Vector3 movement = new Vector3 (acceleration.x, 0.0f,  acceleration.y);  
//
///////////////////////////////////////////////////////////////

		/*
		 * Move the player using touchscreen
		 */
		Vector2 direction = touchPad.GetDirection ();

		/*
		 * Created the vector that holds the player's movement.
		 */
		Vector3 movement = new Vector3 (direction.x, 0.0f, direction.y);  

		/*
		 * Aplly the input into rigidbody as the velocity
		 * 
		 * Input is between 0 and 1, at the maximum 1 unity, so multiply the movement per speed.
		 */  
		rb.velocity = movement * speed;

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

		// Testing rotating in X axis too
		//rb.rotation = Quaternion.Euler (rb.velocity.z * tilt, 0.0f, rb.velocity.x * -tilt);
	}

	/*
	 * Used to calibrate the Input.acceleration input.
	 * 
	 * The initial position user holds the smartphone, will be considered
	 * as 0,0,0. The accelerometer always points to gravity. We just calculate
	 * the offset. 
	 */
	void CalibrateAccelerometer () {
		
		// Takes a snapshot of acceleration, the 3 axis pointing to gravity.
		Vector3 accelerationSnapshot = Input.acceleration;
		// Create a quaternion to represent the rotation
		Quaternion rotateQuaternion = Quaternion.FromToRotation (new Vector3 (0.0f, 0.0f, -1.0f), accelerationSnapshot);
		// Apply the inverse, thus, the gravity will be artificialy behind the smartphone como este estiver no momento do snapshot.
		calibrationQuaternion = Quaternion.Inverse (rotateQuaternion);
	}

	/*
	 * Get the 'calibrated' value from the Input
     */
    Vector3 FixAcceleration (Vector3 acceleration) {
		// At each fixed update, the acceleration will be a aceleracao no momento, vezes a calibracao inicial.
		Vector3 fixedAcceleration = calibrationQuaternion * acceleration;
		return fixedAcceleration;
	}
}
