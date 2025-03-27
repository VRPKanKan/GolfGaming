using UnityEngine;
using UnityEngine.UI;

public class GolfHit : MonoBehaviour
{
    public float maxForce = 20f; // แรงสูงสุดที่ใช้ได้
    public float chargeSpeed = 10f; // ความเร็วในการชาร์จแรง
    public float rotationSpeed = 100f; // ความเร็วในการหมุนทิศทาง
    public float rayDistance = 10f; // ระยะของ Raycast
    public Slider chargeBar; // หลอดชาร์จ

    private float currentForce = 0f; // แรงปัจจุบันที่ชาร์จอยู่
    private Rigidbody rb;
    private Vector3 hitDirection = Vector3.forward; // ทิศทางการตีเริ่มต้น
    private LineRenderer lineRenderer;
    private float verticalAngle = 0f; // มุมเงยในแกน X
    private float maxVerticalAngle = 45f; // ขีดจำกัดมุมเงย

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        lineRenderer = GetComponent<LineRenderer>();
        lineRenderer.positionCount = 2; // จุดเริ่มและจบ
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

        if (Input.GetMouseButton(0)) // กดค้างเพื่อตี
        {
            ChargeForce();
        }

        if (Input.GetMouseButtonUp(0)) // ปล่อยปุ่มเพื่อตี
        {
            ApplyForce();
        }
    }

    void RotateDirection()
    {
        float horizontal = Input.GetAxis("Horizontal"); // A/D หรือ ลูกศรซ้ายขวา
        float vertical = Input.GetAxis("Vertical"); // W/S หรือ ลูกศรขึ้นลง

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

        hitDirection = transform.forward; // อัปเดตทิศทางตามการหมุนของบอล
    }

    void ChargeForce()
    {
        if (currentForce < maxForce)
        {
            currentForce += chargeSpeed * Time.deltaTime; // เพิ่มแรงทีละน้อย
            chargeBar.value = currentForce; // อัปเดตหลอดชาร์จ
        }
    }

    void ApplyForce()
    {
        rb.AddForce(hitDirection * currentForce, ForceMode.Impulse);
        currentForce = 0f; // รีเซ็ตค่าแรงหลังตี
        chargeBar.value = 0f; // รีเซ็ตหลอดชาร์จ
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
