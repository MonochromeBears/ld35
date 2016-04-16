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

	protected float usedSpeed = 0;
	protected Vector2 _move = new Vector2(0, 0);
	protected Vector2 _face = new Vector2(-1, 0);
	protected Vector2 _velocity;
	protected bool _facingRight = false;
	protected Vector2 _direction = new Vector2(-1, 0);
	
	public virtual void TakeADamage(float dmg) {
		health -= dmg;
	}
	
	public virtual void TakeAEffect() {
	}
	
	public virtual void Death() {
		DestroyObject(this.gameObject);
	}

	public float GetDirectionRads() {
		float direction = Mathf.Acos(_direction.y);
		if (_direction.x < 0) {
			direction = Mathf.PI * 2 - direction;
		}
		return direction;
	}
	
	public Vector2 GetDirection() {
		return _direction;
	}
	
	protected virtual void _UpdateDirection() {
		if (Vector2.zero != _face) {
			_direction = _face.normalized;
		}
	}
		
	protected virtual void _Move() {
		usedSpeed = moveForvardSpeed;

		float targetX;
		float targetY;

		float faceDiffGrad = Mathf.Acos(_move.normalized.x * _direction.x + _move.normalized.y * _direction.y)
			* (180 / Mathf.PI);
		if (faceDiffGrad > moveBackGrads) {
			usedSpeed = moveBackSpeed;
		} else if(faceDiffGrad > moveSideGrads) {
			usedSpeed = moveSideSpeed;
		} else {
			usedSpeed = moveForvardSpeed;
		}

		targetX = Mathf.SmoothDamp (
			transform.position.x,
			transform.position.x + _move.x * usedSpeed,
			ref _velocity.x,
			smoothTime
			);
		targetY = Mathf.SmoothDamp (
			transform.position.y,
			transform.position.y + _move.y * usedSpeed,
			ref _velocity.y,
			smoothTime
		);
		transform.position = new Vector3(targetX, targetY, transform.position.z);
	}
}
