using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatrolState : IState<DefaultEnemyAI>
{
    public float patrolDelay = 4;
    private Vector2 randomDirection = Vector2.zero;
    private float currentPatrolDelay;
    public void OnEnter(DefaultEnemyAI enemy)
    {
        randomDirection = Random.insideUnitCircle;
        Debug.Log("enter patrol state");
    }

    public void OnExecute(DefaultEnemyAI enemy)
    {
        if (enemy.detector.TargetVisible)
        {
            enemy.ChangeState(new ShootingState());
        }
        float angle = Vector2.Angle(enemy.tank.aimTurret.transform.right, randomDirection);
        if(currentPatrolDelay <= 0 && (angle < 2))
        {
            randomDirection = Random.insideUnitCircle;
            currentPatrolDelay = patrolDelay;
        }
        else
        {
            if(currentPatrolDelay > 0)
            {
                currentPatrolDelay -= Time.deltaTime;
            }
            else
            {
                enemy.tank.TurretMoving((Vector2)enemy.tank.aimTurret.transform.position + randomDirection);
            }
        }
    }

    public void OnExit(DefaultEnemyAI enemy)
    {

    }
}
