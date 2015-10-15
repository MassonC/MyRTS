using UnityEngine;
using System.Collections;

public class Wander : FSM_State 
{
	const float timeOutValue = 10;

	float timeInState = 0;
	float lastUpdate = Time.fixedTime;

	public Wander(NavMeshAgent passedAgent)
	{
        //Debug.Log("Wandering");
        name = "wander";
		agent = passedAgent;
		Vector3 target = new Vector3(Random.Range(-10,10), 0, Random.Range(-10,10));
		target += agent.transform.position;
		agent.SetDestination(target);
	}

	public override FSM_State Run ()
	{
		timeInState = Time.fixedTime - lastUpdate;
		lastUpdate = Time.fixedTime;

		if (timeInState > timeOutValue)
		{
			Vector3 target = new Vector3(Random.Range(-10,10), 0, Random.Range(-10,10));
			target += agent.transform.position;
			agent.SetDestination(target);
		}
		if (agent.hasPath)
		{
				return this;
		}
		if (!agent.hasPath)
		{
			agent.SetDestination(agent.transform.position);
			return new Idle(agent);
		}
		return new Idle(agent);
	}
}
