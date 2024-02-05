using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Raycast : MonoBehaviour
{
    [SerializeField] float rayCastDistanceLeft;
    [SerializeField] float rayCastDistanceRight;
    public LayerMask layerMask;
    [HideInInspector] public bool isDetected;
    GameObject player;
    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }
    void Update()
    {
        RaycastHit2D[] hit = new RaycastHit2D[2];
        hit[0] = Physics2D.Raycast(transform.position + new Vector3(0, 1, 0), -transform.right, rayCastDistanceLeft, layerMask);
        hit[1] = Physics2D.Raycast(transform.position + new Vector3(0, 1, 0), transform.right, rayCastDistanceRight, layerMask);
        if (hit[0].collider != null || hit[1].collider != null)
        {
            isDetected = true;    
        }
        if (isDetected)
        {
            gameObject.GetComponent<Monster>().MonsterMovement(player);
        }
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawRay(transform.position + new Vector3(0, 1, 0), -transform.right * rayCastDistanceLeft);
        Gizmos.DrawRay(transform.position + new Vector3(0, 1, 0), transform.right * rayCastDistanceRight);
    }
}
