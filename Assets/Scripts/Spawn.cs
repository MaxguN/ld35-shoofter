using UnityEngine;
using System.Collections;

public class Spawn : MonoBehaviour {
	public Transform[] m_ennemiesPool;
	public Transform m_spawnLine;

	private Transform m_target;
	private float m_rate;
	private float m_timer = 0f;

	// Use this for initialization
	void Start () {
		m_target = GameObject.FindGameObjectWithTag("PlayerPosition").transform;
		m_rate = (15f - transform.localPosition.z / 750f) * 0.214f;
	}
	
	// Update is called once per frame
	void Update () {
		if (transform.position.z - m_target.position.z <= 1000) {
			m_timer += Time.deltaTime;

			if (m_timer >= m_rate) {
				SpawnEnnemy();
				m_timer = 0f;
			}
		}
	}

	void SpawnEnnemy() {
		float x, y;
		int index;

		if (Random.value < 0.7) {
			index = 1;
		} else {
			index = 0;
		}

		Transform ennemy = (Transform) Instantiate(m_ennemiesPool[index]);

		if (ennemy.GetComponent<EnnemyAI>().m_type == "Air") {
			x = Random.Range(-18f, 18f);
			y = 19f;
		} else if (ennemy.GetComponent<EnnemyAI>().m_type == "Ground") {
			x = Random.Range(-20f, 20f);
			y = 5f;
		} else {
			x = 0f;
			y = 0f;
		}

		ennemy.localPosition = new Vector3(x, y, m_spawnLine.position.z);
	}
}
