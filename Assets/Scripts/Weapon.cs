using UnityEngine;
using System.Collections;

public class Weapon : MonoBehaviour {
	public Transform[] m_weapons;
	public Transform m_ammunition;
	public AudioClip m_shootSound;
	public float m_cooldown = 1f;

	private float m_timer = 0f;
	private AudioSource m_source;

	// Use this for initialization
	void Start () {
		m_source = GetComponent<AudioSource>();
		if (m_shootSound) {
			m_source.clip = m_shootSound;
		}
	}
	
	// Update is called once per frame
	void Update () {

	}

	public void Shoot(bool fire) {
		if (fire) {
			Shoot(new Vector3(0, 0, 1f));
		}
	}

	public void Shoot(bool fire, Vector3 direction) {
		if (fire) {
			Shoot(direction);
		}
	}

	public void Shoot(Vector3 direction) {
		if (m_timer >= m_cooldown) {
			m_timer = 0f;

			foreach (Transform weapon in m_weapons) {
				Transform ammo = (Transform)Instantiate(m_ammunition);
				ammo.parent = weapon;
				ammo.localPosition = Vector3.zero;

				ammo.parent = null;
				ammo.rotation = Quaternion.LookRotation(direction);
				ammo.GetComponent<Ammunition>().SetVelocity(direction);
			}

			if (m_shootSound) {
				m_source.Play();
			}
		}

		m_timer += Time.deltaTime;
	}
}
