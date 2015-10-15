using UnityEngine;
using System.Collections;
using System;

public class Attack : FSM_State
{
    Unit target;

    public Attack(NavMeshAgent _agent, Unit _target)
    {
        agent = _agent;
        target = _target;
        agent.SetDestination(target.transform.position);
    }

    public override FSM_State Run()
    {
        return new Idle(agent);
    }
}
