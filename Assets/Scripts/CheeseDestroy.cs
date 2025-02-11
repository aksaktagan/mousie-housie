using UnityEngine;

public class CheeseDestroy : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        CheeseCollectables cheeseCol = other.GetComponent<CheeseCollectables>();

        if (cheeseCol != null)
        {
            cheeseCol.CheeseCollected();
            gameObject.SetActive(false);
        }
    }
}
