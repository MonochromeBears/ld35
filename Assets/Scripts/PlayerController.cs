using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PlayerController : UnitController {
	
	public float flirtTime = 2f;
	public float eatTime = 2f;
	public UnitController food;

	private float flirtTimeDump = 0;
	private float eatTimeDump = 0;
	private List<UnitController> flirting = new List<UnitController>();
	private List<UnitController> following = new List<UnitController>();

	private bool emptyBelly = false;

	// Use this for initialization
	void Start () {
		animator = GetComponent<Animator>();
		directionsCount = Mathf.PI * 2 / 4;
	}
	
	// Update is called once per phisics frame
	void FixedUpdate() {
		//print ("vasya");
		List<UnitController> visible = vision.getVisible();

		int flirtType = 0;
		if (Input.GetAxis("Flirt1") == 1) {
			flirtType = 1;
		} else if (Input.GetAxis("Flirt2") == 1) {
			flirtType = 2;
		} else if (Input.GetAxis("Flirt3") == 1) {
			flirtType = 3;
		}


		
		if (Input.GetAxis("Eat") == 1) {
			print ("Pressed");
			print (following.Count);
			// TODO: Ply tryEating sound;
			if (following.Count > 0 && visible.Contains(following[0])) {
				print ("Here we are");
				animator.SetTrigger("EatIt");				
				food = following[0];
				eating = true;
				food.eating = true;
				food.Death ();
				following.Clear();
				eatTimeDump = eatTime;
				_RelocateForMeet(food.GetComponent<Transform>());
				// TODO: Start eatin sound;
			}
		}


		// FLIRT
		if (flirtType > 0 && visible.Count > 0 && !flirts) {
			foreach (EnemyController enemy in visible) {
				if (enemy.flirtFails || enemy.follow) {
					continue;
				}
				_RelocateForMeet(enemy.GetComponent<Transform>());
				flirts = true;
				enemy.flirts = true;
				flirtTimeDump = flirtTime;
				if (enemy.flirtType != flirtType) {
					enemy.flirtFails = true;
				}
				flirting.Add(enemy);
			}
			if (flirts) {
				animator.SetInteger("Flirting", flirtType);
				// TODO: Start flirt sound;
			}
		}

		if (flirts && flirtTimeDump > 0) {
			flirtTimeDump -= Time.deltaTime;
		} else if (flirts && flirtTimeDump < 0) {
			flirts = false;
			foreach (EnemyController enemy in flirting) {
				enemy.flirts = false;
				if (!enemy.flirtFails) {
					enemy.follow = true;
					following.Add (enemy);
				}
			}
			flirting.Clear ();
			animator.SetInteger ("Flirting", 0);
			// TODO: Stop flirt sound;
		} else {
			UpdateAxis ();
		}


		if (eating && eatTimeDump > 0) {
			eatTimeDump -= Time.deltaTime;
		} else if(eating && eatTimeDump <= 0) {
			eating = false;
		} else {
			UpdateAxis ();
		}

		//print (eatTimeDump);




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
