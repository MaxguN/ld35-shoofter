using UnityEngine;
using System.Collections;

public class Ammunition : MonoBehaviour {
	public float m_damage = 50f;
	public float m_velocity = 50f;

	private Rigidbody m_rigidbody;

	// Use this for initialization
	void Start () {
		m_rigidbody = GetComponent<Rigidbody>();
		m_rigidbody.velocity = new Vector3(0, 0, m_velocity);
	}
	
	// Update is called once per frame
	void FixedUpdate () {
	}

	void OnTriggerEnter(Collider other) {
		Health health = other.GetComponent<Health>();

		if (health) {
			health.Hit(m_damage);
		}

		Destroy(gameObject);
	}
}
