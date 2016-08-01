using UnityEngine;
using System.Collections;

public class Ammunition : MonoBehaviour {
	public float m_damage = 50f;
	public float m_velocity = 50f;
	public float m_lifetime = 30f;
	public string[] m_originTag;
	public AudioClip m_explosionSound;

	private Rigidbody m_rigidbody;
	private float m_duration = 0f;

	// Use this for initialization
	void Awake () {
		m_rigidbody = GetComponent<Rigidbody>();
		m_rigidbody.velocity = new Vector3(0, 0, m_velocity);
	}
	
	// Update is called once per frame
	void Update () {
		m_duration += Time.deltaTime;

		if (m_duration >= m_lifetime) {
			Destroy(gameObject);
		}
	}

	void OnTriggerEnter(Collider other) {
		foreach (string tag in m_originTag) {
			if (tag == other.tag) {
				return;
			}
		}

		if (other.tag == "LaserPlane") {
			return;
		}

		Health health = other.GetComponent<Health>();

		if (health) {
			health.Hit(m_damage);
		}

		if (m_explosionSound) {
			AudioSource.PlayClipAtPoint(m_explosionSound, Camera.main.transform.position + new Vector3(0, 0, 10));
		}

		Destroy(gameObject);
	}

	public void SetVelocity(Vector3 direction) {
		direction.Normalize();
		m_rigidbody.velocity = direction * m_velocity;
	}
}
