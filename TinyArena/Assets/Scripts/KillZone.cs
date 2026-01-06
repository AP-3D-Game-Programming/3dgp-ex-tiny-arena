using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DeathPlane : MonoBehaviour
{
<<<<<<<< HEAD:TinyArena/Assets/Scripts/DeathPlane.cs
    [SerializeField] Image gameOverText;
    GameObject player;
    PlayerHealth playerHealth;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        playerHealth = player.GetComponent<PlayerHealth>();
    }

    // Update is called once per frame
    void Update()
    {
        
========
    private PlayerHealth health;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        health = GameObject.FindWithTag("Player").GetComponent<PlayerHealth>();
>>>>>>>> waves:TinyArena/Assets/Scripts/KillZone.cs
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject == player)
        {
<<<<<<<< HEAD:TinyArena/Assets/Scripts/DeathPlane.cs
            playerHealth.TakeDamage(10000f);
========
            health.TakeDamage(100000f);
>>>>>>>> waves:TinyArena/Assets/Scripts/KillZone.cs
        }
        else if (other.gameObject.CompareTag("Enemy"))
        {
            Destroy(other.gameObject);
        }
    }
}
