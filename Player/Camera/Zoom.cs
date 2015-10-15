//TODO Clamp altitude to min and max

using UnityEngine;
using System.Collections;

namespace PlayerCamera
{

public class Zoom : MonoBehaviour 
{
	public Camera cam;
	public float SPEED = 8;
	public float MAX = 100;
	public float MIN = 2;
	// Update is called once per frame
	void Update () 
	{
		//Up
		if (Input.GetAxis("Mouse ScrollWheel") < 0)
		{
			Vector3 newPos = cam.transform.position;
			newPos.y += SPEED;
			cam.transform.position = newPos;
		}
		//Down
		else if (Input.GetAxis("Mouse ScrollWheel") > 0)
		{
			Vector3 newPos = cam.transform.position;
			newPos.y -= SPEED;
			cam.transform.position = newPos;
		}
	}
}

}