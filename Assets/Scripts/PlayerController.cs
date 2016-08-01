using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class PlayerController : MonoBehaviour {
    public Transform m_mecha;
    public Transform m_tank;
    public Transform m_ship;
	public float m_shiftCooldown = 5f;
	public float m_forwardSpeed = 50f;
	public float m_strafeSpeed = 50f;
	public float m_rotateSpeed = 2f;

    private Rigidbody m_rigidbody;
    private Transform m_currentVehicle;
	private AudioSource m_source;
	private UserInterface m_ui;

	private float m_mechaTimer = 0f;
	private float m_shipTimer = 0f;
	private float m_tankTimer = 0f;

	private float m_mechaHP = 0f;
	private float m_shipHP = 0f;
	private float m_tankHP = 0f;

	private bool m_shifting = false;

	private int m_score = 0;

	// Use this for initialization
	void Start () {
        m_rigidbody = GetComponent<Rigidbody>();
		m_source = GetComponent<AudioSource>();
		m_ui = GameObject.FindGameObjectWithTag("UI").GetComponent<UserInterface>();

		m_mechaHP = m_mecha.GetComponent<Health>().m_healthPoints;
		m_shipHP = m_ship.GetComponent<Health>().m_healthPoints;
		m_tankHP = m_tank.GetComponent<Health>().m_healthPoints;

		Shift(m_mecha, m_mechaHP);
	}
	
	// Update is called once per frame
	void Update () {
		bool shiftMecha = Input.GetButton("Shift1");
		bool shiftShip = Input.GetButton("Shift2");
		bool shiftTank = Input.GetButton("Shift3");

		if (m_mechaTimer > 0f) {
			m_mechaTimer -= Time.deltaTime;
			m_ui.SetMechaCooldown(m_mechaTimer);
		}
		if (m_shipTimer > 0f) {
			m_shipTimer -= Time.deltaTime;
			m_ui.SetShipCooldown(m_shipTimer);
		}
		if (m_tankTimer > 0f) {
			m_tankTimer -= Time.deltaTime;
			m_ui.SetTankCooldown(m_tankTimer);
		}

		if (shiftMecha) {
			ShiftToMecha();
		} else if (shiftShip) {
			ShiftToShip();
		} else if (shiftTank) {
			ShiftToTank();
		}

		m_ui.SetScore(m_score);
	}

    void FixedUpdate() {
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");
		bool fire = Input.GetButton("Fire1");

		if (GameObject.FindGameObjectWithTag("Player")) {
			Vehicle vehicle = GameObject.FindGameObjectWithTag("Player").GetComponent<Vehicle>();

			//Debug.Log(GameObject.FindGameObjectWithTag("Player"));

			if (vehicle && !m_shifting) {
				switch (vehicle.m_vehicle) {
					case Vehicle.VehicleType.Mech:
						m_rigidbody.velocity = vehicle.m_rotation * (new Vector3(0, 0, z) * m_forwardSpeed + new Vector3(x, 0, 0) * m_strafeSpeed);
						Animator animator = vehicle.GetComponent<Animator>();
						animator.SetBool("Move", x > 0.1f || x < -0.1f || z > 0.1f || z < -0.1f);
						break;
					case Vehicle.VehicleType.Ship:
						m_rigidbody.velocity = vehicle.m_rotation * (new Vector3(0, 0, z) * m_forwardSpeed + new Vector3(x, 0, 0) * m_strafeSpeed);
						vehicle.Lean(x);
						break;
					case Vehicle.VehicleType.Tank:
						// ToDo: Decrease speed when tank moving horizontally (use strafeSpeed)
						transform.localEulerAngles += new Vector3(0, x, 0) * m_rotateSpeed;
						m_rigidbody.velocity = vehicle.transform.rotation * new Vector3(0, 0, -z) * m_forwardSpeed;
						break;
				}
			} else {
				m_rigidbody.velocity = Vector3.zero;
			}

			//Debug.Log(m_rigidbody.velocity);
		}


		
		if (x != 0f || z != 0f) {
			Move();
		} else {
			Stop();
		}

		if (!m_shifting) {
			m_currentVehicle.GetComponent<Weapon>().Shoot(fire, GetComponent<MouseTargeting>().GetDirection());
		}
    }

    void Shift(Transform newVehicle, float health) {
		StartCoroutine(TimedShift(newVehicle, health));
	}

	IEnumerator TimedShift(Transform newVehicle, float health) {
		Animator animator;

		if (m_currentVehicle) {
			if (m_currentVehicle.name.Contains("mecha")) {
				m_mechaHP = m_currentVehicle.GetComponent<Health>().GetHealth();
			} else if (m_currentVehicle.name.Contains("ship")) {
				m_shipHP = m_currentVehicle.GetComponent<Health>().GetHealth();
			} else if (m_currentVehicle.name.Contains("tank")) {
				m_tankHP = m_currentVehicle.GetComponent<Health>().GetHealth();
			}

			animator = m_currentVehicle.GetComponent<Animator>();
			if (animator) {
				animator.SetTrigger("Shapeshift");
				m_shifting = true;
				while (!animator.GetCurrentAnimatorStateInfo(animator.GetLayerIndex("Base Layer")).IsName("Shapeshift Out")) yield return new WaitForSeconds(0.01f);
				yield return new WaitForSeconds(animator.GetCurrentAnimatorStateInfo(animator.GetLayerIndex("Base Layer")).length);
				m_shifting = false;
				Debug.Log("Shift out over");
			}

			animator = null;

			Destroy(m_currentVehicle.gameObject);
		}

		m_currentVehicle = (Transform)Instantiate(newVehicle);
		m_currentVehicle.parent = transform;
		m_currentVehicle.localPosition = new Vector3(0, m_currentVehicle.localPosition.y, 0);
		m_currentVehicle.GetComponent<Health>().SetHealth(health);

		if (newVehicle == m_ship) {
			GetComponent<MouseTargeting>().AirControl();
			Camera.main.GetComponent<CameraTracking>().AirTrack();
		} else {
			GetComponent<MouseTargeting>().GroundControl();
			Camera.main.GetComponent<CameraTracking>().GroundTrack();
		}

		animator = m_currentVehicle.GetComponent<Animator>();
		if (animator) {
			m_shifting = true;
			while (!animator.GetCurrentAnimatorStateInfo(animator.GetLayerIndex("Base Layer")).IsName("Shapeshift In")) yield return new WaitForSeconds(0.01f);
			yield return new WaitForSeconds(animator.GetCurrentAnimatorStateInfo(animator.GetLayerIndex("Base Layer")).length);
			m_shifting = false;
			Debug.Log("Shift in over");
		}
	}

	bool ShiftToMecha() {
		if (!m_currentVehicle.name.Contains(m_mecha.name) && m_mechaTimer <= 0f && m_mechaHP > 0) {
			m_mechaTimer = m_shiftCooldown;

			Shift(m_mecha, m_mechaHP);

			return true;
		}
		return false;
	}

	bool ShiftToShip() {
		if (!m_currentVehicle.name.Contains(m_ship.name) && m_shipTimer <= 0f && m_shipHP > 0) {
			m_shipTimer = m_shiftCooldown;

			Shift(m_ship, m_shipHP);

			return true;
		}
		return false;
	}

	bool ShiftToTank() {
		if (!m_currentVehicle.name.Contains(m_tank.name) && m_tankTimer <= 0f && m_tankHP > 0) {
			m_tankTimer = m_shiftCooldown;

			Shift(m_tank, m_tankHP);

			return true;
		}
		return false;
	}

	public void ShiftToNext() {
		string vehicle = m_currentVehicle.name;

		if (vehicle.Contains("mecha")) {
			if (!ShiftToShip() && !ShiftToTank()) {
				Lose();
			}
		} else if(vehicle.Contains("ship")) {
			if (!ShiftToTank() && !ShiftToMecha()) {
				Lose();
			}
		} else if (vehicle.Contains("tank")) {
			if (!ShiftToMecha() && !ShiftToShip()) {
				Lose();
			}
		}
	}

	public string GetVehicleType() {
		if (m_currentVehicle) {
			return m_currentVehicle.GetComponent<Vehicle>().m_type;
		}

		return "Ground";
	}

	void Move() {
		if (m_source && !m_source.isPlaying) {
			m_source.clip = m_currentVehicle.GetComponent<Vehicle>().m_moveSound;
			m_source.Play();
			m_source.loop = true;
		}
	}

	void Stop() {
		if (m_source && m_source.isPlaying) {
			m_source.Stop();
		}
	}

	public void AddScore(int score) {
		m_score += score;
	}

	void Win() {
		SceneManager.LoadScene("endgame");
	}

	void Lose() {
		SceneManager.LoadScene("endgame");
	}
}
