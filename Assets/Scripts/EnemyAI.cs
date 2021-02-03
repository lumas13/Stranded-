using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyAI : MonoBehaviour
{
    public GameObject Player;

    Animator Animator;
    NavMeshAgent NavMesh;

    // Start is called before the first frame update
    void Start()
    {
        NavMesh = GetComponent<NavMeshAgent>();
        Animator = GetComponent<Animator>();
        Player = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        NavMesh.SetDestination(Player.transform.position);

        Debug.Log(NavMesh.velocity.magnitude);
 
        if (NavMesh.velocity.magnitude > 0)
        {
            Animator.SetBool("isRunning", true);
        }
        else
        {
            Animator.SetBool("isRunning", false);
        }
    }
}
