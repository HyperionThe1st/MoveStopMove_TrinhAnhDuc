using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatrolState : IState
{
    float timer;
    float randomTime;
    int changeState;
    int attack;
    public void OnEnter(Bot bot)
    {
        bot.pointToMove = bot.GetRandomPoint();
        timer = 0;
        randomTime = Random.Range(3f, 6f);
        changeState = Random.Range(1, 4);
        attack = Random.Range(0, 10);
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
            if (bot.listTargets.Count != 0)
            {
                switch (changeState)
                {
                    case 1:
                        bot.ChangeState(new AttackState());
                        break;
                    case 2:
                        bot.ChangeState(new IdleState());
                        break;
                    case 3:
                        bot.ChangeState(new PatrolState());
                        break;
                }
            }
            else
            {
                if (timer < randomTime)
                {
                    if (bot.transform.position.Equals(bot.pointToMove))
                    {
                        timer = 0;
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
    }

    public void OnExit(Bot bot)
    {

    }
}
