﻿using UnityEngine;
using System.Collections;

public class UIController : MonoBehaviour {

	public void ChangeScene (string sceneName) {
		Application.LoadLevel(sceneName);
	}
		
}
