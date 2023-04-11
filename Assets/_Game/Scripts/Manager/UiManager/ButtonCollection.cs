using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonCollection : MonoBehaviour
{
    public void ChangeScreen(GameObject _screen)
    {
        UIManager.UIInstance.ChangeUIState(_screen);
    }
}
