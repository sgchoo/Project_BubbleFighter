using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyItem_Choo : MonoBehaviour
{
    void OnTriggerEnter(Collider other)
    {
        if (other.transform.tag == "Player")
        {
            Destroy(this.gameObject);
        }
    }
}
