using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeadState : IState
{

    float timer;
    float time;

    public void OnEnter(Bot bot)
    {
        time = 0.1f;
        timer = 3.1f;
        bot.OnHit();
    }

    public void OnExecute(Bot bot)
    {
        time += Time.deltaTime;
        if (time > timer)
        {

            bot.SetIsDead(false);

            SimplePool.Despawn(bot);
            LevelManager.instance.alive--;
            if (LevelManager.instance.alive > Variable.MAXBOTONSCREEN)
            {
                SpawnBot.SpawnerBotInstance.StartCoroutine(SpawnBot.SpawnerBotInstance.DoSpawnBot());
            }
            // SpawnBot.SpawnerBotInstance.DespawnNameBot(bot); Xoa ten cua bot
            SpawnBot.SpawnerBotInstance.listBotToPool.Remove(bot);
            time = 0;
        }
    }

    public void OnExit(Bot bot)
    {
        
    }
}
