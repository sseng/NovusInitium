using UnityEngine;
using System.Collections;

public class Utilities : MonoBehaviour {
	protected int moveableLayer = 1 << 8;
	protected int enemyLayer = 1 << 9;
	protected Vector3 invalidPosition = new Vector3(-99999, -99999, -99999);

	public Vector3 hitPoint(){
		Ray ray = Camera.main.ScreenPointToRay (Input.mousePosition);
		RaycastHit hit = new RaycastHit ();
		if (Physics.Raycast (ray, out hit, Mathf.Infinity, moveableLayer)) {
			return hit.point;
		} 
		else {
			return invalidPosition;
		}
	}

	public GameObject hitObject(){
		Ray ray = Camera.main.ScreenPointToRay (Input.mousePosition);
		RaycastHit hit = new RaycastHit ();
		if (Physics.Raycast (ray, out hit, Mathf.Infinity, enemyLayer)) {
			return hit.collider.gameObject;
		} else {
			return null;
		}
	}

}
