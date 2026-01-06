using TMPro;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class WaveNumber : MonoBehaviour
{
    private GameObject player;
    private TextMeshPro text;
    private int waveNumber = 0;
    [SerializeField] WaveManager waveManager;
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        text = GetComponent<TextMeshPro>();
    }

    // Update is called once per frame
    void Update()
    {
        if (waveNumber != waveManager.waveNumber) { 
            waveNumber = waveManager.waveNumber;
            text.text = waveNumber.ToString();
        }
        Vector3 direction = transform.position - player.transform.position;
        transform.rotation = Quaternion.LookRotation(direction);
    }
}
