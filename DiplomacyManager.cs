
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public static class DiplomacyManager
{
	static int MAX_SIDES = 10;

    public enum SideCode { neutral, side1, side2, side3, side4, side5, side6, side7, side8 };
    public enum diplomacyState { atWar = -2,hostile, neutral, friendly, allied };


    static Side[] sides = new Side[MAX_SIDES];

	public static void Init()
	{
		for (int i = 0; i < MAX_SIDES; i++)
		{
			sides[i] = new Side();
            sides[i].SetSideDiplomacy(i, diplomacyState.allied); //set the Side to ally with itself
		}
	}

	public static void AddSide(SideCode side)
	{
		sides[(int)side].active = true;
	}

	public static void RemoveSide(SideCode side)
	{
		sides[(int)side].active = false;
	}

    public static void SetDiplomacyState(SideCode caller, SideCode target, diplomacyState state)
    {
        sides[(int)caller].SetSideDiplomacy(target, state);
    }

	public static diplomacyState GetDiplomacyState(SideCode asker, SideCode inQuestion) //What is my diplomacy with this group?
	{
		return sides[(int)asker].GetDiplomacyState(inQuestion);
	}


    class Side 
	{		
		public bool active;
		diplomacyState[] diplomacy;

        public Side()
		{
			active = false;
			diplomacy = new diplomacyState[MAX_SIDES];
			for (int i = 0; i < MAX_SIDES; i++)
			{
				SetSideDiplomacy(i, diplomacyState.neutral);
			}
		}

		public void SetSideDiplomacy(SideCode side, diplomacyState state)
		{
			diplomacy[(int)side] = state;
		}

		public void SetSideDiplomacy(int side, diplomacyState state)
		{
			diplomacy[side] = state;
		}

		// Given a side, returns the state of affairs between this side and the given one
		public diplomacyState GetDiplomacyState(SideCode side)
		{
			return diplomacy[(int)side];
		}
	}
}

