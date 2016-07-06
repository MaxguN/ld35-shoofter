using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class Menu : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		bool click = Input.GetButton("Fire1");
	
		if (click) {
			SceneManager.LoadScene("level");
		}
	}
}
