using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackRange : MonoBehaviour
{
    public Character _char;
    private void OnTriggerEnter(Collider other)
    {
        switch (other.tag)
        {
            case Variable.TARGET:
                {
                    if (other.gameObject == null)
                    {
                        break;
                    }
                    _char.listTargets.Add(other.gameObject.GetComponent<GameUnit>());
                    break;
                }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        switch (other.tag)
        {
            case Variable.TARGET:
                {
                    _char.listTargets.Remove(other.gameObject.GetComponent<GameUnit>());
                    break;
                }
        }
    }
}
