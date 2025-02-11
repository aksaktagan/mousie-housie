using UnityEngine;

public class HidingScript : MonoBehaviour
{
    public bool playerIsHiding;

    public void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerIsHiding = true;

            Debug.Log("INSIDE");
        }
    }

    public void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerIsHiding = false;

            Debug.Log("OUTSIDE");
        }
    }

    public bool MouseIsHiding()
    {
        return playerIsHiding;
    }
}
