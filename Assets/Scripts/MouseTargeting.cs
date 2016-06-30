using UnityEngine;
using System.Collections;

public class MouseTargeting : MonoBehaviour {
	public Texture2D m_cursor;
	public LineRenderer m_laser;

	private int groundLaserLayer = 1 << 8;
	private int airLaserLayer = 1 << 9;

	private int currentLayer;
	private float height;

	private Vector3 m_currentDirection;

	// Use this for initialization
	void Start() {
		Cursor.SetCursor(m_cursor, Vector2.zero, CursorMode.Auto);
		//Cursor.visible = false;
		GroundControl();
		m_currentDirection = Vector3.forward;
	}

	// Update is called once per frame
	void Update() {
		RaycastHit hit;
		Vector3 playerPosition = transform.localPosition;
		Vector3 targetPosition = Vector3.zero;
		Ray cursorRay = Camera.main.ScreenPointToRay(Input.mousePosition);

		if (Physics.Raycast(cursorRay, out hit, Mathf.Infinity, currentLayer)) {
			targetPosition = hit.point;
			//targetPosition.x += 1.5f;
			//targetPosition.z -= 3f;
		}

		playerPosition.y = height;

		m_currentDirection = targetPosition - playerPosition;
		m_currentDirection.Normalize();

		if (GameObject.FindGameObjectWithTag("Player") && GameObject.FindGameObjectWithTag("Player").GetComponent<Vehicle>()) {
			GameObject.FindGameObjectWithTag("Player").GetComponent<Vehicle>().SetOrientation(m_currentDirection);
		}
		//Debug.Log(targetPosition + " - " + playerPosition + " = " + direction);

		m_laser.SetPosition(0, playerPosition);


		if (Physics.Raycast(playerPosition, m_currentDirection, out hit)) {
			m_laser.SetPosition(1, hit.point);
		}
	}

	public void GroundControl() {
		currentLayer = groundLaserLayer;
		height = 6f;
	}

	public void AirControl() {
		currentLayer = airLaserLayer;
		height = 18.5f;
	}

	public Vector3 GetDirection() {
		return m_currentDirection;
	}
}