//TODO this should be able to account for group (formation) movement
using UnityEngine;
using System.Collections;

public class Travel : FSM_State 
{
	//Vector3 target;

	public Travel(NavMeshAgent passedAgent, Vector3 _target)
	{
        name = "travel";
		agent = passedAgent;
		agent.SetDestination(_target);
	}

	public override FSM_State Run ()
	{
        Debug.Log("jhlh");
        if (Vector3.Distance(agent.transform.position, agent.destination) < 2)
		{
			return new Idle(agent);
		}
		return this;
	}
}
