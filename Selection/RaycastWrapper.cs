// Not a fan of this being static, but I don't like making new objects anytime I click only
// too imiediatly leave them for garbage collection either.
using UnityEngine;
using System.Collections;

public static class RaycastWrapper  
{
	public static Unit worldObject;
	public static Vector3 worldHit;

	public static void GetClickInfo()
	{
		Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
		RaycastHit hit;
		if (Physics.Raycast(ray, out hit))
		{
			if (hit.collider.gameObject != null && hit.collider.gameObject.tag != "Terrain")
			{
				worldObject = hit.collider.gameObject.GetComponent<Unit>();
				//worldHit = worldObject.transform.position; 
			}
			else 
			{
				worldObject = null;
				worldHit = hit.point;
				//Debug.Log(string.Format("Clicked on world position {0}.", hit.point));
			}
		}
	}

	public static void Reset()
	{
		worldObject = null;
		worldHit = new Vector3(0, 0, 0);
	}

	public static Unit GetWorldObject()
	{
		return worldObject;
	}

	public static Vector3 GetWorldHitPoint()
	{
		return worldHit;
	}

	public static Unit GetUnit()
	{
		return worldObject.GetComponent<Unit>();
	}
}
