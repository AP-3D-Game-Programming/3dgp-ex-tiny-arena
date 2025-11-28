using UnityEngine;

/// <summary>
/// This is a temporary demonstration class, not meant for production.
/// After implementing GameManager into main scene, you can remove this class (as the game itself will handle changing scenes if and when needed).
/// </summary>
public class ChangeScene : MonoBehaviour
{
    private float timeRemaining = 5.0f;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (timeRemaining > 0)
        {
            timeRemaining -= Time.deltaTime;
            Debug.Log($"Time left: {timeRemaining}s");
        }
        else
        {
            if (GameManager.Instance.CurrentState == GameState.Playing)
            {
                GameManager.Instance.ChangeState(GameState.GameOver);
            }
            else
            {
                GameManager.Instance.ChangeState(GameState.Playing);
                timeRemaining = 5.0f;
            }
        }
    }
}
