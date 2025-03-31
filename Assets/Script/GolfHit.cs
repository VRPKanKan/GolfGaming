using UnityEngine;
using UnityEngine.UI;

public class GolfHit : MonoBehaviour
{
    public float maxForce = 20f;
    public float chargeSpeed = 10f;
    public float rotationSpeed = 100f; 
    public float rayDistance = 10f;
    public Slider chargeBar; 

    private float currentForce = 0f;
    private Rigidbody rb;
    private Vector3 hitDirection = Vector3.forward;
    private LineRenderer lineRenderer;
    private float verticalAngle = 0f; 
    private float maxVerticalAngle = 45f; 

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        lineRenderer = GetComponent<LineRenderer>();
        lineRenderer.positionCount = 2; 
        lineRenderer.startWidth = 0.05f;
        lineRenderer.endWidth = 0.05f;
        lineRenderer.material = new Material(Shader.Find("Sprites/Default"));
        lineRenderer.startColor = Color.green;
        lineRenderer.endColor = Color.green;

        chargeBar.maxValue = maxForce;
        chargeBar.value = 0f;
    }

    void Update()
    {
        RotateDirection();
        DrawRaycast();

        if (Input.GetMouseButton(0))
        {
            ChargeForce();
        }

        if (Input.GetMouseButtonUp(0)) 
        {
            ApplyForce();
        }
    }

    void RotateDirection()
    {
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical"); 

        if (horizontal != 0)
        {
            transform.Rotate(Vector3.up, horizontal * rotationSpeed * Time.deltaTime);
        }

        if (vertical != 0)
        {
            verticalAngle -= vertical * rotationSpeed * Time.deltaTime;
            verticalAngle = Mathf.Clamp(verticalAngle, -maxVerticalAngle, maxVerticalAngle);
            transform.localEulerAngles = new Vector3(verticalAngle, transform.localEulerAngles.y, 0f);
        }

        hitDirection = transform.forward; 
    }

    void ChargeForce()
    {
        if (currentForce < maxForce)
        {
            currentForce += chargeSpeed * Time.deltaTime; 
            chargeBar.value = currentForce; 
        }
    }

    void ApplyForce()
    {
        rb.AddForce(hitDirection * currentForce, ForceMode.Impulse);
        currentForce = 0f; 
        chargeBar.value = 0f; 
    }

    void DrawRaycast()
    {
        RaycastHit hit;
        Vector3 startPoint = transform.position;
        Vector3 endPoint = startPoint + hitDirection * rayDistance;

        if (Physics.Raycast(startPoint, hitDirection, out hit, rayDistance))
        {
            endPoint = hit.point;
            lineRenderer.startColor = Color.red;
            lineRenderer.endColor = Color.red;
            Debug.Log("Hit: " + hit.collider.name);
        }
        else
        {
            lineRenderer.startColor = Color.green;
            lineRenderer.endColor = Color.green;
        }

        lineRenderer.SetPosition(0, startPoint);
        lineRenderer.SetPosition(1, endPoint);
    }
}
