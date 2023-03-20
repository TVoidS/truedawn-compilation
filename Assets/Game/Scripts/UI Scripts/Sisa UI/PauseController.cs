using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ONLY HAVE ONE OF THESE. (may not be necessary to only have one.)
/// This system controlls whether the game is paused or unpaused.
/// </summary>
public class PauseController : MonoBehaviour
{
    private static List<GameObject> PauseUI = new();
    private static List<GameObject> UnpauseUI = new();

    private void Start()
    {
        if (PauseUI.Count == 0 && UnpauseUI.Count == 0)
        {
            UnpauseUI.AddRange(GameObject.FindGameObjectsWithTag("UnpausedUI"));
            PauseUI.AddRange(GameObject.FindGameObjectsWithTag("PausedUI"));
            PauseUI.ForEach(i => 
            {
                i.SetActive(false);
            });
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Cancel")) 
        {
            if (UIController.UIState == UIStates.Free)
            {
                UIController.ChangeState(UIStates.Paused);
            }
        }
    }

    public static void Pause() 
    {
        PauseUI.ForEach(i => 
        {
            i.SetActive(true);
        });
        UnpauseUI.ForEach(i => { 
            i.SetActive(false);
        });
        Time.timeScale = 0f;
        Cursor.lockState = CursorLockMode.Confined;
        Cursor.visible = true;
    }

    public static void Unpause() 
    {
        PauseUI.ForEach(i => {
            i.SetActive(false);
        });
        UnpauseUI.ForEach(i =>
        {
            i.SetActive(true);
        });
        Time.timeScale = 1f;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }
}
