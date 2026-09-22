using UnityEngine;
using UnityEngine.Rendering.Universal;
using TMPro;

public class PlayerLogic : MonoBehaviour
{
    [Header("Player Status")]
    public bool hasEnergy = false;

    [Header("Lighting Setup")]
    public Light2D roomCeilingLight;
    public Light2D elevatorLight;
    public GameObject flashlight;

    [Header("Light Intensity Settings")]
    public float roomLightOffIntensity = 0f;
    public float elevatorLightOnIntensity = 1f;

    [Header("UI & Systems")]
    public TextMeshPro tutorialText;
    public GameManager gameManager;
    public ElevatorTransition elevatorTransition; // เพิ่มช่องเชื่อมระบบสลับแมป

    void Start()
    {
        if (flashlight != null) flashlight.SetActive(false);
        if (tutorialText != null) tutorialText.gameObject.SetActive(false);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            if (flashlight != null) flashlight.SetActive(!flashlight.activeSelf);
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        // 1. เก็บพลังงาน (สำหรับห้องจริง)
        if (other.CompareTag("Energy"))
        {
            hasEnergy = true;
            Destroy(other.gameObject);
        }

        // 2. ลิฟต์ห้องเล่นจริง (ต้องมีพลังงาน ถึงจะไฟดับและมอนออก)
        if (other.CompareTag("Elevator") && hasEnergy == true)
        {
            hasEnergy = false;
            if (roomCeilingLight != null) roomCeilingLight.intensity = roomLightOffIntensity;
            if (elevatorLight != null) elevatorLight.intensity = elevatorLightOnIntensity;
            if (gameManager != null) gameManager.StartWave();
        }

        // 3. ลิฟต์ห้องสอนเล่น (เดินเข้าปุ๊บ สลับไปแมปหลักทันที มอนไม่เกิด)
        else if (other.CompareTag("TutorialElevator"))
        {
            if (hasEnergy == true)
            {
                hasEnergy = false;

                // สั่งให้ลิฟต์เริ่มทำงาน ปิดประตูและนับเวลา 5 วินาที
                if (elevatorTransition != null)
                {
                    elevatorTransition.StartTransitionDelay();
                }
            }
            else
            {
                // ถ้าเดินเข้าลิฟต์แต่ยังไม่มีพลังงาน ให้โชว์ข้อความเตือน
                ShowTutorialText("ลิฟต์ยังไม่มีพลังงาน...");
            }
        }
    }

    public void ShowTutorialText(string message)
    {
        if (tutorialText != null)
        {
            tutorialText.text = message;
            tutorialText.gameObject.SetActive(true);
        }
    }

    public void HideTutorialText()
    {
        if (tutorialText != null)
        {
            tutorialText.gameObject.SetActive(false);
        }
    }
}