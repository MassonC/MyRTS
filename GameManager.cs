using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour
{
    public static string STATS_DATA_PATH = Application.dataPath + "/WorldObjectStats/";

    public Player player;
    public UnitManager unitManager;
    public UnitTemplateManager templateManager;

    void Awake()
    {
        DiplomacyManager.Init();
        templateManager.Init();
    }

    void Start()
    {
        DiplomacyManager.AddSide(player.side);
        DiplomacyManager.SetDiplomacyState(player.side, DiplomacyManager.SideCode.side2, DiplomacyManager.diplomacyState.atWar);
        DiplomacyManager.SetDiplomacyState(player.side, DiplomacyManager.SideCode.side3, DiplomacyManager.diplomacyState.atWar);

        DiplomacyManager.AddSide(DiplomacyManager.SideCode.side2);
        DiplomacyManager.SetDiplomacyState(DiplomacyManager.SideCode.side2, DiplomacyManager.SideCode.side1, DiplomacyManager.diplomacyState.atWar);
        DiplomacyManager.SetDiplomacyState(DiplomacyManager.SideCode.side2, DiplomacyManager.SideCode.side3, DiplomacyManager.diplomacyState.atWar);

        DiplomacyManager.AddSide(DiplomacyManager.SideCode.side3);
        DiplomacyManager.SetDiplomacyState(DiplomacyManager.SideCode.side3, DiplomacyManager.SideCode.side1, DiplomacyManager.diplomacyState.atWar);
        DiplomacyManager.SetDiplomacyState(DiplomacyManager.SideCode.side3, DiplomacyManager.SideCode.side2, DiplomacyManager.diplomacyState.atWar);

        /*unitManager.SpawnUnit(1, new Vector3(0, 0, 0), DiplomacyManager.SideCode.side1);
        unitManager.SpawnUnit(2, new Vector3(10, 0, 0), DiplomacyManager.SideCode.side1);
        unitManager.SpawnUnit(1, new Vector3(0, 0, 10), DiplomacyManager.SideCode.side1);*/

        for (int i = 0; i < 10; i++)
        {
            for (int j = 0; j < 10; j++)
            {
                unitManager.SpawnUnit(1, new Vector3(i, 0, j), DiplomacyManager.SideCode.side1);
            }
        }
    }

    public DiplomacyManager.SideCode GetPlayerSide()
    {
        return player.side;
    }
}

