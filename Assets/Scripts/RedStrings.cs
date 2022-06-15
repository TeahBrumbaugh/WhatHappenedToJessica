using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RedStrings : MonoBehaviour
{
    private LineRenderer redString;
    [SerializeField] private Transform pointA;
    [SerializeField] private Transform pointB;

    private void Awake()
    {
        redString = GetComponent<LineRenderer>();
        redString.SetPosition(0, pointA.position);
        redString.SetPosition(1, pointB.position);
    }
}
