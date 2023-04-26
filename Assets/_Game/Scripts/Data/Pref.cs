using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pref : MonoBehaviour
{
    public static int CurSkinId
    {
        set => PlayerPrefs.SetInt(Variable.SKIN_ID, value);
        get => PlayerPrefs.GetInt(Variable.SKIN_ID);
    }
    public static int Cost
    {
        set => PlayerPrefs.SetInt(Variable.COST, value);
        get => PlayerPrefs.GetInt(Variable.COST);

    }
    public static void SetBool(string key, bool isOn)
    {
        if (isOn)
        {
            PlayerPrefs.SetInt(key, 1);
        }
        else
        {
            PlayerPrefs.SetInt(key, 0);
        }
    }
    public static bool GetBool(string key)
    {
        /*        if(PlayerPrefs.GetInt(key) == 1)
                {
                    return true;
                }
                else
                {
                    return false;
                }*/
        return PlayerPrefs.GetInt(key) == 1 ? true : false;
    }
}
