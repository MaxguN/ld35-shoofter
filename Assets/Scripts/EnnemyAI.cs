using UnityEngine;
using System.Collections;

public class EnnemyAI : MonoBehaviour {
	public float m_speed = 25f;
	public string m_type;

	private Transform m_target;
	private Rigidbody m_rigidbody;
	// Use this for initialization
	void Start () {
		m_target = GameObject.FindGameObjectWithTag("PlayerPosition").transform;
		m_rigidbody = GetComponent<Rigidbody>();
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		float distance_z = transform.localPosition.z - m_target.localPosition.z;

		if (distance_z > 200) {
			Move();
		} else {
			Stop();
		}

		if (distance_z < 500 && distance_z > 0) {
			Shoot();
		}

		if (distance_z < -100) {
			Destroy(gameObject);
		}
	}

	void Move() {
		m_rigidbody.velocity = new Vector3(0, 0, -m_speed);
	}

	void Stop() {
		m_rigidbody.velocity = new Vector3(0, 0, 0);
	}

	void Shoot() {
		Vector3 direction = transform.localPosition - m_target.localPosition;
		direction.Normalize();

		GetComponent<Weapon>().Shoot(direction);
	}
}
