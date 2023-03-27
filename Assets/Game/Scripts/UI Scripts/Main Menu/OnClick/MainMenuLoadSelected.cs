using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenuLoadSelected : MonoBehaviour
{
    public CanvasContentsController UISwapper;
    public DetailedSaveDisplayDistributor DetailedSaveDisplay;

    private void Awake()
    {
        gameObject.GetComponent<Button>().onClick.AddListener(() => {
            StartCoroutine(AsyncLoad());
        });
    }

    IEnumerator AsyncLoad()
    {
        // TODO: Make this actually send the save data to the SaveLoad object properly.
        // I may have to modify the OnStart methods of the SoulCore scene to get this to work.

        SaveData save = DetailedSaveDisplay.SaveData;
        UISwapper.SwapTo(2);

        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync("SoulCore");
        while (!asyncLoad.isDone)
        {
            yield return null;
        }
        SaveLoad.LoadSave(save.Path);
    }
}
