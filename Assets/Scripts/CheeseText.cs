using TMPro;
using UnityEngine;

public class CheeseText : MonoBehaviour
{
    private TextMeshProUGUI cheeseText;

    void Start()
    {
        cheeseText = GetComponent<TextMeshProUGUI>();
    }

    public void UpdateCheeseText(CheeseCollectables cheeseCol)
    {
        cheeseText.text = cheeseCol.NumOfCheese.ToString();
    }
}
