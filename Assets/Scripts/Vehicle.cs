using UnityEngine;
using System.Collections;

public class Vehicle : MonoBehaviour {
	public enum VehicleType {
		Mech,
		Tank,
		Ship
	}

	enum Leaning {
		None,
		Left,
		Right
	}

	public VehicleType m_vehicle;
	public string m_type = "Ground";
	public AudioClip m_moveSound;

	public Quaternion m_rotation;

	private UserInterface m_ui;
	private Leaning m_leaning = Leaning.None;

	// Use this for initialization
	void Start () {
		m_ui = GameObject.FindGameObjectWithTag("UI").GetComponent<UserInterface>();
	}

	// Update is called once per frame
	void Update () {
		m_ui.SetHP((int) GetComponent<Health>().GetHealth());
	}

	public void Lean(int x) {
		// TODO : Fix leaning angle (pb is angle set by LookRotation

		//if (m_vehicle == VehicleType.Ship) {
		//	Vector3 angle = new Vector3(20, 0, 0);

		//	if (x < 0 && m_leaning != Leaning.Left) { // left
		//		if (m_leaning == Leaning.Right) {
		//			transform.RotateAround(Vector3.zero, Vector3.forward, 20);
		//		}
		//		transform.RotateAround(Vector3.zero, Vector3.forward, 20);
		//		m_leaning = Leaning.Left;
		//	} else if (x > 0 && m_leaning != Leaning.Right) { // right
		//		if (m_leaning == Leaning.Left) {
		//			transform.RotateAround(Vector3.zero, Vector3.forward, -20);
		//		}
		//		transform.RotateAround(Vector3.zero, Vector3.forward, -20);
		//		m_leaning = Leaning.Right;
		//	} else if (m_leaning != Leaning.None) {
		//		if (m_leaning == Leaning.Left) {
		//			transform.RotateAround(Vector3.zero, Vector3.forward, -20);
		//		} else if (m_leaning == Leaning.Right) {
		//			transform.RotateAround(Vector3.zero, Vector3.forward, 20);
		//		}
		//		m_leaning = Leaning.None;
		//	}
		//}
	}

	public void SetOrientation(Vector3 direction) {
		m_rotation = Quaternion.LookRotation(direction);

		switch (m_vehicle) {
			case VehicleType.Mech:
				transform.rotation = m_rotation;
				transform.localEulerAngles += new Vector3(0, 90, 0);
				break;
			case VehicleType.Ship:
				transform.rotation = m_rotation;
				transform.localEulerAngles += new Vector3(0, 180, 0);
				break;
			case VehicleType.Tank:
				GameObject.FindGameObjectWithTag("Turret").transform.rotation = m_rotation;
				GameObject.FindGameObjectWithTag("Turret").transform.eulerAngles += new Vector3(90, 0, 0);
				break;
		}
	}
}
