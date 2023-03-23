using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    [SerializeField] private GameObject _weaponPrefabs;
    [SerializeField] private Rigidbody _weaponRigid;
    [SerializeField] private GameObject attackBox;

    private void Start()
    {
        OnInit();
    }
    private void OnInit()
    {

    }
    public void onDespawn()
    {
        Destroy(gameObject);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == Variable.TARGET)
        {
            
            collision.GetComponent<Character>().OnHit();
            Instantiate(_weaponPrefabs, attackBox.transform.position, transform.rotation);
            onDespawn();
        }
    }
}
