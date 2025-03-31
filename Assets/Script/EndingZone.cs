using UnityEngine;

public class EndingZone : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player")) 
        {
            FindObjectOfType<Timer>().StopTimer(); 
            Debug.Log("Level Completed! Timer Stopped.");
        }
    }
}
