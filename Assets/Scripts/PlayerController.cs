using UnityEngine;
using System.Collections;

public class PlayerController : UnitController {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per phisics frame
	void FixedUpdate() {
		/*-- Moving --*/
		_move.x = Input.GetAxis("Horizontal");
		_move.y = Input.GetAxis("Vertical");
		
		/*-- Face directing --*/
		_face.x = Input.GetAxis("Face X");
		_face.y = Input.GetAxis("Face Y");
	}
	
	// Update is called once per frame
	void Update () {
		/*-- Moving --*/
		_Move();
		
		/*-- Face directing --*/
		_UpdateDirection();
	}
}
