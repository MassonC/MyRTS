using UnityEngine;
using System.Collections;

public class Unit : WorldObject
{
    public UnitTemplate template;

    AI_FSM brain;
    NavMeshAgent agent;

    void Awake()
    {
        agent = gameObject.GetComponent<NavMeshAgent>();
        brain = new AI_FSM(agent, this);
    }

    void Start()
    {
        agent.speed = template.moveSpeed;
        agent.angularSpeed = template.turnSpeed;
    }

    void Update()
    {
        brain.Run();
    }

    public int AttackStat()
    {
        return 0;
    }

    public int StrengthStat()
    {
        return 0;
    }

   

    /// 
    /// Commands
    ///

    public void TravelTo(Vector3 pos)
    {
        brain.SetStateMove(pos);
    }

    public void Attack(WorldObject target)
    {
       
    }

    public void Guard(WorldObject target)
    {
        brain.SetStateGuard(target);
    }

    // This is completly untested. Should more or less work. Probably. Can't garuntee side effects/lack of bugs if it does work.
    public void Wait(float seconds)
    {
        brain.SetStateWait(seconds);
    }
}
