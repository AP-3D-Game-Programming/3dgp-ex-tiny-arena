using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float speed = 40f;
    public float lifetime = 3f;


    void Start()
    {

        // Bullet vernietigt zichzelf na lifetime seconden
        Destroy(gameObject, lifetime);
    }

    void Update()
    {
        // De bullet beweegt elke frame naar voren
        transform.Translate(Vector3.forward * speed * Time.deltaTime);
    }
}
