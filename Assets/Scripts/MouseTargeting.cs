using UnityEngine;
using System.Collections;

public class MouseTargeting : MonoBehaviour {
	public Texture2D m_cursor;
	public LineRenderer m_laser;

	private int groundLaserLayer = 1 << 8;
	private int airLaserLayer = 1 << 9;

	// Use this for initialization
	void Start() {
		Cursor.SetCursor(m_cursor, Vector2.zero, CursorMode.Auto);
		//Cursor.visible = false;
	}

	// Update is called once per frame
	void Update() {
		RaycastHit hit;
		Vector3 cursorPosition = Input.mousePosition;
		cursorPosition.z = 65f;

		Vector3 playerPosition = transform.localPosition;
		Vector3 targetPosition = Vector3.zero;
		Ray cursorRay = Camera.main.ScreenPointToRay(cursorPosition);

		if (Physics.Raycast(cursorRay, out hit, Mathf.Infinity, groundLaserLayer)) {
			targetPosition = hit.point;
			targetPosition.x += 1.5f;
			targetPosition.z -= 3f;
		}

		playerPosition.y = 6f;

		Vector3 direction = targetPosition - playerPosition;
		direction.Normalize();
		//Debug.Log(targetPosition + " - " + playerPosition + " = " + direction);

		m_laser.SetPosition(0, playerPosition);


		if (Physics.Raycast(playerPosition, direction, out hit)) {
			m_laser.SetPosition(1, hit.point);
		}
	}
}