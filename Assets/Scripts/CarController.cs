using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CarController : MonoBehaviour
{
    public float MoveSpeed = 50;
    public float MaxSpeed = 15;
    public float Drag = 0.98f;
    public float SteerAngle = 20;
    public float Traction = 1;

    public float hp;
    public float maxHp = 100f;
    
    [SerializeField] Slider hpBar;
    [SerializeField] TMP_Text speedGUI;
    [SerializeField] private TMP_Text healthText;
    [SerializeField] private TMP_Text scoreText;
    public int score;
    private Vector3 MoveForce;
    public Rigidbody rb;

    void Start()
    {
        rb = this.GetComponent<Rigidbody>();
        hp = maxHp;
        healthText.text = hp.ToString() + "/" + maxHp;
        Fuel.OnCollected += IncreaseFuel;
    }

    private void IncreaseFuel(int amount)
    {
        hp += amount;
        healthText.text = hp.ToString() + "/" + "100";
    }

    private void FixedUpdate()
    {
        // Move
        MoveForce += transform.forward * MoveSpeed * Input.GetAxis("Vertical") * Time.fixedDeltaTime;
        rb.velocity = MoveForce * 50 * Time.fixedDeltaTime;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Enemy")
        {
            MoveSpeed *= 0.5f;
            rb.velocity *= 0.85f;
            collision.gameObject.GetComponent<ZombieController>().ReplaceWithRagdoll();
            if (rb.velocity.magnitude > 20)
            {
                switch (collision.gameObject.GetComponent<ZombieController>().currentZombieType)
                {
                    case ZombieController.zombieType.basic:
                        hp -= 10;
                        break;
                    case ZombieController.zombieType.running:
                        hp -= 5;
                        break;
                    case ZombieController.zombieType.giant:
                        hp -= 50;
                        break;
                    case ZombieController.zombieType.exploding:
                        hp -= 25;
                        gameObject.GetComponent<Rigidbody>().AddForce(Vector3.back, ForceMode.Impulse);
                        break;

                }
            }
        }
    }

    void Update()
    {
        speedGUI.text = ((int)rb.velocity.magnitude).ToString();
        hpBar.value = hp;
        hpBar.maxValue = maxHp;
        scoreText.text = score.ToString();
        healthText.text = hp.ToString() + "/" + maxHp;

        if (MoveSpeed<30)
            MoveSpeed += Time.deltaTime*5f;
        // Steer
        float steerInput = Input.GetAxis("Horizontal");
        transform.Rotate(Vector3.up * steerInput * MoveForce.magnitude * SteerAngle * Time.deltaTime);

        // Drag
        MoveForce *= Drag;

        // Max speed limit
        MoveForce = Vector3.ClampMagnitude(MoveForce, MaxSpeed);

        // Traction
        MoveForce = Vector3.Lerp(MoveForce.normalized, transform.forward, Traction * Time.deltaTime) * MoveForce.magnitude;

    }
}
