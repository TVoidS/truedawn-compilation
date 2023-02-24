using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;

public class NewGameButton : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        gameObject.GetComponent<Button>().onClick.AddListener(delegate() {
            // Button Pressed event!
            // TODO:
            // Swap Scenes
            // Send "Don't Load" signal.

            PlayerStats.SetLoadFile("");
            StartCoroutine(AsyncLoad());
        });
    }

    IEnumerator AsyncLoad()
    {
        GameObject.Find("Canvas").GetComponent<CanvasContentsController>().SwapTo(2);

        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync("SoulCore");
        while (!asyncLoad.isDone) 
        {
            yield return null;
        }
    }
}
