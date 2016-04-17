using UnityEngine;
using System.Collections;

public class EnemyController : UnitController {

	public int flirtType = 1;
	public bool follow = false;
	public bool flirtFails = false;
	public Transform target;

	void Start() {
		animator = GetComponent<Animator>();
		directionsCount = Mathf.PI * 2 / 4;
//		GetComponent<Rigidbody2D>().velocity = Random.insideUnitCircle * (0.5f + 0.5f * Random.value) * 50f;
	}

	void OnCollisionEnter2D(Collision2D collision) {
//		transform.localRotation = Quaternion.Euler(Random.value * 360, Random.value * 360, Random.value * 360);
	}
	
	// Update is called once per frame
	void Update () {
		if (health <=0 ) {
			Death();
		}
	}
}
