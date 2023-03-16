using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SaveController : MonoBehaviour
{
    private Button saveButton;
    private List<GameObject> saveUI = new();

    public List<GameObject> DefaultUI = new();
    public List <GameObject> SecondUI = new();

    // Start is called before the first frame update
    void Start()
    {
        saveButton = GameObject.FindGameObjectWithTag("SaveButton").GetComponent<Button>();

        saveButton.onClick.AddListener(() => {
            SwapToSaveUI();
        });

        saveUI.AddRange(GameObject.FindGameObjectsWithTag("SaveUI"));
        saveUI.ForEach(x => x.SetActive(false));
    }

    private void SwapToSaveUI() 
    {
        // Disable old UI.

        saveUI.ForEach(x => x.SetActive(true));

    }

    public void DisableSaveUI() 
    {
        saveUI.ForEach(x => x.SetActive(false));
    }
}
