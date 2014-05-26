using UnityEngine;
using System.Collections;

[System.Serializable]
public class OrthoCam{
	public float size = 4.5f;
	public float minSize = 2f;
	public float maxSize = 7f;
}

public class CameraFollow : MonoBehaviour {
	public Transform target;
	public float smoothTime = 0.3f;
	public OrthoCam orthoCam;

	private Vector3 velocity = Vector3.zero;
	private Vector3 initialPosition;
	
	void Start () {
		initialPosition = transform.position - target.position;	
	}

	void Update () {
		Vector3 targetPosition = new Vector3(target.position.x + initialPosition.x, 
		                                 	 target.position.y + initialPosition.y, 
		                                 	 target.position.z + initialPosition.z);
		transform.position = Vector3.SmoothDamp(transform.position, targetPosition, ref velocity, smoothTime);
		CameraZoom ();
	}

	void CameraZoom(){
		if(Camera.main.orthographic == true){
			if(Input.GetAxis("Mouse ScrollWheel") < 0){
				orthoCam.size += 0.1f;
			}
			if(Input.GetAxis("Mouse ScrollWheel") > 0){
				orthoCam.size -= 0.1f;
			}
			orthoCam.size = Mathf.Clamp(orthoCam.size, orthoCam.minSize, orthoCam.maxSize);
			Camera.main.orthographicSize = orthoCam.size;
		}
	}
}
