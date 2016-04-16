using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PlayerController : UnitController {
	
	public float flirtTime = 2f;

	private float flirtTimeDump = 0;
	private List<UnitController> visibleDump = new List<UnitController>();

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per phisics frame
	void FixedUpdate() {

		List<UnitController> visible = vision.getVisible();
		if (Input.GetAxis("Jump") == 1 && visible.Count > 0 && !flirts) {
			foreach (EnemyController enemy in visible) {
				if (enemy.flirtFails) {
					continue;
				}
				flirts = true;
				enemy.flirts = true;
				flirtTimeDump = flirtTime;
				if (enemy.flirtType != 1) {
					enemy.flirtFails = true;
				}
				visibleDump.Add(enemy);
			}
			if (flirts) {
				// TODO: Start flirt animation and flirt sound;
			}
		}
		
		if (flirts && flirtTimeDump > 0 ) {
			flirtTimeDump -= Time.deltaTime;
		} else if(flirts && flirtTimeDump < 0 ) {
			flirts = false;
			foreach (EnemyController enemy in visibleDump) {
				enemy.flirts = false;
				if (!enemy.flirtFails) {
					enemy.follow = true;
				}
			}
			visibleDump.Clear();
			// TODO: Stop flirt animation and flirt sound;
		} else {
			UpdateAxis();
		}

	}
	
	// Update is called once per frame
	void Update () {
		/*-- Moving --*/
		_Move();
		
		/*-- Face directing --*/
		_UpdateDirection();
	}


	private void UpdateAxis() {
		/*-- Moving --*/
		_move.x = Input.GetAxis("Horizontal");
		_move.y = Input.GetAxis("Vertical");
		
		/*-- Face directing --*/
		_face.x = _move.x;
		_face.y = _move.y;
	}
}
