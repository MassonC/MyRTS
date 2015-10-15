// Consider using a fall off funtion to slowly increase the pan speed based on distance from the edge of the screen.
// On the cusp of the buffer zone equals slower movement, while hitting the edge of the screen results in maximum panning speed.

using UnityEngine;
using System.Collections;

namespace PlayerCamera
{

public class Pan : MonoBehaviour 
{
	public float BUFFER = 20;
	public float SPEED = 1;

	float zoomMitigation = 0.01f;

	public Camera cam;
	Vector3 mousePos;

	void Update()
	{
		mousePos = Input.mousePosition;

		//Horizontal
		//Right
		if (mousePos.x >= Screen.width - BUFFER || Input.GetKey(KeyCode.D))
		{
			Vector3 newPos = cam.transform.position;
			//We want the pan speed to increase with distance from the landscape
			newPos.x += (SPEED + (cam.transform.position.y * zoomMitigation)) ;
			cam.transform.position = newPos;
		}
		//Left
		if (mousePos.x <= BUFFER || Input.GetKey(KeyCode.A))
		{
			Vector3 newPos = cam.transform.position;
			newPos.x -= (SPEED + (cam.transform.position.y * zoomMitigation));
			cam.transform.position = newPos;
		}

		//Vertical
		//Up
		if (mousePos.y >= Screen.height - BUFFER || Input.GetKey(KeyCode.W))
		{
			Vector3 newPos = cam.transform.position;
			//Mouse Y is in screen space, this is in world space, hence Z instead of Y
			newPos.z += (SPEED + (cam.transform.position.y * zoomMitigation)); 
			cam.transform.position = newPos;
		}
		//Down
		if (mousePos.y <= BUFFER || Input.GetKey(KeyCode.S))
		{
			Vector3 newPos = cam.transform.position;
			newPos.z -= (SPEED + (cam.transform.position.y * zoomMitigation));
			cam.transform.position = newPos;
		}
	}
}

}