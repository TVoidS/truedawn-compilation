using UnityEngine;

public class PauseUISwapper : MonoBehaviour
{
    public GameObject SaveUI;
    public GameObject PauseUI;

    // Start is called before the first frame update
    void Start()
    {
        // Repeat this if there are any other instances of UI types that get added.
        SaveUI.SetActive(false);
    }

    private void Update()
    {
        if (UIController.UIState == UIStates.Paused) 
        {
            if (Input.GetButtonDown("Cancel")) 
            {
                SwapToPlayUI();
            }
        }
    }

    /// <summary>
    /// Swaps the currently displayed UI to the SaveUI
    /// </summary>
    public void SwapToSaveUI() 
    {
        SaveUI.SetActive(true);
        PauseUI.SetActive(false);
    }

    /// <summary>
    /// Swaps the currently displayed UI to the PauseUI.
    /// </summary>
    public void SwapToPauseUI() 
    {
        SaveUI.SetActive(false);
        PauseUI.SetActive(true);
    }

    /// <summary>
    /// Swaps the currently displayed UI to the Game UI.  May be useless, who knows.
    /// </summary>
    public void SwapToPlayUI() 
    {
        PauseUI.SetActive(true);
        SaveUI.SetActive(false);

        UIController.ChangeState(UIStates.Free);
    }
}
