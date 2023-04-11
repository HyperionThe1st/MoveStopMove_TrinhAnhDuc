using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
public class Bot : Character
{
    //Moving
    [SerializeField] public NavMeshAgent agent;
    private Bounds flrBounds;
    public GameObject floor;
    public Vector3 pointToMove;

    //Weapon


    private void Start()
    {
        flrBounds = floor.GetComponent<Renderer>().bounds;
        ChangeState(new IdleState());
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

    public Vector3 GetRandomPoint()
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
    public override void Attack()
    {
        base.Attack();
        SpawnWeapon();
    }

    public IEnumerator DoAttack()
    {
        Attack();
        float time = 0;
        float timer = 1.1f;
        while (time < timer)
        {
            time += Time.deltaTime;

            yield return null;
        }
        int numRand = Random.Range(0, 100);
        if (numRand > 50)
        {
            ChangeState(new IdleState());
        }
        else
        {
            ChangeState(new PatrolState());
        }
        yield return null;
    }


    public void OnDespawn()
    {
        SimplePool.Despawn(this);
    }
    public override void OnHit()
    {
        base.OnHit();
        StopMoving();
        SetObjectActive(false);
        LevelManager.instance.RemoveTarget(this);
    }
}

