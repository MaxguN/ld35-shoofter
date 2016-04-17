using UnityEngine;
using System.Collections;

public class CameraTracking : MonoBehaviour {
    public Transform m_target;

	private float m_height;

	// Use this for initialization
	void Start () {
		GroundTrack();
	}
	
	// Update is called once per frame
	void Update () {
        //Debug.Log(transform.localPosition);

        float player_z = m_target.localPosition.z;
        float distance = player_z - transform.localPosition.z;
        Vector3 newPosition = new Vector3(transform.localPosition.x, transform.localPosition.z, transform.localPosition.z);

        if (distance > 65) {
			newPosition.z = player_z - 65;
        } else if (distance < 60) {
			newPosition.z = player_z - 60;
        }

		newPosition.y = m_height;
		//Debug.Log("Setting y to " + m_height);

		transform.localPosition = newPosition;
	}

	public void AirTrack() {
		m_height = 52f;
	}

	public void GroundTrack() {
		m_height = 36f;
	}
}
