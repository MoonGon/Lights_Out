using UnityEngine;
using Unity.Cinemachine;
using System.Collections;

public class ElevatorTransition : MonoBehaviour
{
    [Header("Maps to Switch")]
    public GameObject tutorialMap;
    public GameObject mainMap;

    [Header("Doors")]
    public GameObject tutorialDoor; 
    public GameObject mainDoor;    

    [Header("Camera System")]
    public CinemachineConfiner2D confiner;
    public PolygonCollider2D mainMapBounds;

    public void StartTransitionDelay()
    {
        StartCoroutine(TransitionRoutine());
    }

    private IEnumerator TransitionRoutine()
    {
        if (tutorialDoor != null) tutorialDoor.SetActive(true);
        Debug.Log("ประตูปิด... กำลังสลับฉากในอีก 5 วินาที");

        yield return new WaitForSeconds(5f);

        if (tutorialMap != null) tutorialMap.SetActive(false);
        if (mainMap != null) mainMap.SetActive(true);

        if (confiner != null && mainMapBounds != null)
        {
            confiner.BoundingShape2D = mainMapBounds;
            confiner.InvalidateBoundingShapeCache();
        }

        if (mainDoor != null) mainDoor.SetActive(false);
    }
}