using UnityEngine;
using System.Collections;

public class Enemy : MonoBehaviour {
	public Transform target;
	private NavMeshAgent agent;
	private Animator animator;
	private Vector3 oldPosition;
	

	// Use this for initialization
	void Start () {
		this.agent = this.transform.GetComponent<NavMeshAgent> ();
		this.animator = this.transform.Find("Sprite").GetComponent<Animator>();
		this.oldPosition = this.transform.position;
		animator.SetInteger ("Direction", (int)Directions.DOWN);
		animator.SetBool("Moving", false);
	}
	
	// Update is called once per frame
	void Update () {
		if (this.target != null) {
			this.agent.updateRotation = false;
			this.agent.SetDestination (target.position);
		}

		Vector3 direction = (this.transform.position - this.oldPosition).normalized;

		if ((direction.x >= Mathf.PI / 4f)) {
			this.animator.SetInteger ("Direction", (int)Directions.RIGHT);
		} else if (direction.x <= -Mathf.PI / 4f) {
			this.animator.SetInteger ("Direction", (int)Directions.LEFT);
		} else if (direction.z > Mathf.PI / 4f) {
			this.animator.SetInteger ("Direction", (int)Directions.TOP);
		} else {
			this.animator.SetInteger ("Direction", (int)Directions.DOWN);
		}

		this.animator.SetBool("Moving", direction.sqrMagnitude > 0);

		this.oldPosition = this.transform.position;
	}
}
