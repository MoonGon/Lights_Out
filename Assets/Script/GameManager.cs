using UnityEngine;
using UnityEngine.Rendering.Universal;

public class GameManager : MonoBehaviour
{
    [Header("Level Objects")]
    public GameObject door;
    public Transform[] spawnPoints;
    public GameObject[] monsterPrefabs; // รองรับมอนสเตอร์หลายแบบ
    public Light2D roomCeilingLight;    // ไฟเพดานดวงใหญ่ของห้อง

    [Header("Lighting Settings")]
    public float lightOnIntensity = 1f; // ระดับแสงตอนเริ่มเกม (ก่อนไฟดับ)

    [Header("Wave Settings")]
    public float spawnDelay = 2f;
    private bool isWaveStarted = false;

    void Start()
    {
        // เริ่มเกมมาหน่วงเวลา 1 วินาที แล้วเปิดประตู
        Invoke("OpenDoor", 1f);
    }

    void OpenDoor()
    {
        if (door != null)
        {
            door.SetActive(false); // เปิดประตูลิฟต์
            Debug.Log("GameManager: ประตูเปิดแล้ว!");
        }

        // สั่งให้ไฟเพดานสว่างขึ้นมาตามค่าที่ตั้งไว้ตอนเริ่มเกม
        if (roomCeilingLight != null)
        {
            roomCeilingLight.intensity = lightOnIntensity;
        }
    }

    public void StartWave()
    {
        if (isWaveStarted) return;
        isWaveStarted = true;

        Debug.Log("GameManager: ชาร์จพลังงานสำเร็จ! เริ่มสปอว์นมอนสเตอร์!");

        InvokeRepeating("SpawnMonster", 1f, spawnDelay);
    }

    void SpawnMonster()
    {
        if (spawnPoints.Length == 0 || monsterPrefabs.Length == 0) return;

        int randomPoint = Random.Range(0, spawnPoints.Length);
        Transform selectedPoint = spawnPoints[randomPoint];

        int randMonster = Random.Range(0, monsterPrefabs.Length);

        Instantiate(monsterPrefabs[randMonster], selectedPoint.position, Quaternion.identity);
    }
}