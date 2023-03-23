using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{
    private string currentAnimName;
    [SerializeField] private Animator _animator;


    protected void changeAnim(string animName)
    {
        if (currentAnimName != animName)
        {
            _animator.ResetTrigger(animName);
            currentAnimName = animName;
            _animator.SetTrigger(currentAnimName);
        }
    }
    public void OnHit()
    {
        Debug.Log("Hit");
    }

}
