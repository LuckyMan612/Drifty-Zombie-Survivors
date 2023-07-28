using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.InteropServices.ComTypes;
using TMPro;
using UnityEngine;
using UnityEngine.AI;

public class ZombieController : MonoBehaviour
{
    [SerializeField] NavMeshAgent agent;
    [SerializeField] GameObject ragdoll;

    public AudioClip[] sounds;
    private AudioSource audioSource;

    public float minInterval = 5f;
    public float maxInterval = 10f;

    private float nextSoundTime;
    private CarController car;

    private float maximumDistanceThreshold = 300f;
    
    
    public enum zombieType
    {
        basic,
        running,
        giant,
        exploding,
    }

    public zombieType currentZombieType;
    
    
    public void ReplaceWithRagdoll()
    {
        var o = Instantiate(ragdoll, transform.position, transform.rotation);
        switch (currentZombieType)
        {
            case zombieType.basic:
                GameObject.FindObjectOfType<LevelUpSystem>().currentXp += 1;
                GameObject.FindObjectOfType<CarController>().score += 100;
                break;
            case zombieType.running:
                GameObject.FindObjectOfType<LevelUpSystem>().currentXp += 2;
                GameObject.FindObjectOfType<CarController>().score += 50;
                break;
            case zombieType.exploding:
                GameObject.FindObjectOfType<LevelUpSystem>().currentXp += 5;
                GameObject.FindObjectOfType<CarController>().score += 250;
                break;
            case zombieType.giant:
                GameObject.FindObjectOfType<LevelUpSystem>().currentXp += 10;
                GameObject.FindObjectOfType<CarController>().score += 500;
                break;



        }
        o.GetComponentInChildren<Rigidbody>().velocity = gameObject.GetComponent<Rigidbody>().velocity;
        Destroy(gameObject);
    }

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        SetNextSoundTime();
        car = GameObject.FindObjectOfType<CarController>();
    }

    void Update()
    {
        float distanceFromZombieToPlayer = Vector3.Distance(transform.position, car.transform.position);
        
        if (distanceFromZombieToPlayer > maximumDistanceThreshold)
        {
            gameObject.SetActive(false);
        }
        else
        {
            gameObject.SetActive(true);
        }
        
        
        agent.SetDestination(car.transform.position);

        if (Time.time >= nextSoundTime)
        {
            PlayRandomSound();
            SetNextSoundTime();
        }
    }

    private void SetNextSoundTime()
    {
        nextSoundTime = Time.time + UnityEngine.Random.Range(minInterval, maxInterval);
    }

    private void PlayRandomSound()
    {
        if (sounds.Length > 0)
        {
            int randomIndex = UnityEngine.Random.Range(0, sounds.Length);
            AudioClip randomSound = sounds[randomIndex];

            audioSource.PlayOneShot(randomSound);
        }
    }
}
