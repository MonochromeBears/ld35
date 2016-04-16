using UnityEngine;
using System.Collections;

public class SpriteController : MonoBehaviour {
	public Sprite[] sprites;

	private float __sector;
	private float __direction;
	private UnitController __unitController;

	// Use this for initialization
	void Start () {
		__unitController = transform.parent.GetComponent<UnitController>();
		__sector = Mathf.PI * 2 / sprites.Length;
	}
	
	// Update is called once per frame
	void Update () {
		float newDirection = __unitController.GetDirectionRads();
		if (newDirection != __direction) {
			print (newDirection);
			__direction = newDirection;
			int sprite = (int) Mathf.Round(__direction / __sector);
			if (sprite >= sprites.Length){
				sprite = 0;
			}
			gameObject.GetComponent<SpriteRenderer>().sprite = sprites[sprite];
		}
	}
}
