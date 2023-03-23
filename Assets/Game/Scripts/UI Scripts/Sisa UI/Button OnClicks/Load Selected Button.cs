using UnityEngine;
using UnityEngine.UI;

public class LoadSelectedButton : MonoBehaviour
{
    public PauseUISwapper UISwapper;
    public DetailedSaveDisplayDistributor DetailedSaveDisplay;

    private void Awake()
    {
        gameObject.GetComponent<Button>().onClick.AddListener(() => {
            DetailedSaveDisplay.LoadSave();
            UISwapper.SwapToPlayUI();
        });
    }
}
