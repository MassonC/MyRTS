/*  AI_Entity is a FSM and a base class for Unit.
    Its not very pretty or clever, but it works. Unit will implemnt the pretty methods so we don't have to deal with this to much.
    It is a switch of switches run in a loop to call specific methods at the appropriate time and allow transition
    to and from differnt states based on the status of the current state.

    Every run of the main update loop, the current status is checked, after which the state is checked in reference to the status.
    This is done through consecutive switches inside status and state specific methods.
    The status switch will call either Activate(), Continue(), Transition(), or ClearState(). Each of these methods (except ClearState())
    is itself a switch statement. They test the state and call the appropriate method for that state based on status.

    This is also where behavior for each state's behavior is implemented.

   
    
    
    Notes:  

                                                  

                                                *Adding new states*

    Every state must implement 3 methods: "X"Activate, "X"Continue, and "X"Transition (X being the state name). 
    These methods must then be added to the switches inside the appropriate state test method, ie Activate(), Continue(), Transition().

                    
                        *Transitions and changing state from outside of AI_Entity (genrally done in Unit)*

    Most Transition() currently calls ChangeStateClean, which will clear out all state specific variables.
    If you want to set state specific variables, do so after calling ChangeStateClean. This ensures no odd behavior due to sticky variables and
    that every state starts with a fresh set of variables that only that state needs.
    
    This applies to calling state changes outside of AI_Entity (Unit mostly) as well (fe, telling a unit to move to a specific spot).

    Exceptions to this are travel and wait. These use ChangeStateSticky, which only alters state and status, leaving other variables alone.
    They also set status to active once they have finished. This means states can wait or travel without disrupting their own active status.
 */

