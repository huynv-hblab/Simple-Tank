using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static DefaultEnemyAI;

public class ShootingState : IState<DefaultEnemyAI>
{
    public float fieldOfVisionForShooting = 60;
    public bool targetInFOV = false;
    public void OnEnter(DefaultEnemyAI enemy)
    {
        Debug.Log("enter shooting state");
    }

    public void OnExecute(DefaultEnemyAI enemy)
    {
        if (!enemy.detector.TargetVisible)
        {
            if(enemy.Patrolstate == EnemyPatrolState.Static)
            {
                enemy.ChangeState(new PatrolState());
            }
            else if(enemy.Patrolstate == EnemyPatrolState.PatrolPath)
            {
                enemy.ChangeState(new PatrolPathState());
            }
        }
        else
        {
            if (TargetInFOV(enemy.tank, enemy.detector))
            {
                enemy.tank.SetBodyMovingVector(Vector2.zero);
                enemy.tank.Shooting();
            }
            enemy.tank.TurretMoving(enemy.detector.Target.position);
        }
    }

    public void OnExit(DefaultEnemyAI enemy)
    {

    }

    private bool TargetInFOV(TankController tank, AIDetector detector)
    {
        targetInFOV = false;
        var direction = detector.Target.position - tank.aimTurret.transform.position;
        if(Vector2.Angle(tank.aimTurret.transform.right, direction) < fieldOfVisionForShooting / 2)
        {
            targetInFOV = true;
            return true;
        }
        return false;
    }
}
