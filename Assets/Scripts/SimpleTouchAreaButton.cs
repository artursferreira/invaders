using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class SimpleTouchAreaButton : MonoBehaviour, IPointerDownHandler, IPointerUpHandler {

	// Boolean that controls if the touch area is touched
	private bool touched;

	// Keep track of the id of the touch to prevent a second touch to affect
	private int pointerID; 

	void Awake () {
		touched = false; // ainda que a area de toque esteja com um dedo tocando, nao serah considerado.
		// soh considera que tocou quando ativa o evento 'onPointerDown', ou seja, 
		// soh considera tocar a tela depois que o jogo comeca.
	}

	/*
	 * When the user put or hold the finger
	 */
	public void OnPointerDown(PointerEventData eventData){

		// Se ainda nao foi tocado
		if (!touched) {
			touched = true;
			pointerID = eventData.pointerId; // guardar o id do toque			
		}
	}

	/*
	 * When the user release the finger
	 */ 
	public void OnPointerUp(PointerEventData eventData){

		// Only considerer finger that lifts if is the same finger that initially touched the screen
		if (eventData.pointerId == pointerID) {			
			touched = false; // allows new control for the next touch
		}

	}

	/*
	 * Returns if the area is touched
	 */ 
	public bool CanFire() {

		return touched;
	}
}
