using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ONLY HAVE ONE OF THESE.
/// This system 
/// </summary>
public class PauseController : MonoBehaviour
{
    private static List<GameObject> PauseUI = new();
    private void Start()
    {
        if (PauseUI.Count == 0)
        {
            PauseUI.AddRange(GameObject.FindGameObjectsWithTag("PauseUI"));
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

                Time.timeScale = 1.0f;
            }
        }
    }

    public static void Pause() 
    {
        // This will handle any needed
        UIController.ChangeState(UIStates.Paused);
        PauseUI.ForEach(i => 
        {
            i.SetActive(true);
        });
        Time.timeScale = 0f;
    }
}
