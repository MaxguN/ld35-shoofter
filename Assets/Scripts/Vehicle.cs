using UnityEngine;
using System.Collections;

public class Vehicle : MonoBehaviour {
	public string m_type = "Ground";
	public AudioClip m_moveSound;
	private UserInterface m_ui;

	// Use this for initialization
	void Start () {
		m_ui = GameObject.FindGameObjectWithTag("UI").GetComponent<UserInterface>();
	}

	// Update is called once per frame
	void Update () {
		m_ui.SetHP((int) GetComponent<Health>().GetHealth());
	}
}
