using UnityEngine;
using System.Collections;

public class EnemyController : UnitController {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if (health <=0 ) {
			Death();
		}
	}
}
