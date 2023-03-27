using UnityEngine;

public class CanvasContentsController : MonoBehaviour
{
    public GameObject LoadingContainer;
    public GameObject SaveSelectContainer;
    public GameObject MainMenuContainer;
    public GameObject NewSaveContainer;

    void Start()
    {
        MainMenuContainer.SetActive(true);
        LoadingContainer.SetActive(false);
        SaveSelectContainer.SetActive(false);
        NewSaveContainer.SetActive(false);
    }

    /// <summary>
    /// Handles the swapping to different displays.
    /// Will automatically disable the current display and enable the desired display.
    /// </summary>
    /// <param name="container"> the ID for the containers. 0 for Main, 1 for Save, 2 for Loading, 3 for New Game </param>
    public void SwapTo(byte container) 
    {
        switch (container) 
        {
            case 0:
                NewSaveContainer.SetActive(false);
                MainMenuContainer.SetActive(true); 
                SaveSelectContainer.SetActive(false); 
                LoadingContainer.SetActive(false); 
                break;
            case 1:
                NewSaveContainer.SetActive(false);
                MainMenuContainer.SetActive(false); 
                SaveSelectContainer.SetActive(true); 
                LoadingContainer.SetActive(false); 
                break;
            case 2:
                NewSaveContainer.SetActive(false);
                MainMenuContainer.SetActive(false); 
                SaveSelectContainer.SetActive(false); 
                LoadingContainer.SetActive(true); 
                break;
            case 3:
                NewSaveContainer.SetActive(true);
                MainMenuContainer.SetActive(false);
                SaveSelectContainer.SetActive(false);
                LoadingContainer.SetActive(false);
                break;

        }
    }
}
