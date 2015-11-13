using UnityEngine;
using System.Collections;

public class CameraControl : MonoBehaviour {
	public GameObject target;
	public float minFov = 3.5f;
	public float maxFov = 7f;
	public float sensitivity = 1f;
	public float rotate_Speed = 6f;
	public float moveX_Speed = 0.7f;
	public float moveY_Speed = 0.7f;
	
	private Vector3 originPosition;
	private Quaternion originRotation;

	void Start()
	{

	}

	void FixedUpdate()
	{
		// Zoom In & Out with the wheel
		float fov = Camera.main.fieldOfView;
		fov += -Input.GetAxis("Mouse ScrollWheel") * sensitivity;
		fov = Mathf.Clamp(fov, minFov, maxFov);
		Camera.main.fieldOfView = fov;

		// Rotate the camera 
		if (Input.GetMouseButton(1))
		{
			float mousX = Input.GetAxis("Mouse X");
			transform.RotateAround(target.transform.position, new Vector3(0, mousX, 0), rotate_Speed);
		}

	
	}

	
	
}
