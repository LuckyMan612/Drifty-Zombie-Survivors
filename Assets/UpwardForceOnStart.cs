using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpwardForceOnStart : MonoBehaviour
{
    [SerializeField] Rigidbody rb;
    // Start is called before the first frame update
    void Start()
    {
        rb.velocity=rb.velocity+new Vector3(0, 50, 0);

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
