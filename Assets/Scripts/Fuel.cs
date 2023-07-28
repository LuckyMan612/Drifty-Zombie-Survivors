using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fuel : MonoBehaviour
{
    public static event Action<int> OnCollected;

    private float timeToReSpawn = 15F;

    void Update()
    {
        transform.localRotation = Quaternion.Euler(0, Time.time * 100f, 0);
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            OnCollected?.Invoke(25);
            gameObject.SetActive(false);
            Invoke("renableFuel", timeToReSpawn);
        }
    }

    private void renableFuel()
    {
        gameObject.SetActive(true);
    }
}
