using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatrolState : IState
{
    float timer;
    float randomTime;
    public void OnEnter(Bot bot)
    {
        timer = 0;
        randomTime = Random.Range(3f, 6f);
    }

    public void OnExecute(Bot bot)
    {
        timer += Time.deltaTime;

        if (bot.listTargets.Count != 0)
        {
            bot.ChangeState(new AttackState());
        }
        else
        {
            if (timer < randomTime)
            {
                bot.Move();
            }
            else
            {
                bot.ChangeState(new IdleState());
            }
        }

    }

    public void OnExit(Bot bot)
    {

    }
}
