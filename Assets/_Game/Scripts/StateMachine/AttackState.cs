using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackState : IState
{
    float timer;
    public void OnEnter(Bot bot)
    {
        bot.StopMoving();
        bot.RotateToEnemy();
        bot.StartCoroutine(bot.DoAttack());
        timer = 0;
    }

    public void OnExecute(Bot bot)
    {
        timer += Time.deltaTime;
        if (timer >= 1.5f)
        {
            bot.ChangeState(new PatrolState());
        }
    }

    public void OnExit(Bot bot)
    {
    }
}
