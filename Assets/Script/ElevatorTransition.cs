using UnityEngine;
using Unity.Cinemachine;
using System.Collections; // จำเป็นต้องมีบรรทัดนี้เพื่อใช้ระบบนับเวลา

public class ElevatorTransition : MonoBehaviour
{
    [Header("Maps to Switch")]
    public GameObject tutorialMap;
    public GameObject mainMap;

    [Header("Doors")]
    public GameObject tutorialDoor; // ประตูลิฟต์ห้องสอนเล่น
    public GameObject mainDoor;     // ประตูลิฟต์ห้องหลัก

    [Header("Camera System")]
    public CinemachineConfiner2D confiner;
    public PolygonCollider2D mainMapBounds;

    // ฟังก์ชันนี้จะถูกเรียกเมื่อเอาพลังงานมาใส่ลิฟต์ห้องสอน
    public void StartTransitionDelay()
    {
        StartCoroutine(TransitionRoutine());
    }

    private IEnumerator TransitionRoutine()
    {
        // 1. ปิดประตูลิฟต์ห้องสอน (SetActive เป็น true เพื่อให้กำแพงประตูกลับมาบัง)
        if (tutorialDoor != null) tutorialDoor.SetActive(true);
        Debug.Log("ประตูปิด... กำลังสลับฉากในอีก 5 วินาที");

        // 2. หน่วงเวลา 5 วินาที (เอิร์ธแก้ตัวเลข 5f เป็นเลขอื่นได้ตามชอบ)
        yield return new WaitForSeconds(5f);

        // 3. สลับแมป
        if (tutorialMap != null) tutorialMap.SetActive(false);
        if (mainMap != null) mainMap.SetActive(true);

        // 4. เปลี่ยนขอบเขตกล้อง
        if (confiner != null && mainMapBounds != null)
        {
            confiner.BoundingShape2D = mainMapBounds;
            confiner.InvalidateBoundingShapeCache();
        }

        // 5. เปิดประตูลิฟต์ห้องหลักให้เดินออกไปลุย
        if (mainDoor != null) mainDoor.SetActive(false);
    }
}