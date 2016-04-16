using UnityEngine;
using System.Collections;

public class EnemyController : UnitController {

	public int flirtType = 1;
	public bool follow = false;
	public bool flirtFails = false;

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
