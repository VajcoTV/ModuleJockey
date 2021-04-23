using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Knife : MonoBehaviour
{
    private void Start()
    {
        StartCoroutine("Destroyer");
    }

    IEnumerator Destroyer()
    {
        
        yield return new WaitForSeconds(0.5f);
        Destroy(this.gameObject);
        
    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "ZelenyDuch")
        {
            Destroy(this.gameObject);
        }
    }

}
