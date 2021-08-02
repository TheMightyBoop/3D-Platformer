using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HurtPlayer : MonoBehaviour
{
    public int damageToGive = 1;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Player")
        {
            Vector3 hitDir = other.transform.position - transform.position;
            hitDir = hitDir.normalized;

            FindObjectOfType<HealthManager>().HurtPlayer(damageToGive, hitDir);
        }
    }
}
