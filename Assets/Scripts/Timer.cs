using UnityEngine;
using System.Collections;

public class Timer : MonoBehaviour {

	public float levelTime = 300f;

	private float countdown;
	private float timeBarPosYStart;
	private float timeBarHeight = 348;

	// Use this for initialization
	void Start () {
		countdown = levelTime;
		timeBarPosYStart = this.transform.position.y;
		timeBarHeight = this.transform.GetComponent<SpriteRenderer>().bounds.size.y * this.transform.localScale.y;
	}

	// Update is called once per frame
	void Update () {

		if (countdown <= 0) {
			// Game Over
		}
		countdown -= Time.deltaTime;

		this.updateBars();
	}

	void updateBars()
	{
		float bareScale = (levelTime - countdown) / levelTime;
		float posTimeY = timeBarPosYStart + bareScale * timeBarHeight / 2 - timeBarHeight/2;
		this.transform.position = new Vector3(this.transform.position.x, posTimeY, this.transform.position.z);
		this.transform.localScale = new Vector3(this.transform.localScale.x, bareScale, this.transform.localScale.z);
	}
}