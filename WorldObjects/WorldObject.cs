using UnityEngine;
using System.Collections;
using System.Xml;
using System.Xml.Serialization;

public class WorldObject : MonoBehaviour
{
    //public Stats stats;
    public string templateName;

    public int id;
    public DiplomacyManager.SideCode side;

    int health;

    public bool Dead()
    {
        if (health < 0)
        {
            return true;
        }
        return false;
    }

    public void SufferAttack(int attack, int strength)
    {
        /*
        int attackRoll = attack - stats.defense + Random.Range(1, 6);
        int damageRoll = strength - stats.resilience + Random.Range(1, 6);
        if (attackRoll > 3 && damageRoll > 3)
        {
            health--;
            if (health < 1)
            {
                Die();
            }
        }*/
    }

    public void Die()
    {
        //UnitManager.DestroyUnit(this);
    }

}
