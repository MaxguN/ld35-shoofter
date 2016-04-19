using UnityEngine;
using System.Collections;

public class Health : MonoBehaviour {
	public float m_healthPoints = 100f;
	public AudioClip m_destroyed;

	private float m_currentHP = 0f;

	// Use this for initialization
	void Awake () {
		if (m_currentHP == 0f) {
			m_currentHP = m_healthPoints;
		}
	}

	// Update is called once per frame
	void Update () {
	
	}

	public void Hit(float damage) {
		m_currentHP -= damage;

		if (m_currentHP <= 0f) {
			if (m_destroyed) {
				AudioSource.PlayClipAtPoint(m_destroyed, Camera.main.transform.position + new Vector3(0, 0, 5));
			}

			if (tag == "Ennemy") {
				GameObject.FindGameObjectWithTag("PlayerPosition").GetComponent<PlayerController>().AddScore((int)(m_healthPoints * Random.Range(1f, 1.5f)));
			} else if (tag == "Player") {
				GameObject.FindGameObjectWithTag("PlayerPosition").GetComponent<PlayerController>().AddScore((int) m_healthPoints * (-1000));
			}

			Destroy(gameObject);
		}
	}

	public float GetHealth() {
		return m_currentHP;
	}

	public void SetHealth(float health) {
		m_currentHP = health;
	}
}
