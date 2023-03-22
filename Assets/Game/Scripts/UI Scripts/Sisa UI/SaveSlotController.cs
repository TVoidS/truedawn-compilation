using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SaveSlotContoller : MonoBehaviour
{
    private SaveData save;

    // Start is called before the first frame update
    void Awake()
    {
        gameObject.transform.Find("SlotClick").GetComponent<Button>().onClick.AddListener(() => {
            GameObject.FindGameObjectWithTag("detaileddistributor").GetComponent<DetailedSaveDisplayDistributor>().Distribute(save);
        });
    }

    public void Fill(SaveData data) 
    {
        save = data;
        gameObject.transform.Find("Name").GetComponent<TextMeshProUGUI>().SetText(data.Name);
        gameObject.transform.Find("SaveTime").GetComponent<TextMeshProUGUI>().SetText(data.LastSaveTime.ToString());
    }

    private void OnDisable()
    {
        Destroy(gameObject);
        // YAY DESTRUCTION!!!
    }
}
