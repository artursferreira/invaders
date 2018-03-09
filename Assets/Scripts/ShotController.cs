/**
 * Criado em 13/01/2018
 * 
 * Ricardo Lara
 **/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShotController : MonoBehaviour {

	// Fire rate
	public float fireRate;

	// Delay 
	public float delay;

	// Bolt
	public GameObject shot;

	// Shot Spawn is a location, a Transform.
	public Transform shotSpawn;

	// Use this for initialization
	void Start () {
			
		InvokeRepeating ("Fire", delay, fireRate);		
	}

	// Fire
	void Fire() {

		/**
		 * We don't need to keep their reference, the shots can take care of themselves after shooting them.
		 */
		/** GameObject clone = Instantiate(shot, shotSpawn.position, shotSpawn.rotation) as GameObject; */
		Instantiate (shot, shotSpawn.position, shotSpawn.rotation);

		/*
	     * Por minha conta, fiz diferente o som do tiro do inimigo, do tiro da nave.  
	     * No tiro da nave, o som do tiro eh feito chamando o audiosource assim que o tiro eh instanciado. 
	     * Aqui, o audiosource nao eh instanciado apos o tiro, ele estah associado ao objeto 'enemy bolt'. 
	     * Assim que o 'enemy bolt' tem start, o audio source estah como 'Play on Awake'. Acho mais  
	     * correto que o proprio tiro emita seu som.	 
	     */
		//audioSource.Play ();
	}
}
