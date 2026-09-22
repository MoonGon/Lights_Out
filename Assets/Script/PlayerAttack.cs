using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    [Header("Attack Settings")]
    public Transform attackPoint; // ตำแหน่งที่จะเกิดดาเมจ (ก้อนสีแดง)
    public float attackRange = 0.8f; // ระยะความกว้างของการฟัน

    void Update()
    {
        // ถ้าคลิกเมาส์ซ้าย (0 คือคลิกซ้าย)
        if (Input.GetMouseButtonDown(0))
        {
            Attack();
        }
    }

    void Attack()
    {
        // 1. สร้างวงกลมล่องหนขึ้นมาตรงตำแหน่ง AttackPoint เพื่อจับว่าโดนอะไรบ้าง
        Collider2D[] hitObjects = Physics2D.OverlapCircleAll(attackPoint.position, attackRange);

        // 2. วนลูปเช็กว่าสิ่งที่อยู่ในวงกลม มีแท็กเป็นมอนสเตอร์ไหม?
        foreach (Collider2D obj in hitObjects)
        {
            if (obj.CompareTag("Monster"))
            {
                Debug.Log("ฟันโดน! มอนสเตอร์ตายแล้ว!");
                Destroy(obj.gameObject); // ทำลายมอนสเตอร์
            }
        }
    }

    // ฟังก์ชันนี้ช่วยวาดวงกลมสีแดงในหน้า Scene ให้เรากะระยะฟันได้ง่ายๆ (ในเกมจะไม่เห็น)
    void OnDrawGizmosSelected()
    {
        if (attackPoint == null) return;
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(attackPoint.position, attackRange);
    }
}