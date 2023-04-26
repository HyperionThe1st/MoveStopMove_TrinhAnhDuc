using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class SpawnBot : GameUnit
{
    //Object
    public static SpawnBot SpawnerBotInstance;
    [SerializeField] public PlayerMovement _player;
    [SerializeField] public Bot botPrefabs;
    [SerializeField] public List<Bot> listBotToPool;

    //Amount
    private int totalAmount;
    //public const float SPAWNDISTANCE = 10.0f;
    //public const int MAXBOTONSCREEN = 10;

    //Position
    private float posX;
    private float posZ;
    private int radius = 100;
    private void Awake()
    {
        SpawnerBotInstance = this;
        listBotToPool = new List<Bot>();
        totalAmount = Variable.MAXBOT;
    }
    private void Update()
    {

    }

    private void Start()
    {

        _player.OnInit();
        OnInit();
    }

    private void OnInit()
    {
        LevelManager.instance.characterList.Add(_player);
        Vector3 newPos = GetRandomPosition(transform.position, radius, -1);
        for (int i = 0; i < totalAmount; i++)
        {
            Bot bot = SimplePool.Spawn<Bot>(botPrefabs, newPos, Quaternion.identity);
            bot.gameObject.SetActive(false);
            listBotToPool.Add(bot);
        }

        for (int i = 0; i < Variable.MAXBOTONSCREEN; i++)
        {
            BotSpawn();
        }
    }


    public Bot GetBotFromPool()
    {
        Vector3 newPos = GetRandomPosition(transform.position, radius, -1);
        for (int i = 0; i < listBotToPool.Count; i++)
        {
            if (!listBotToPool[i].gameObject.activeInHierarchy)
            {
                return listBotToPool[i];
            }
        }
        Bot bot = SimplePool.Spawn<Bot>(PoolType.Bot, newPos, Quaternion.identity);
        bot.gameObject.SetActive(false);
        //bot.SetIsDead(false);
        listBotToPool.Add(bot);
        return bot;
    }

    public void BotSpawn()
    {
        Bot bot = GetBotFromPool();
        bot.OnInit();
        if (CheckRandomPos(bot))
        {
            bot.gameObject.SetActive(true);

            LevelManager.instance.characterList.Add(bot);
        }
    }
    public IEnumerator DoSpawnBot()
    {
        yield return new WaitForSeconds(2f);
        BotSpawn();
    }

    public static Vector3 GetRandomPosition(Vector3 origin, float dist, int layermask)
    {
        Vector3 randomDir = Random.insideUnitSphere * dist;
        randomDir += origin;
        NavMeshHit navHit;
        NavMesh.SamplePosition(randomDir, out navHit, dist, layermask);
        return navHit.position;
    }
    public bool CheckRandomPos(Character character)
    {
        bool validPos = false;
        while (!validPos)
        {
            character.transform.position = GetRandomPosition(character.transform.position, radius, -1);
            //character.transform.position = new Vector3(Random.Range(-pointX, pointX), 0, Random.Range(-pointZ, pointZ));
            validPos = true;
            foreach (Character otherCharacter in LevelManager.instance.characterList)
            {
                if (Vector3.Distance(character.transform.position, otherCharacter.transform.position) < Variable.SPAWNDISTANCE)
                {
                    validPos = false;
                    break;
                }
            }
        }
        return validPos;
    }

}
