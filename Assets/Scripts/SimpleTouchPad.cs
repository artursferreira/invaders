using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

/**
 * IPointerDownHandler - enables to use OnPointerDown
 * IDragHandler - enables to use OnDrag
 * IPointerUpHandler - enables to use OnPointerUp 
 * 
 * Dentro da Movement Zone, nao ha um local que o usuario deva colocar o dedo para comecar.
 * Em qualquer lugar que ele pressionar e arrastar, vai ser interpretado.
 */ 
public class SimpleTouchPad : MonoBehaviour, IPointerDownHandler, IDragHandler, IPointerUpHandler {

	// It's where the user start touching the screen
	private Vector2 origin;  

	// Hold the drag direction raw
	private Vector2 direction;

	// Smooth level
	public float smoothing;

	// Direction interpolated between the last smoothDirection and direction (raw)
	private Vector2 smoothDirection;

	// Boolean that controls if the touch area is touched (to prevent a second touch to affect)
	private bool touched;

	// Keep track of the id of the touch to prevent a second touch to affect
	private int pointerID; 

	void Awake () {
		direction = Vector2.zero;
		touched = false; // ainda que a area de toque esteja com um dedo tocando, nao serah considerado.
		                 // soh considera que tocou quando ativa o evento 'onPointerDown', ou seja, 
		                 // soh considera tocar a tela depois que o jogo comeca.
	}

	/*
	 * Set our start point, where the user put the finger
	 */
	public void OnPointerDown(PointerEventData eventData){

		// Se ainda nao foi tocado
		if (!touched) {
			touched = true;
			pointerID = eventData.pointerId; // guardar o id do toque	
			origin = eventData.position;     // Holds the start position from eventData. 
			//Debug.Log("Origin: " + origin + ", " + eventData.pointerId);
		}
	}

	/*
	 * Compare the difference between our start position and the current position
	 */
	public void OnDrag(PointerEventData eventData){

		// Only considerer the drag if is the same finger that initially touched the screen

		if(eventData.pointerId == pointerID){
			// Holds the current position from eventData.
			Vector2 currentPosition = eventData.position; 

			// A direcao eh a diferenca entre a posicao atual e a inicial.
			Vector2 directionRaw = currentPosition - origin; 

			/*
			 * Normalizar um vetor, eh manter sua direcao, mas com tamanho 1.
			 * Se o vetor for muito pequeno, a normalizacao vai deixa-lo com tamanho 0.
			 */ 
			direction = directionRaw.normalized; 
			// Debug.Log ("Direction: " + direction);
		}
	}

	/*
	 * Reset everything. The user stopped touching the screen.
	 */ 
	public void OnPointerUp(PointerEventData eventData){

		// Only considerer finger that lifts if is the same finger that initially touched the screen
		if (eventData.pointerId == pointerID) {
			direction = Vector2.zero; // Shorthand for Vector2(0,0).
			touched = false; // allows new control for the next touch
		}
	}

	/*
	 * Returns the direction
	 */ 
	public Vector2 GetDirection() {

		/*
		 * Acredito que retornando 'smoothDirection' ao inves de 'direction'
		 * Funcione assim:
		 * 
		 * Retornando somente direction, a forca eh sempre aplicada em relacao ao 
		 * ponto inicial.
		 * 
		 * Usando smoothDirection, com smoothing entre 0.1 e 0.9, a direcao retornada
		 * eh uma interpolacao entre a ultima direcao, e a nova, com o fator 'smoothing'
		 * pendendo mais pra ultima direcao (proximo de 0.0), ou pendendo mais para a nova
		 * (proxima de 1.0).
		 * 
		 * Por isso que com 'smoothing' = 1, fica igual a retornar 'direction', e com 
		 * 'smoothing' = 0, nao vai sair do lugar, pois o smoothDirection inicial eh 0
		 * e nunca vai mudar, pois a interpolacao de 0 com alguma coisa, pendendo totalmente
		 * para 0, eh zero.
		 * 
		 * O efeito, na pratica eh reagir pouco ao movimento (smoothing pequeno), ou reagir
		 * muito ao movimento do dedo no touch (smoothing grande).
		 * 
		 * Tanto direction quando smoothingDirection sao vetores partindo da origem que
		 * eh onde o dedo tocou a tela. O resultado eh um novo smoothDirection que eh a 
		 * combinacao do smoothDirection anterior e o novo direction.
		 * 
		 * Move towards, assim, para dois vetores, seria como um ponteiro de relogio
		 * que vai sentido horario ou antihorario, e o centro do relogio eh onde
		 * o usuario tocou a tela primeiro.
		 */
		smoothDirection = Vector2.MoveTowards (smoothDirection, direction, smoothing);
		return smoothDirection;
		//return direction;
	}
}
