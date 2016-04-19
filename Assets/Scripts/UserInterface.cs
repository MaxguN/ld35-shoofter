using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class UserInterface : MonoBehaviour {
	public Text m_score;
	public Text m_healthPoints;
	public Image m_shiftMecha;
	public Text m_mechaCD;
	public Image m_shiftShip;
	public Text m_shipCD;
	public Image m_shiftTank;
	public Text m_tankCD;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void SetScore(int score) {
		m_score.text = "" + score;
	}

	public void SetHP(int hp) {
		m_healthPoints.text = "" + hp;
	}

	public void SetMechaCooldown(float cooldown) {
		if (cooldown > 0) {
			m_mechaCD.text = "" + Mathf.Ceil(cooldown);
			m_shiftMecha.color = new Color(0.2f, 0.2f, 0.2f, cooldown / 15f);
		} else {
			m_mechaCD.text = "";
			m_shiftMecha.color = new Color(1f, 1f, 1f, 1f);
		}
	}

	public void SetShipCooldown(float cooldown) {
		if (cooldown > 0) {
			m_shipCD.text = "" + Mathf.Ceil(cooldown);
			m_shiftShip.color = new Color(0.2f, 0.2f, 0.2f, cooldown / 15f);
		} else {
			m_shipCD.text = "";
			m_shiftShip.color = new Color(1f, 1f, 1f, 1f);
		}
	}

	public void SetTankCooldown(float cooldown) {
		if (cooldown > 0) {
			m_tankCD.text = "" + Mathf.Ceil(cooldown);
			m_shiftTank.color = new Color(0.2f, 0.2f, 0.2f, cooldown / 15f);
		} else {
			m_tankCD.text = "";
			m_shiftTank.color = new Color(1f, 1f, 1f, 1f);
		}
	}
}
