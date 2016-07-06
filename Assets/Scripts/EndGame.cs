using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class EndGame : MonoBehaviour {

	// Use this for initialization
	void Start () {
		StartCoroutine(LoadMenu());
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	IEnumerator LoadMenu() {
		yield return new WaitForSeconds(5);

		SceneManager.LoadScene("menu");
	}
}
