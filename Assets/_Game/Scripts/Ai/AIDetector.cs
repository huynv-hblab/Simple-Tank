using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIDetector : MonoBehaviour
{
    [Range(1, 15)]
    [SerializeField] private float viewRadius = 11;
    [SerializeField] private float detectionCheckDelay = 0.1f;
    [SerializeField]
    private Transform target = null;
    [SerializeField] private LayerMask playerLayerMask;
    [SerializeField] private LayerMask visiblityLayer;

    //check target visible before check Target
    [field: SerializeField]
    public bool TargetVisible { get; private set; }

    public Transform Target
    {
        get => target;
        set
        {
            target = value;
            TargetVisible = false;
        }
    }

    private void Start()
    {
        StartCoroutine(DetectionCoroutine());
    }

    private void Update()
    {
        if(Target != null)
        {
            TargetVisible = ChecktargetVisible();
        }
    }

    private bool ChecktargetVisible()
    {
        var result = Physics2D.Raycast(transform.position, Target.position - transform.position, viewRadius, visiblityLayer);
        if(result.collider != null)
        {
            //binary for check if collider's layer in playerLayerMask or not
            return (playerLayerMask & (1 << result.collider.gameObject.layer)) != 0;
        }
        return false;
    }

    private void DetectTarget()
    {
        if(Target == null)
        {
            CheckIfPlayerInRange();
        }
        else if(Target != null)
        {
            //if determined object -> check if the object is out of range or not
            DetectIfOutOfRange();
        }
    }

    private void DetectIfOutOfRange()
    {
        if(Target == null || Target.gameObject.activeSelf == false || 
            Vector2.Distance(transform.position, Target.position) > viewRadius) 
        {
            Target = null;
        }
    }

    private void CheckIfPlayerInRange()
    {
        Collider2D collision = Physics2D.OverlapCircle(transform.position, viewRadius, playerLayerMask);
        if(collision != null)
        {
            Target = collision.transform;
        }
    }

    //check target will have cooldown 0.1f
    IEnumerator DetectionCoroutine()
    {
        yield return new WaitForSeconds(detectionCheckDelay);
        DetectTarget();
        StartCoroutine(DetectionCoroutine());
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, viewRadius);
    }
}
