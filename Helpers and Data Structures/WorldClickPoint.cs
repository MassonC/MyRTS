using UnityEngine;
using System.Collections;

public class WorldClickPoint 
{
	public GameObject gameObject {get; private set;}
	public Vector3 worldHit {get; private set;}
	public WorldObject worldObject {get; private set;}
	
	public WorldClickPoint()
	{
		Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
		RaycastHit hit;
		if (Physics.Raycast(ray, out hit))
		{
			if (hit.collider.gameObject != null && hit.collider.gameObject.tag != "Terrain")
			{
				gameObject = hit.collider.gameObject;
				worldHit = gameObject.transform.position;
                worldObject = gameObject.GetComponent<Unit>();
			}
			else 
			{
				gameObject = null;
                worldObject = null;
				worldHit = hit.point;
				Debug.Log(string.Format("Clicked on world position {0}.", hit.point));
			}
		}
	}
}
