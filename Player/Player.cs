// This class is used for storing and retreiving information about the player in game.
// It is also in charge of intilizing hotkeys etc

//TODO Set up Hot Key customization storage and intitilization;

using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Player : MonoBehaviour 
{
	public DiplomacyManager.SideCode side;
    public SelectRectangle selector;

    List<GameObject> selection;
    
	void Start()
	{

    }

    public void OnEndSelect(List<GameObject> selectedObjects)
    {
        Debug.Log("Player has a selection");
        selection = selectedObjects;
    }

    void Update()
    { 
        if (Input.GetMouseButtonUp(0))
        {
            selection = selector.GetSelectedObjects();
        }
		//Issue orders, context sensitive
		if (Input.GetMouseButtonDown(1) && selection.Count > 0)
		{
			WorldClickPoint click = new WorldClickPoint();

			//Clicked ground, Move order
			if (click.gameObject == null) 
			{
				for(int i = 0; i < selection.Count; i++)
                {
                    selection[i].GetComponent<Unit>().TravelTo(click.worldHit);
                }
			}
			//Clicked unit, attack/guard order
			else if (click.worldObject != null)
			{
				if (DiplomacyManager.GetDiplomacyState(side, click.worldObject.side) < (int)DiplomacyManager.diplomacyState.neutral)
				{
                    for (int i = 0; i < selection.Count; i++)
                    {
                        selection[i].GetComponent<Unit>().Attack(click.worldObject);
                    }
                }
				else if (DiplomacyManager.GetDiplomacyState(side, click.worldObject.side) > (int)DiplomacyManager.diplomacyState.neutral)
				{
                    for (int i = 0; i < selection.Count; i++)
                    {
                        selection[i].GetComponent<Unit>().Guard(click.worldObject);
                    }
                }
			}
		}
	}
}
