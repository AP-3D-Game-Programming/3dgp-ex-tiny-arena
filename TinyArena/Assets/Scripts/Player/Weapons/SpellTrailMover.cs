using UnityEngine;

public class SpellTrailMover : MonoBehaviour
{
    public float speed = 60f;

    void Update()
    {
        transform.position += transform.forward * speed * Time.deltaTime;
    }
}