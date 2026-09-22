using UnityEngine;

public class TutorialZone : MonoBehaviour
{
    [Header("ข้อความที่จะแสดงเมื่อเดินมาถึง")]
    public string messageToShow;

    void OnTriggerEnter2D(Collider2D other)
    {
        // ถ้าคนที่เดินมาชนคือ Player ให้โชว์ข้อความ
        if (other.CompareTag("Player"))
        {
            PlayerLogic player = other.GetComponent<PlayerLogic>();
            if (player != null) player.ShowTutorialText(messageToShow);
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        // ถ้า Player เดินออกจากโซน ให้ซ่อนข้อความ
        if (other.CompareTag("Player"))
        {
            PlayerLogic player = other.GetComponent<PlayerLogic>();
            if (player != null) player.HideTutorialText();
        }
    }
}