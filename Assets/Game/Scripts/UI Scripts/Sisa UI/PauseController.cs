using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ONLY HAVE ONE OF THESE.
/// This system 
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
        if (UIController.UIState == UIStates.Free)
        {
            if (Input.GetButtonDown("Cancel")) 
            {
                UIController.ChangeState(UIStates.Paused);
            }
        }
        else if(UIController.UIState == UIStates.Paused) 
        {
            if(Input.GetButtonDown("Cancel"))
            {
                UIController.ChangeState(UIStates.Free);
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
