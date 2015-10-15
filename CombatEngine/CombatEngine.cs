using UnityEngine;
using System.Collections;

public static class Combat
{
    public static void Fight(Unit attacker, Unit defender)
    {
        defender.SufferAttack(attacker.AttackStat(), attacker.StrengthStat());
    }
}
