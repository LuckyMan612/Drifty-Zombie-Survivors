using UnityEngine;

public class DestroyAfterTimer : MonoBehaviour
{
    public float destroyTimer = 3.0f; // Set the time in seconds after which the object will be destroyed

    void Start()
    {
        // Call the DestroyObject method after 'destroyTimer' seconds
        Invoke("DestroyObject", destroyTimer);
    }

    void DestroyObject()
    {
        // Destroy the GameObject this script is attached to
        Destroy(gameObject);
    }
}