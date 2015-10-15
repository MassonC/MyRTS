using UnityEngine;
using System.Collections;

public class Idle : FSM_State 
{
	float waitTime;

	public Idle(NavMeshAgent passedAgent)
	{
        name = "idle";
		agent = passedAgent;
		//Debug.Log("Idle");
		waitTime = Time.fixedTime + Random.Range(1,5);
	}

	public override FSM_State Run ()
	{
        if (Time.fixedTime >= waitTime)
		{
			return new Wander(agent);
		}
		return this;
	}
}
