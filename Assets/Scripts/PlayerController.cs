using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour {
    public Transform m_mecha;
    public Transform m_tank;
    public Transform m_ship;

    private Rigidbody m_rigidbody;
    private Transform m_currentVehicle;

    private float m_speed = 50f;

	// Use this for initialization
	void Start () {
        m_rigidbody = GetComponent<Rigidbody>();

		Shift(m_mecha);
	}
	
	// Update is called once per frame
	void Update () {
	}

    void FixedUpdate() {
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");
		bool fire = Input.GetButton("Fire1");

		m_rigidbody.velocity = (new Vector3(x, 0, z)) * m_speed;
		m_currentVehicle.GetComponent<Weapon>().Shoot(fire);
    }

    void Shift(Transform newVehicle) {
        m_currentVehicle = newVehicle;

		if (newVehicle == m_ship) {
			GetComponent<MouseTargeting>().AirControl();
			Camera.main.GetComponent<CameraTracking>().AirTrack();
		} else {
			GetComponent<MouseTargeting>().GroundControl();
			Camera.main.GetComponent<CameraTracking>().GroundTrack();
		}

        // ToDo : shift animation
    }
}
