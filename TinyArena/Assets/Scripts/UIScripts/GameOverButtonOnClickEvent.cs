using UnityEngine;

public class GameOverButtonOnClickEvent : MonoBehaviour
{
    public void RestartGame()
    {
        GameManager.Instance.ChangeState(GameState.Playing);
    }
}
