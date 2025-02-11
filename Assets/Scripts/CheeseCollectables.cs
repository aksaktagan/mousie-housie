using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class CheeseCollectables : MonoBehaviour
{
    public int NumOfCheese { get; private set; }

    public UnityEvent<CheeseCollectables> OnCheeseCollected;

    public void CheeseCollected()
    {
        NumOfCheese++;
        OnCheeseCollected.Invoke(this);
    }

    void Update()
    {
        if(CollectedEveryCheese())
        {
            SceneManager.LoadScene("GameWon");
        }
    }

    bool CollectedEveryCheese()
    {
        return NumOfCheese >= 16;
    }
}
