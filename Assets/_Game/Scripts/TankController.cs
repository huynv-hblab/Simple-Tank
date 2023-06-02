using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TankController : MonoBehaviour
{
    public TankMove tankMove;
    public AimTurret aimTurret;
    public Turret[] turrets;

    private void Awake()
    {
        if(tankMove== null)
        {
            tankMove = GetComponentInChildren<TankMove>();
        }
        if(aimTurret == null)
        {
            aimTurret = GetComponentInChildren<AimTurret>();
        }
        if(turrets == null || turrets.Length == 0)
        {
            turrets = GetComponentsInChildren<Turret>();
        }
    }
    public void Shooting()
    {
        foreach(var turret in turrets) 
        { 
            turret.Shooting();
        }
    }

    public void SetBodyMovingVector(Vector2 movementVector)
    {
        tankMove.SetTankMovingVector(movementVector);
    }

    public void TurretMoving(Vector2 pointerPosition)
    {
        aimTurret.TurretMoving(pointerPosition);
    }

}
