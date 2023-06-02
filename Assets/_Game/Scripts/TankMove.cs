using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TankMove : MonoBehaviour
{
    private Rigidbody2D rb2D;
    [SerializeField] private float currentSpeed = 0f;
    private Vector2 movementVector;
    [SerializeField] private float direction = 0f;
    public TankMovementData movementData;

    private void Awake()
    {
        rb2D = GetComponentInParent<Rigidbody2D>();
    }
    public void SetTankMovingVector(Vector2 movementVector)
    {
        this.movementVector = movementVector;
        CalculSpeed(movementVector);
        if (movementVector.y > 0)
        {
            direction = 1f;
        }
        else if( movementVector.y < 0)
        {
            direction = -1f;
        }
    }
    void FixedUpdate()
    {
        Moving();
    }

    private void CalculSpeed(Vector2 momentVector)
    {
        if (Input.GetButtonDown("Vertical") && currentSpeed > 0f)
        {
            currentSpeed = 0f;
        }
        if(Mathf.Abs(movementVector.y) > 0)
        {
            currentSpeed += movementData.acceleration * Time.deltaTime;
        }
        else
        {
            currentSpeed -= movementData.deacceleration * Time.deltaTime;
        }
        currentSpeed = Mathf.Clamp(currentSpeed, 0f, movementData.maxSpeed);
    }
    public void Moving()
    {
        rb2D.velocity = (Vector2)transform.up * currentSpeed * direction * Time.fixedDeltaTime;
        rb2D.MoveRotation(transform.rotation * Quaternion.Euler(0, 0, -movementVector.x * movementData.rotationSpeed * Time.fixedDeltaTime));
    }
}
