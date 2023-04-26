using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : GameUnit
{
    [SerializeField] private Rigidbody _rb;
    private Character _character;
    private Vector3 attackPos;


    public virtual void WeaponInit(Character _char, Vector3 targetPostion)
    {
        this._character = _char;
        attackPos = _character.attackBox.transform.position;
        TF.forward = (targetPostion - TF.position).normalized;
    }


    private void Update()
    {
        if (Vector3.Distance(TF.position, attackPos) < (_character.attackRange + 1.0f))
        {
            TF.forward = new Vector3(TF.forward.x, 0, TF.forward.z);
            TF.Translate(TF.forward * Variable.WEAPONDEFAULTSPEED * Time.deltaTime, Space.World);
        }
        else
        {
            OnDespawn();
        }
    }
    public void OnDespawn()
    {
        SimplePool.Despawn(this);
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(Variable.TARGET) && other.GetComponent<Character>() != _character)
        {
            _character.score += 1;
            _character.ChangeSize();
            _character.listTargets.Remove(other.gameObject.GetComponent<GameUnit>());
            OnDespawn();
            Bot triggeredBot = other.gameObject.GetComponent<Bot>();
            if (triggeredBot != null)
            {
                //Debug.Log("DeadState");
                //triggeredBot.ChangeState(new DeadState());
                triggeredBot.isDead = true;
            }
            else
            {
                other.gameObject.GetComponent<PlayerMovement>().OnHit();
            }



        }
    }

    public virtual void OnMoving()
    {

    }
}
