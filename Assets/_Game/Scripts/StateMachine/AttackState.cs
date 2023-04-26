using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackState : IState
{
    float timer;
    float changeTime;
    public void OnEnter(Bot bot)
    {
        bot.StopMoving();
        //bot.RotateToEnemy();
        bot.StartCoroutine(bot.DoAttack());
        timer = 0;
        changeTime = Random.Range(2, 5);
    }

    public void OnExecute(Bot bot)
    {
        if (bot.isDead)
        {
            bot.ChangeState(new DeadState());
        }

        timer += Time.deltaTime;
        if (timer >= changeTime)
        {
            bot.ChangeState(new PatrolState());
        }

    }

    public void OnExit(Bot bot)
    {
    }
}
