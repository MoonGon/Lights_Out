using UnityEngine;
using UnityEngine.Rendering.Universal;

public class MonsterAI : MonoBehaviour
{
    // สร้างตัวเลือก (Dropdown) สำหรับประเภทมอนสเตอร์
    public enum TargetType { ElevatorOnly, PlayerOnly, ClosestTarget }

    [Header("Monster Settings")]
    public TargetType targetType = TargetType.ElevatorOnly; // เลือกประเภทมอนสเตอร์ตรงนี้
    public float speed = 2f;

    private Transform player;
    private Transform elevator;
    private Transform currentTarget;

    void Start()
    {
        // หาตัวผู้เล่นและลิฟต์ในฉากเตรียมไว้
        GameObject pObj = GameObject.FindGameObjectWithTag("Player");
        if (pObj != null) player = pObj.transform;

        GameObject eObj = GameObject.FindGameObjectWithTag("Elevator");
        if (eObj != null) elevator = eObj.transform;
    }

    void Update()
    {
        DetermineTarget(); // คำนวณหาเป้าหมายก่อนเดิน

        if (currentTarget != null)
        {
            // เดินพุ่งไปหาเป้าหมายที่เลือกไว้
            transform.position = Vector2.MoveTowards(transform.position, currentTarget.position, speed * Time.deltaTime);
        }
    }

    void DetermineTarget()
    {
        switch (targetType)
        {
            case TargetType.ElevatorOnly:
                currentTarget = elevator;
                break;

            case TargetType.PlayerOnly:
                currentTarget = player;
                break;

            case TargetType.ClosestTarget:
                if (player != null && elevator != null)
                {
                    // วัดระยะทางว่าใครอยู่ใกล้กว่ากัน
                    float distToPlayer = Vector2.Distance(transform.position, player.position);
                    float distToElevator = Vector2.Distance(transform.position, elevator.position);

                    // ถ้าผู้เล่นอยู่ใกล้กว่า ให้พุ่งหาผู้เล่น ถ้าลิฟต์ใกล้กว่าพุ่งหาลิฟต์
                    currentTarget = (distToPlayer < distToElevator) ? player : elevator;
                }
                else if (player != null) currentTarget = player;
                else if (elevator != null) currentTarget = elevator;
                break;
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Elevator"))
        {
            Light2D elevatorLight = other.GetComponentInChildren<Light2D>();
            if (elevatorLight != null)
            {
                elevatorLight.pointLightOuterRadius -= 1f;
                if (elevatorLight.pointLightOuterRadius <= 0) elevatorLight.pointLightOuterRadius = 0;
            }
            Destroy(gameObject);
        }
        else if (other.CompareTag("Player"))
        {
            // เมื่อมอนสเตอร์วิ่งมาชนผู้เล่น (เอิร์ธสามารถเพิ่มระบบลดเลือดตัวละครตรงนี้ได้ในอนาคต)
            Debug.Log("มอนสเตอร์พุ่งชนผู้เล่น!");
            Destroy(gameObject);
        }
    }
}