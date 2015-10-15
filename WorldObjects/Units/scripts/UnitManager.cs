// Deals with unit to unit interaction on the global level,
// including the generation and clean up of units,
// as well as factioning. 
// Faction data is stored elsewhere, this simply assigns units to the proper faction at start up

using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class UnitManager : MonoBehaviour
{ 
	const int MAX_UNITS = 1000;

    public Unit genericUnit;

    static Dictionary<int, Unit> unitInstance = new Dictionary<int, Unit>(); //Replace with list?

    public GameManager gameManager;

    public void SpawnUnit(int templateID, Vector3 pos, DiplomacyManager.SideCode side)
    {
        Unit newUnit = Instantiate(genericUnit, pos, new Quaternion()) as Unit;
        newUnit.template = UnitTemplateManager.UnitReference(templateID);
        newUnit.side = side;
        if (gameManager.GetPlayerSide() == side)
        {
            newUnit.tag = SelectRectangle.SELECTABLE;
        }
        //unitInstance.Add(newUnit.id, newUnit);
    }

    public static void DestroyUnit(Unit unit)
    {
        
        Destroy(unit.gameObject);
    }
    
    void InitilizeWorldObjectList ()
    {

    }
}
