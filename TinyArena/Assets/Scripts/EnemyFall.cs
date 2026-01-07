using UnityEngine;
using UnityEngine.AI;

public class EnemyFall : MonoBehaviour
{
    private NavMeshAgent agent;
    private float falltime = 0;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (!agent.isOnOffMeshLink && !Grounded())
        {
            agent.enabled = false;
        }
        if (agent.enabled == false)
        {
            falltime += Time.deltaTime;
            if (falltime < 0.5 && Grounded())
                agent.enabled = true;
        }
    }

    private bool Grounded()
    {
        return Physics.Raycast(transform.position, -Vector3.up, 2f);
    }
}
