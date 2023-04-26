using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IdleState : IState
{

    float timer;
    float randomTime;
    int ranAtk;
    public void OnEnter(Bot bot)
    {
        bot.StopMoving();
        timer = 0;
        randomTime = Random.Range(2.5f, 4f);
        ranAtk = Random.Range(1, 5);
    }

    public void OnExecute(Bot bot)
    {
        if (bot.isDead)
        {
            bot.ChangeState(new DeadState());
        }
        else
        {
            timer += Time.deltaTime;
            if (timer > randomTime)
            {
                bot.ChangeState(new PatrolState());
            }
            else
            {
                if (bot.listTargets.Count > 0)
                {
                    if (ranAtk > 2)
                    {
                        bot.ChangeState(new AttackState());
                    }
                }
            }
        }

    }

    public void OnExit(Bot bot)
    {

    }
}
