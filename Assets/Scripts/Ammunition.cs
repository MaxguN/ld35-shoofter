using UnityEngine;
using System.Collections;

public class Ammunition : MonoBehaviour {
	public float m_damage = 50f;
	public float m_velocity = 50f;
	public float m_lifetime = 30f;
	public string m_originTag;

	private Rigidbody m_rigidbody;
	private float m_duration = 0f;

	// Use this for initialization
	void Start () {
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
		Health health = other.GetComponent<Health>();

		if (other.tag == m_originTag) {
			return;
		}

		if (health) {
			health.Hit(m_damage);
		}

		Destroy(gameObject);
	}
}
