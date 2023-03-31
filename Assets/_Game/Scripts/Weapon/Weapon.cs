using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    [SerializeField] private Rigidbody _rb;
    private Vector3 destination;
    private Vector3 start;
    private Character _char;
    private void Start()
    {
        //_rb.velocity = (destination - start) * Variable.WEAPONDEFAULTSPEED;
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == Variable.TARGET)
        {
            gameObject.SetActive(false);
            other.gameObject.SetActive(false);
        }
    }
    public void SetDestination(Vector3 des)
    {
        destination = des;
    }
    public void SetStart(Vector3 thisStart)
    {
        start = thisStart;
    }
}
