using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;

public class PlayerGrenade : MonoBehaviour
{
    public GameObject bomb;
    public Transform bombSpawn;
    public float bombMass = 30;
    public float throwForce = 30;
    public float throwDelay = 0.1f;
    bool isWaiting = false;
    //public UnityEvent OnShoot;
    
    public BombTrajectoryLine bombTrack;

    void Update()
    {
        bombTrack.ShowLine(bombSpawn.position, bombSpawn.forward*throwForce/bombMass);
        if (Input.GetButtonUp("Fire1") && isWaiting == false)
        {
            GameObject waterBomb = Instantiate(bomb);
            waterBomb.transform.position = bombSpawn.position;
            //OnShoot?.Invoke();
            Rigidbody rb = waterBomb.GetComponent<Rigidbody>();
            rb.mass = bombMass;
            rb.AddForce(bombSpawn.forward * throwForce, ForceMode.Impulse);
            isWaiting = true;
            StartCoroutine(DelayThrowing());
        }    
    }
    IEnumerator DelayThrowing()
    {
        yield return new WaitForSeconds(throwDelay);
        isWaiting = false;
    }
}
