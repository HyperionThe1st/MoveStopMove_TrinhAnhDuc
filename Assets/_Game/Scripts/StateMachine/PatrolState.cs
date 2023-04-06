using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatrolState : IState
{
    float timer;
    float randomTime;
    public void OnEnter(Bot bot)
    {
        bot.pointToMove = bot.GetRandomPoint();
        timer = 0;
        randomTime = Random.Range(3f, 6f);
    }

    public void OnExecute(Bot bot)
    {
        timer += Time.deltaTime;

        if (bot.listTargets.Count != 0)
        {
            int ran = Random.Range(0, 10);
            if (ran > 8)
            {
                bot.ChangeState(new AttackState());
            }
            else
            {
                bot.Move();
            }     
        }
        else
        {
            if (timer < randomTime)
            {
                if (bot.transform.position.Equals(bot.pointToMove))
                {
                    bot.ChangeState(new IdleState());
                }
                else
                {
                    bot.Move();
                }
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
