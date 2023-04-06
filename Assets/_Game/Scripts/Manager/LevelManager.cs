using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    public List<Character> characterList;
    public PlayerMovement player;
    public static LevelManager instance;
    public int alive;
    private void Awake()
    {
        instance = this;

    }
    // Start is called before the first frame update
    void Start()
    {
        player.OnInit();
    }
    public void RemoveTarget(Character character)
    {
        for (int i = 0; i < characterList.Count; i++)
        {
            if (characterList[i].listTargets.Contains(character.gameObject))
            {
                characterList[i].listTargets.Remove(character.gameObject);
            }
        }
    }
}
