using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelControllerTemp : MonoBehaviour
{
    private float spawnDelay = 10;
    private float waitingTime = 0;

    [SerializeField] GameObject enemy;

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    // Update is called once per frame
    void Update()
    {
        if (waitingTime + Time.deltaTime > spawnDelay)
        {
            InstantiateInAdditiveScene("GameLevelScene");
            waitingTime = 0;
            if (spawnDelay > 2f)
                spawnDelay -= 0.5f;
        } 
        else 
            waitingTime += Time.deltaTime;
    }

    private void InstantiateInAdditiveScene(string sceneName)
    {
        Scene sceneToLoad = SceneManager.GetSceneByName(sceneName);
        GameObject instantiatedEnemy = Instantiate(enemy, new Vector3(-10, 1, 20), Quaternion.identity);

        if (sceneToLoad.IsValid())
        {
            SceneManager.MoveGameObjectToScene(instantiatedEnemy, sceneToLoad);
        }
    }
}
