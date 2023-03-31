using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
public class Bot : Character
{
    NavMeshAgent agent;
    private Bounds flrBounds;
    public GameObject floor;
    Vector3 pointToMove;
    private void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        flrBounds = floor.GetComponent<Renderer>().bounds;
    }

    private void Update()
    {
        if (currentState != null
            //&& !isDead
            )
        {
            currentState.OnExecute(this);
        }
    }

    private Vector3 GetRandomPoint()
    {
        float rx = Random.Range(flrBounds.min.x, flrBounds.max.x);
        float rz = Random.Range(flrBounds.min.z, flrBounds.max.z);
        Vector3 destination = new Vector3(rx, transform.position.y, rz);
        return destination;
    }
    public void Move()
    {
        ChangeAnim(Variable.RUN);
        agent.SetDestination(pointToMove);
    }

    public void StopMoving()
    {
        ChangeAnim(Variable.IDLE);
        agent.SetDestination(transform.position);
    }
    private IState currentState;
    public void ChangeState(IState newstate)
    {
        if (currentState != null)
        {
            currentState.OnExit(this);
        }
        currentState = newstate;
        if (currentState != null)
        {
            currentState.OnEnter(this);
        }
    }
}

