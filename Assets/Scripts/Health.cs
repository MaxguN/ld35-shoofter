using UnityEngine;
using System.Collections;

public class Health : MonoBehaviour {
	public float m_healthPoints = 100f;

	private float m_currentHP = 0f;

	// Use this for initialization
	void Start () {
		m_currentHP = m_healthPoints;
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void Hit(float damage) {
		m_currentHP -= damage;

		if (m_currentHP <= 0f) {
			Destroy(gameObject);
		}
	}
}
