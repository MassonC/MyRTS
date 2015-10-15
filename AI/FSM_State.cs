// Consider creating an AI manager which runs the test in FixedUpdate, adding units that need an update to a que, then 
// telling only those units to update

using UnityEngine;
using System.Collections;

public abstract class FSM_State 
{
    public string name { get; internal set; }
	protected NavMeshAgent agent;
	public abstract FSM_State Run();
	//public abstract FSM_State TransitionState();
}
