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
    public ElevatorTransition elevatorTransition;

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

        if (other.CompareTag("Energy"))
        {
            hasEnergy = true;
            Destroy(other.gameObject);
        }


        if (other.CompareTag("Elevator") && hasEnergy == true)
        {
            hasEnergy = false;
            if (roomCeilingLight != null) roomCeilingLight.intensity = roomLightOffIntensity;
            if (elevatorLight != null) elevatorLight.intensity = elevatorLightOnIntensity;
            if (gameManager != null) gameManager.StartWave();
        }


        else if (other.CompareTag("TutorialElevator"))
        {
            if (hasEnergy == true)
            {
                hasEnergy = false;


                if (elevatorTransition != null)
                {
                    elevatorTransition.StartTransitionDelay();
                }
            }
            else
            {

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