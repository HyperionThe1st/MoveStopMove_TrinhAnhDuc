using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{
    private string currentAnimName;
    [SerializeField] private Animator _animator;
    [SerializeField] public Transform meshPlayer;
    private bool isRunning;
    private bool isDead;
    public List<GameObject> listTargets;

    private void Start()
    {
        isDead = false;
    }
    protected void ChangeAnim(string animName)
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
        ChangeAnim(Variable.DIE);
    }

    public void SetRunning(bool isRun)
    {
        isRunning = isRun;
    }

    public bool GetRunning()
    {
        return isRunning;
    }

    [System.Obsolete]
    public GameObject GetClosetObject()
    {
        CheckExist();
        List<float> distance = new List<float>();
        if (listTargets.Count == 0) return null;
        else
        {
            foreach (GameObject go in listTargets)
            {
                float temp = Vector3.Distance(go.transform.position, transform.position);
                distance.Add(temp);
            }
            float minDistance = distance[0];
            int index = 0;
            if (listTargets.Count == 1)
            {
                return listTargets[0];
            }
            else
            {
                for (int i = 1; i < distance.Count; i++)
                {
                    if (distance[i] < minDistance)
                    {
                        minDistance = distance[i];
                        index = i;
                    }
                }
                return listTargets[index];
            }
        }

    }

    public void AttackClosestEnemy()
    {
        //ChangeAnim(Variable.ATTACK);
        Invoke(nameof(Attack), Variable.ATTACKDELAY);
    }

    public void Attack()
    {
        GameObject closestEnemy = GetClosetObject();
        if (closestEnemy != null)
        {
            CreateWeapon(closestEnemy);
            ShowUnderline(closestEnemy);
        }

    }
    public void CreateWeapon(GameObject _closestEnemy)
    {
        GameObject instanceWeapon = ObjectPool.PoolInstance.GetPooledObject();
        if (instanceWeapon != null)
        {
            Vector3 attackPos = transform.GetChild(2).transform.position;
            Vector3 destinationPos = _closestEnemy.transform.position;
            instanceWeapon.transform.position = Vector3.MoveTowards(attackPos, destinationPos, Variable.WEAPONDEFAULTSPEED * Time.deltaTime);
            instanceWeapon.transform.rotation = Quaternion.LookRotation(destinationPos - attackPos);
            instanceWeapon.SetActive(true);
            Rigidbody weaponRigid = instanceWeapon.GetComponent<Rigidbody>();
            if (weaponRigid != null)
            {
                weaponRigid.velocity = (destinationPos - attackPos) * Variable.WEAPONDEFAULTSPEED;
            }
            StartCoroutine(DeactivateProjectile(instanceWeapon));
        }
    }
    public IEnumerator DeactivateProjectile(GameObject gameObject)
    {
        yield return new WaitForSeconds(Variable.WEAPONLIFETIME);
        gameObject.SetActive(false);
    }

    public void ShowUnderline(GameObject _go)
    {
        int bottomMost = _go.transform.childCount - 1;
        _go.transform.GetChild(bottomMost).gameObject.SetActive(true);
    }

    [System.Obsolete]
    public void CheckExist()
    {
        if (listTargets.Count.Equals(0))
        {
            return;
        }
        else
        {
            foreach (GameObject go in listTargets)
            {
                if (go.active == false)
                {
                    listTargets.Remove(go);
                }
            }
        }

    }
    public void RotateToEnemy()
    {
        GameObject target = GetClosetObject();
        if (target != null)
        {
            Vector3 targetPosition = target.transform.position;
            Vector3 lookDirection = targetPosition - transform.position;

            meshPlayer.rotation = Quaternion.LookRotation(lookDirection);
        }
    }
}
