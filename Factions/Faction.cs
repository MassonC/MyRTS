using System.Collections.Generic;

public class Faction
{
    static int numberOfFactions;

    string name;

    List<UnitTemplate> availableUnits = new List<UnitTemplate>();

    public void AddWorldObject(UnitTemplate unit)
    {
        availableUnits.Add(unit);
    }
}
