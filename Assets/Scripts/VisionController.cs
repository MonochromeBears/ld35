using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class VisionController : MonoBehaviour {
	public string targetTag;
	public Transform target;
	public float distance = 0.5f;
	
	private UnitController targetController;
	private float smoothTime = 0.5f;
	private List<UnitController> visible = new List<UnitController>();

	// Use this for initialization
	void Start () {
		targetController = target.GetComponent<UnitController>();
	}
	
	// Update is called once per frame
	void Update () {
		Vector2 targetDirection = targetController.GetDirection();
		float targetDirectionRads = targetController.GetDirectionRads();
		
		float newX = target.position.x + targetDirection.x * distance;
		float newY = target.position.y + targetDirection.y * distance;
		Vector3 newPosition = new Vector3(newX, newY, transform.position.z);
		transform.position = Vector3.Lerp(transform.position, newPosition, smoothTime);
//		print(new Vector2(transform.position.x - target.position.x, transform.position.y - target.position.y).normalized);
		
		float targetDirectionGrads = targetDirectionRads * (180 / Mathf.PI);
		Vector3 newRotation = new Vector3(0, 0, -targetDirectionGrads);
		transform.rotation = Quaternion.Euler(newRotation);
	}

	public List<UnitController> getVisible() {
		return visible;
	}
	
	public virtual void OnTriggerEnter2D(Collider2D unitCol) {

		if (unitCol.tag == targetTag) {
			UnitController unitController = unitCol.GetComponent<UnitController>();
			visible.Add(unitController);
		}
	}
	
	public virtual void OnTriggerExit2D(Collider2D unitCol) {

		if (unitCol.tag == targetTag) {
			UnitController unitController = unitCol.GetComponent<UnitController>();
			visible.Remove(unitController);
		}
	}
}
