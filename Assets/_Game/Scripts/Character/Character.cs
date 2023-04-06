using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : GameUnit
{

    //Di Chuyen
    private string currentAnimName;
    [SerializeField] private Animator _animator;
    [SerializeField] public Transform meshPlayer;
    private bool isRunning;
    private bool isDead;
    public List<GameObject> listTargets;
    [SerializeField] public GameObject attackBox;

    //Vu khi
    public float attackRange = 5f;
    [SerializeField] public Weapon _wp;
    private void Start()
    {

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

    public virtual void OnInit()
    {
        isDead = false;
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

    public virtual void Attack()
    {
        GameObject closestEnemy = GetClosetObject();
        if (closestEnemy != null)
        {
            RotateToEnemy();
            ChangeAnim(Variable.ATTACK);
            ShowUnderline(closestEnemy);
        }

    }

    public virtual void SpawnWeapon()
    {
        Vector3 target = GetClosetObject().transform.position;
        Weapon weapon = SimplePool.Spawn<Weapon>(_wp, attackBox.transform.position, Quaternion.identity);
        weapon?.WeaponInit(this, target);
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
                if (go.activeSelf == false)
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
