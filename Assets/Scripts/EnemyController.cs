using UnityEngine;
using System.Collections;

public class EnemyController : UnitController {

	public int flirtType = 1;
	public bool follow = false;
	public bool flirtFails = false;
	public int followDistance = 1;
	public Transform target;

	void Start() {
		animator = GetComponent<Animator>();
		directionsCount = Mathf.PI * 2 / 4;
//		GetComponent<Rigidbody2D>().velocity = Random.insideUnitCircle * (0.5f + 0.5f * Random.value) * 50f;
	}

	void OnCollisionEnter2D(Collision2D collision) {
//		transform.localRotation = Quaternion.Euler(Random.value * 360, Random.value * 360, Random.value * 360);
	}

	// Update is called once per phisics frame
	void FixedUpdate() {

		if (follow) {
			Vector2 speed = Vector2.zero;
			float newPosX = target.position.x - transform.position.x;
			float newPosY = target.position.y - transform.position.y;

			bool needWalk = Mathf.Abs((int) newPosX) >= 1.5
				|| Mathf.Abs((int) newPosY) >= 2;
			if (needWalk) {
				speed = new Vector2(newPosX, newPosY).normalized;
			}

			_move.x = speed.x;
			_move.y = speed.y;

			_face.x = _move.x;
			_face.y = _move.y;
		}

	}
	
	// Update is called once per frame
	void Update () {

		if (health <=0 ) {
			Death();
		}

		/*-- Moving --*/
		_Move();

		/*-- Face directing --*/
		_UpdateDirection();
	}
}
