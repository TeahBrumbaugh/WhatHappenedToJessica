using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallReveal : MonoBehaviour
{
    [SerializeField] private BoxCollider room;
    [SerializeField] private GameObject wall;

    private void OnTriggerEnter(Collider collision)
    {
        wall.SetActive(false);
    }

    private void OnTriggerExit(Collider collision)
    {
        wall.SetActive(true);
    }
}
