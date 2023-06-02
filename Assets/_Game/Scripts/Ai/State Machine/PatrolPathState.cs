using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class PatrolPathState : MonoBehaviour, IState<DefaultEnemyAI> 
{
    public PatrolPath patrolPath;
    [Range(0.1f, 1f)] 
    public float arriveDistance = 1;
    public float waitTime = 0.5f;
    public float time;
    [SerializeField]
    private bool isWaiting = false;
    [SerializeField]
    Vector2 currentPatrolTarget = Vector2.zero;
    bool isInitialized = false;

    private int currentIndex = -1;
    public void OnEnter(DefaultEnemyAI enemy)
    {
        if (patrolPath == null)
        {
            Debug.Log("get patrol Path");
            patrolPath = enemy.GetComponentInChildren<PatrolPath>();
            if (patrolPath != null)
            {
                Debug.Log("patrolPath" + patrolPath.Length);
            }
        }
    }

    public void OnExecute(DefaultEnemyAI enemy)
    {
        if (enemy.detector.TargetVisible)
        {
            enemy.ChangeState(new ShootingState());
        }
        if (!isWaiting)
        {
            if (patrolPath.Length < 2)
                return;
            if(!isInitialized)
            {
                var currentPathPoints = patrolPath.GetClosetPathPoint(enemy.tank.transform.position);
                this.currentIndex = currentPathPoints.Index;
                this.currentPatrolTarget = currentPathPoints.Position;
                isInitialized = true;
            }
            //when tank get close to destination
            if (Vector2.Distance(enemy.tank.transform.position, currentPatrolTarget) < arriveDistance)
            {
                isWaiting = true;
                //time = waitTime;
                //time -= Time.deltaTime;
                //if(time <= 0)
                //{
                //}
                NextPointDetect();
            }
            Vector2 directionToGo = currentPatrolTarget - (Vector2)enemy.tank.tankMove.transform.position;
            var dotProduct = Vector2.Dot(enemy.tank.tankMove.transform.up, directionToGo.normalized);

            if (dotProduct < 0.98f)
            {
                var crossProduct = Vector3.Cross(enemy.tank.tankMove.transform.up, directionToGo.normalized);
                int rotationResult = crossProduct.z >= 0 ? -1 : 1;
                enemy.tank.SetBodyMovingVector(new Vector2(rotationResult, 1));
            }
            else
            {
                enemy.tank.SetBodyMovingVector(Vector2.up);
            }
        }
    }

    public void OnExit(DefaultEnemyAI enemy)
    {

    }

    void NextPointDetect()
    {
        var nextPathPoint = patrolPath.GetNextPathPoint(currentIndex);
        currentPatrolTarget = nextPathPoint.Position;
        currentIndex = nextPathPoint.Index;
        isWaiting = false;
    }
}
