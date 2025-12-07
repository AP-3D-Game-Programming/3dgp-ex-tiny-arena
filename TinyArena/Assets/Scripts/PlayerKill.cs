using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerKill : MonoBehaviour
{
    [SerializeField] Image gameOverText;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            gameOverText.enabled = true;
            Time.timeScale = 0;
        }
        else if (other.gameObject.CompareTag("Enemy"))
        {
            Destroy(other.gameObject);
        }
    }
}
