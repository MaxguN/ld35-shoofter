using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour {
    public Transform m_mecha;
    public Transform m_tank;
    public Transform m_ship;
	public float m_shiftCooldown = 5f;

    private Rigidbody m_rigidbody;
    private Transform m_currentVehicle;

    private float m_speed = 50f;
	private float m_mechaTimer = 0f;
	private float m_shipTimer = 0f;
	private float m_tankTimer = 0f;

	// Use this for initialization
	void Start () {
        m_rigidbody = GetComponent<Rigidbody>();

		Shift(m_mecha);
	}
	
	// Update is called once per frame
	void Update () {
		bool shiftMecha = Input.GetButton("Shift1");
		bool shiftShip = Input.GetButton("Shift2");
		bool shiftTank = Input.GetButton("Shift3");

		if (m_mechaTimer > 0f) {
			m_mechaTimer -= Time.deltaTime;
		}
		if (m_shipTimer > 0f) {
			m_shipTimer -= Time.deltaTime;
		}
		if (m_tankTimer > 0f) {
			m_tankTimer -= Time.deltaTime;
		}

		if (shiftMecha) {
			ShiftToMecha();
		} else if (shiftShip) {
			ShiftToShip();
		} else if (shiftTank) {
			ShiftToTank();
		}
	}

    void FixedUpdate() {
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");
		bool fire = Input.GetButton("Fire1");

		m_rigidbody.velocity = (new Vector3(x, 0, z)) * m_speed;
		m_currentVehicle.GetComponent<Weapon>().Shoot(fire, GetComponent<MouseTargeting>().GetDirection());
    }

    void Shift(Transform newVehicle) {
		if (m_currentVehicle) {
			Destroy(m_currentVehicle.gameObject);
		}

		m_currentVehicle = (Transform) Instantiate(newVehicle);
		m_currentVehicle.parent = transform;
		m_currentVehicle.localPosition = new Vector3(0, m_currentVehicle.localPosition.y, 0);

		if (newVehicle == m_ship) {
			GetComponent<MouseTargeting>().AirControl();
			Camera.main.GetComponent<CameraTracking>().AirTrack();
		} else {
			GetComponent<MouseTargeting>().GroundControl();
			Camera.main.GetComponent<CameraTracking>().GroundTrack();
		}

		// ToDo : shift animation
	}

	void ShiftToMecha() {
		if (!m_currentVehicle.name.Contains(m_mecha.name) && m_mechaTimer <= 0f) {
			m_mechaTimer = m_shiftCooldown;

			Shift(m_mecha);
		}
	}

	void ShiftToShip() {
		if (!m_currentVehicle.name.Contains(m_ship.name) && m_shipTimer <= 0f) {
			m_shipTimer = m_shiftCooldown;

			Shift(m_ship);
		}
	}

	void ShiftToTank() {
		if (!m_currentVehicle.name.Contains(m_tank.name) && m_tankTimer <= 0f) {
			m_tankTimer = m_shiftCooldown;

			Shift(m_tank);
		}
	}

	public string GetVehicleType() {
		if (m_currentVehicle) {
			return m_currentVehicle.GetComponent<Vehicle>().m_type;
		}

		return "Ground";
	}
}
