using UnityEngine;
using UnityEngine.UI;

public class NewBehaviourScript : MonoBehaviour
{
    public PauseUISwapper UISwapper;

    private void Awake()
    {
        gameObject.GetComponent<Button>().onClick.AddListener(() => {
            SaveLoad.Save();
            UISwapper.SwapToPlayUI();
        });
    }
}
