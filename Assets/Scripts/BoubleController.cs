using UnityEngine;
using System.Collections;

public class BoubleController : MonoBehaviour {

	public Sprite[] sprites;
	public Transform target;
	public float showTime = 5;
	public float waitShowTime = 25;

	private float showTimeDump = 0;
	private float waitShowTimeDump = 0;
	private SpriteRenderer sprite;

	// Use this for initialization
	void Start () {
		this.sprite = this.transform.GetComponent<SpriteRenderer>();
		this.sprite.color = new Color(1f,1f,1f,0f);
		int index = target.GetComponent<EnemyController>().flirtType;
		this.sprite.sprite = this.sprites[index-1];
	}
	
	// Update is called once per frame
	void Update () {
		this.showTimeDump -= Time.deltaTime;
		this.waitShowTimeDump -= Time.deltaTime;
		if (this.showTimeDump < 0) {
			this.sprite.color = new Color(1f,1f,1f,0f);
		}
		if (((int) (Random.value * 1000) == 123) && this.waitShowTimeDump < 0) {
			this.sprite.color = new Color(1f,1f,1f,1f);
			this.showTimeDump = this.showTime;
			this.waitShowTimeDump = this.waitShowTime;
		}
	}
}
