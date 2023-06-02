using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DefaultEnemyAI : MonoBehaviour
{
    public enum EnemyPatrolState
    {
        Static,
        PatrolPath
    }

    IState<DefaultEnemyAI> currentState;
    public TankController tank;
    public AIDetector detector;
    public EnemyPatrolState Patrolstate;

    private void Awake()
    {
        detector = GetComponentInChildren<AIDetector>();
        tank = GetComponentInChildren<TankController>();
    }

    private void Start()
    {
        if(Patrolstate == EnemyPatrolState.Static)
        {
            ChangeState(new PatrolState());
        }
        else
        {
            ChangeState(new PatrolPathState());
        }
    }
    private void Update()
    {
        currentState.OnExecute(this);
    }

    public void ChangeState(IState<DefaultEnemyAI> state)
    {
        if (currentState != null)
        {
            currentState.OnExit(this);
        }
        currentState = state;
        if (currentState != null)
        {
            currentState.OnEnter(this);
        }
    }
}
