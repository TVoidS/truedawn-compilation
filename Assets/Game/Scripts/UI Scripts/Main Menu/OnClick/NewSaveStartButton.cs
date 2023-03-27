using System.Collections;
using System.IO;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class NewSaveStartButton : MonoBehaviour
{
    public TMP_InputField nameField;
    public CanvasContentsController CanvasContentsController;

    // Start is called before the first frame update
    void Start()
    {
        gameObject.GetComponent<Button>().onClick.AddListener(() => {
            // Make sure it isn't empty         Make sure it doesn't have invalid file chars                make sure it doesn't have a .    Add more if needed.
            if (nameField.text != "" && !(nameField.text.IndexOfAny(Path.GetInvalidFileNameChars()) >= 0 || nameField.text.Contains(".")))
            {
                // Doesn't contain anything bad, go ahead.
                // Button Pressed event!
                // TODO:
                // Send "Don't Load" signal.
                if (PlayerStats.SetNewName(nameField.text))
                {
                    StartCoroutine(AsyncLoad());
                }
            }
            else 
            {
                // Invalid input.
                // TODO: turn red.
            }
        });
    }

    IEnumerator AsyncLoad()
    {
        CanvasContentsController.SwapTo(2);

        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync("SoulCore");
        while (!asyncLoad.isDone)
        {
            yield return null;
        }
    }
}
