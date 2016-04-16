using UnityEngine;
using System.Collections;

public class MainMenu : MonoBehaviour {

	public Texture backgroundTextere;

	void OnGUI () {
		GUI.DrawTexture(new Rect(0, 0, Screen.width, Screen.height), backgroundTextere);
	}

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
