using UnityEngine;
using System.Collections;

public class CameraController : MonoBehaviour {
	
	public Transform target;						// Reference to the player's transform.
	public Vector2 smooth = new Vector2(2, 2); 		// How smoothly the camera catches up with it's target movement.
	public Vector2 bordersMax = new Vector2(2, 2);
	public Vector2 bordersMin = new Vector2(-2, -2);
	
	bool CheckXMargin() {
		// Returns true if the distance between the camera and the player in the x axis is greater than the x margin.
		return target.position.x < bordersMax.x && target.position.x > bordersMin.x;
	}	
	
	bool CheckYMargin() {
		// Returns true if the distance between the camera and the player in the y axis is greater than the y margin.
		return target.position.y < bordersMax.y && target.position.y > bordersMin.y;
	}
	
	void TrackPlayer() {
		// By default the target x and y coordinates of the camera are it's current x and y coordinates.
		float targetX = transform.position.x;
		float targetY = transform.position.y;
		
		// If the player has moved beyond the x margin...
		if (CheckXMargin()) {
			// ... the target x coordinate should be a Lerp between the camera's current x position and the player's current x position.
			targetX = Mathf.Lerp(transform.position.x, target.position.x, smooth.x * Time.deltaTime);
		}
		
		// If the player has moved beyond the y margin...
		if(CheckYMargin()) {
			// ... the target y coordinate should be a Lerp between the camera's current y position and the player's current y position.
			targetY = Mathf.Lerp(transform.position.y, target.position.y, smooth.y * Time.deltaTime);
		}
		
		
		// Set the camera's position to the target position with the same z component.
		transform.position = new Vector3(targetX, targetY, transform.position.z);
	}
	
	void Update() {
		TrackPlayer();
	}
}
