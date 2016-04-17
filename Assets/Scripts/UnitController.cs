using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;

public abstract class UnitController : MonoBehaviour {
	public float health = 100;
	public float smoothTime = 0.1f;
	public float moveForvardSpeed = 0.2f;
	public float moveSideSpeed = 0.15f;
	public float moveBackSpeed = 0.08f;
	public float moveBackGrads = 140;
	public float moveSideGrads = 80;
	public VisionController vision;
	public bool flirts = false;
	public bool eating = false;
	public Animator animator;

	protected float usedSpeed = 0;
	protected Vector3 _move = new Vector3(0, 0, 0);
	protected Vector3 _face = new Vector3(-1, 0, 0);
	protected Vector3 _velocity;
	protected bool _facingRight = false;
	protected Vector3 _direction = new Vector3(-1, 0, 0);
	protected float directionsCount = 0;
	
	public virtual void TakeADamage(float dmg) {
		health -= dmg;
	}
	
	public virtual void TakeAEffect() {
	}
	
	public virtual void Death() {
		// TODO: Play die animation and sound;
		DestroyObject(this.gameObject);
	}

	public float GetDirectionRads() {
		float direction = Mathf.Acos(_direction.z);
		if (_direction.x < 0) {
			direction = Mathf.PI * 2 - direction;
		}
		return direction;
	}
	
	public Vector2 GetDirection() {
		return _direction;
	}
	
	protected virtual void _UpdateDirection() {
		if (Vector3.zero != _face) {
			_direction = _face.normalized;
			float directionRads = GetDirectionRads();
			int animationDirection = (int) Mathf.Round(directionRads / directionsCount);
			animator.SetInteger("Direction", animationDirection + 1);
		}
	}
		
	protected virtual void _Move() {

		if (flirts || eating) {
			animator.SetBool("Moving", false);
			return;
		}

		usedSpeed = moveForvardSpeed;

		float targetX;
		float targetY;

		float faceDiffGrad = Mathf.Acos(_move.normalized.x * _direction.x + _move.normalized.z * _direction.z)
			* (180 / Mathf.PI);
		if (faceDiffGrad > moveBackGrads) {
			usedSpeed = moveBackSpeed;
		} else if(faceDiffGrad > moveSideGrads) {
			usedSpeed = moveSideSpeed;
		} else {
			usedSpeed = moveForvardSpeed;
		}

		targetX = Mathf.SmoothDamp (
			this.transform.parent.position.x,
			this.transform.parent.position.x + _move.x * usedSpeed,
			ref _velocity.x,
			smoothTime
			);
		targetY = Mathf.SmoothDamp (
			this.transform.parent.position.z,
			this.transform.parent.position.z + _move.z * usedSpeed,
			ref _velocity.z,
			smoothTime
		);

		bool moving = targetX != this.transform.parent.position.x
			|| targetY != this.transform.parent.position.z;

		if (moving) {
			this.transform.parent.position = new Vector3(targetX, this.transform.parent.position.y, targetY);
		}

		animator.SetBool("Moving", moving);
	}

	protected virtual void _RelocateForMeet(Transform target) {
		var targetX = target.position.x;
		var selfX = this.transform.parent.position.x;

		if (targetX > selfX) {
			selfX = targetX;
			targetX -= 2.5f;
		} else {
			targetX = selfX - 2.5f;
		}

		target.position = new Vector3(targetX, this.transform.parent.position.y, target.position.z);
		UnitController enemyController = target.GetComponent<UnitController>();
		enemyController.animator.SetInteger("Direction", 2);
		this.transform.parent.position = new Vector3(selfX, this.transform.parent.position.y, this.transform.parent.position.z);
		animator.SetInteger("Direction", 3);
	}
}
