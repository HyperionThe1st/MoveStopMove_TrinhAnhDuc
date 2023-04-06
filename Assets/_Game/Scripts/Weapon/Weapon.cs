using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : GameUnit
{
    [SerializeField] private Rigidbody _rb;
    private Character _character;
    private Vector3 attackPos;
    private float atkRange;

    public void WeaponInit(Character _char, Vector3 targetPostion)
    {
        this._character = _char;
        attackPos = _character.attackBox.transform.position;
        TF.forward = (targetPostion - TF.position).normalized;
    }


    private void Update()
    {
        if (Vector3.Distance(TF.position, attackPos) < _character.attackRange)
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
        if (other.tag == Variable.TARGET)
        {
            OnDespawn();
            //Xoa khoi List
            _character.listTargets.Remove(other.gameObject);
            //Chuyen Anim
            //Despawn
            Destroy(other.gameObject);
        }
    }
}
