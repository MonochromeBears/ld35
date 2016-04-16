using UnityEngine;
using System.Collections;

public class VisionController : MonoBehaviour {
	public string targetTag;
	public Transform target;
	public float distance = 0.5f;
	
	private UnitController targetController;
	private float smoothTime = 0.5f;
	private Vector2 newPositionDamp = new Vector2(-1, 0);

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
	
	public virtual void OnTriggerEnter2D(Collider2D hitBox) {
		
		if (hitBox.tag == targetTag) {
//			print ("entred");
		}
	}
}
