using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneMessenger : MonoBehaviour
{

    private Scene scene;
    private SaveData save;

    private void Awake()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
        SceneManager.sceneLoaded += OnSceneLoaded;
        
        DontDestroyOnLoad(gameObject);

        scene = SceneManager.GetActiveScene();
    }

    private void OnDestroy()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    private void OnSceneLoaded(Scene newScene, LoadSceneMode mode) 
    {
        // Identify which scene is being loaded and handle it appropriately.

        if (string.Equals(scene.name, newScene.name)) 
        {
            // Current Scene, Break everything
        }
        else 
        {
            switch (newScene.name)
            {
                case "SoulCore": SkillController.Startup();  break;
            }
        }

    }

    public void SetSaveData(SaveData saveData) 
    {
        save = saveData;
    }
}
