using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyFSM
{
    public EnemyState[] states;
    public EnemyAgent agent;
    public EnemyStateId currentStateId;

    public EnemyFSM(EnemyAgent agent)
    {
        this.agent = agent;
        int numStates = System.Enum.GetNames(typeof(EnemyStateId)).Length;
        states = new EnemyState[numStates];
    }

    public void RegisterState(EnemyState state)
    {
        int index = (int)state.GetId();
        states[index] = state;
    }

    public void UpdateState()
    {
        GetState(currentStateId)?.UpdateState(agent);
    }
    
    public void ChangeState(EnemyStateId newStateId)
    {
        // happen in one frame
        GetState(currentStateId)?.ExitState(agent);
        currentStateId = newStateId;
        GetState(currentStateId)?.EnterState(agent);
    }

    public EnemyState GetState(EnemyStateId stateId)
    {
        return states[(int)stateId];
    }

}