using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public struct AI_FSM
{
    static float GAURD_DISTANCE = 5.0f;

    NavMeshAgent agent;
    Unit unit;

    State state;// { get; private set; }
    Status status; //{ get; private set; }
    Aggression aggression; //{ get; private set; }

    /// 
    /// State specific variables. 
    /// When adding new variables, ensure they are handled correctly in ClearState() and ChangeStateClean()
    /// 
    float timer;
    public State caller { get; private set; }
    WorldObject ally;
    WorldObject enemy;
    Vector3 location;

    public AI_FSM(NavMeshAgent _agent, Unit _unit)
    {
        agent = _agent;
        unit = _unit;

        state = State.idle;
        status = Status.inactive;
        aggression = Aggression.defensive;
        timer = 0;
        ally = null;
        enemy = null;
        location = agent.transform.position;
        caller = State.idle;
    }

    public void Run()
    {
        switch (status)
        {
            case Status.inactive:
                Activate();
                break;
            case Status.active:
                Continue();
                break;
            case Status.complete:
                Transition();
                break;
            case Status.failed:
                ClearState();
                break;

            default:
                ClearState();
                Debug.LogError("AI_Entity status check defaulted");
                break;
        }
    }

    /// 
    /// General Methods
    ///
    void Activate()
    {
        switch (state)
        {
            case State.attack:
                AttackActivate();
                break;
            case State.flee:
                FleeActivate();
                break;
            case State.idle:
                IdleActivate();
                break;
            case State.travel:
                TravelActivate();
                break;
            case State.wander:
                WanderActivate();
                break;
            case State.wait:
                WaitActivate();
                break;
            case State.guard:
                GuardActivate();
                break;

            default:
                ClearState();
                Debug.LogError("AI_Entity Activate() state check defaulted");
                break;
        }
    }

    void Continue()
    {
        switch (state)
        {
            case State.attack:
                AttackContinue();
                break;
            case State.flee:
                FleeContinue();
                break;
            case State.idle:
                IdleContinue();
                break;
            case State.travel:
                TravelContinue();
                break;
            case State.wander:
                WanderContinue();
                break;
            case State.wait:
                WaitContinue();
                break;
            case State.guard:
                GuardContinue();
                break;

            default:
                ClearState();
                Debug.LogError("AI_Entity Continue() state check defaulted");
                break;
        }
    }

    void Transition()
    {
        switch (state)
        {
            case State.attack:
                AttackTransition();
                break;
            case State.flee:
                FleeTransition();
                break;
            case State.idle:
                IdleTransition();
                break;
            case State.travel:
                TravelTransition();
                break;
            case State.wander:
                WanderTransition();
                break;
            case State.wait:
                WaitTransition();
                break;
            case State.guard:
                GuardTransition();
                break;

            default:
                ClearState();
                Debug.LogError("AI_Entity Transition() state check defaulted");
                break;
        }
    }

    void ChangeStateClean(State _state)
    {
        timer = 0;
        ally = null;
        enemy = null;
        location = agent.transform.position;
        caller = State.idle;
        state = _state;
        status = Status.inactive;
    }

    void ChangeStateSticky(State _state)
    {
        state = _state;
        status = Status.inactive;
    }

    void ClearState()
    {
        timer = 0;
        ally = null;
        enemy = null;
        location = agent.transform.position;
        caller = State.idle;
        state = State.idle;
        status = Status.inactive;
    }

    /// State specific methods

    void WaitActivate()
    {
        if (timer > 0)
        {
            timer += Time.fixedTime;
            status = Status.active;
        }
        else
        {
            status = Status.failed;
        }
    }

    void WaitContinue()
    {
        if (--timer < 0)
        {
            status = Status.complete;
        }
    }

    void WaitTransition()
    {
        ChangeStateClean(caller);
    }

    /// Travel   
    void TravelActivate()
    {
        agent.stoppingDistance = 1;
        agent.destination = location;
        status = Status.active;
    }

    void TravelContinue()
    {
        if (DoneMoving())
        {
            status = Status.complete;
        }
    }

    void TravelTransition()
    {
        //agent.Stop();
        ChangeStateClean(caller);
    }

    /// Idle
    void IdleActivate()
    {
        status = Status.active;
    }

    void IdleContinue()
    {
        if (++timer > 1000)
        {
            status = Status.complete;
        }
        if (EnemyNear(out enemy))
        {
            ChangeStateSticky(State.attack);
            caller = State.idle;
        }
    }

    void IdleTransition()
    {
        ChangeStateClean(State.wander);
    }

    /// Attack
    void AttackActivate()
    {
        Debug.Log("Attack Active");
        status = Status.active;
    }

    void AttackContinue()
    {
        Debug.Log("Attack Continuing");
        status = Status.complete;
    }

    void AttackTransition()
    {
        Debug.Log("Finished Attacking. Entering {0}" + caller.ToString());
        ChangeStateClean(caller);
    }

    /// Flee
    void FleeActivate()
    {

    }

    void FleeContinue()
    {

    }

    void FleeTransition()
    {

    }

    /// Wander
    void WanderActivate()
    {
        Vector3 target = new Vector3(Random.Range(-10, 10), 0.5f, Random.Range(-10, 10));
        target += agent.transform.position;
        agent.SetDestination(target);
        status = Status.active;
    }

    void WanderContinue()
    {
        if (caller == State.wait)
        {
            ChangeStateClean(State.wander);
        }
        if (!agent.pathPending)
        {
            if (agent.remainingDistance <= agent.stoppingDistance)
            {
                status = Status.complete;
            }
        }
    }

    void WanderTransition()
    {
        ChangeStateClean(State.wait);
        timer = Random.Range(0.0f, 100.0f);
        caller = State.wander;
    }

    /// Guard
    void GuardActivate()
    {
        if (ally != null)
        {
            agent.stoppingDistance = 3;
            location = ally.transform.position;
            status = Status.active;
        }
        else
        {
            status = Status.failed;
        }
    }

    void GuardContinue()
    {
        if (ally.Dead())
        {
            status = Status.failed;
            return;
        }

        if (!WithinRange(ally.transform.position, GAURD_DISTANCE))
        {
            agent.SetDestination(ally.transform.position);
        }
    }

    void GuardTransition()
    {

    }

    /// Command Functions
    public void SetStateMove(Vector3 pos)
    {
        ChangeStateClean(State.travel);
        location = pos;
    }

    public void SetStateAttack(WorldObject target)
    {
        ChangeStateClean(State.attack);
        enemy = target;
    }

    public void SetStateGuard(WorldObject target)
    {
        ChangeStateClean(State.guard);
        ally = target;
    }

    public void SetStateGuard(Vector3 _location)
    {
        ChangeStateClean(State.guard);
        location = _location;
    }

    public void SetStateWait(float seconds)
    {
        caller = state;
        ChangeStateSticky(State.wait);
        timer = seconds;
    }
    

    /// Helper Functions
    bool DoneMoving()
    {
        if (!agent.pathPending)
        {
            if (agent.remainingDistance <= agent.stoppingDistance)
            {
                return true;
            }
        }
        return false;
    }

    bool WithinRange(Vector3 target, float range)
    {
        //Calculate everthing in square space for efficeny
        Vector3 pos = agent.transform.position;
        pos.x *= pos.x;
        pos.y *= pos.y;
        pos.z *= pos.z;

        target.x *= target.x;
        target.y *= target.y;
        target.z *= target.z;

        range *= range;

        if( Mathf.Abs( (target.x - pos.x) + (target.z - pos.z) ) > range )
        {
            return false;
        }
        
        return true;
    }

    bool EnemyNear(out WorldObject enemy)
    {
        RaycastHit hit;
        if (Physics.SphereCast(agent.transform.position, GAURD_DISTANCE, agent.transform.forward, out hit, GAURD_DISTANCE * 2))
        {
            WorldObject obj = hit.collider.GetComponent<WorldObject>();
            if (obj != null)
            {
                if (DiplomacyManager.GetDiplomacyState(unit.side, obj.side) < (int)DiplomacyManager.diplomacyState.neutral)
                {
                    enemy = obj;
                    return true;
                }
            }
        }
        enemy = null;
        return false;
    }

    /// 
    /// Enums
    /// 
    public enum State
    {
        idle,
        travel,
        attack,
        flee,
        wander,
        wait,
        guard
    };

    public enum Status
    {
        active,
        complete,
        failed,
        inactive
    };

    public enum Aggression
    {
        aggresive,
        defensive,
        passive,
        rampant
    };
}