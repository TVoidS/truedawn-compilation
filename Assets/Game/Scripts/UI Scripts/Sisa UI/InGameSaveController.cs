using UnityEngine;

/// <summary>
/// This is primarily to pre-load the DetailedSaveDisplay with the current save data when the SaveUI enables.
/// It will also swap the data when it is told to by an OnClick that all prefabs will have.
/// </summary>
public class InGameSaveController : MonoBehaviour
{
    public DetailedSaveDisplayDistributor DetailedSaveDisplay;

    private Transform Contents;

    public GameObject SaveSlotPrefab;

    private void Awake()
    {
        Contents = gameObject.transform.Find("Scroll View").transform.Find("Viewport").transform.Find("Content").GetComponent<Transform>();
    }

    /// <summary>
    /// Fills the DetailedSaveDisplay with the current game's save data, despite there not being a save.
    /// This will activate whenever the screen is enabled, so it should happen whenever the window is open.
    /// </summary>
    private void OnEnable()
    {
        DetailedSaveDisplay.Distribute(SaveLoad.CurrentSaveDetails());

        SaveLoad.GetSaveDatas().ForEach(save => {
            // Generate Prefab in Content.
            // Get Prefab Component (SaveSlotController)
            // Run Fill(x); on component
            GameObject slot = Instantiate(SaveSlotPrefab, Contents);
            slot.GetComponent<SaveSlotContoller>().Fill(save);
        });
    }
}
