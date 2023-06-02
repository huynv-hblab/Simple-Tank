using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyUtil : MonoBehaviour
{
    //to destroy object which contain this script
    public void DestroyHelper()
    {
        Destroy(gameObject);
    }
}
