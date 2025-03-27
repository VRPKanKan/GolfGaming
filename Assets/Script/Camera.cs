using UnityEngine;

public class Camera : MonoBehaviour
{
    public Transform target;
    private Vector3 offset;

    private void Start()
    {
        offset = transform.position;
    }

    private void Update()
    {
        transform.position = new Vector3(target.position.x + offset.x,target.position.y + offset.y, target.position.z + offset.z);        
    }

}
