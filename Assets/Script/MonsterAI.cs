using UnityEngine;
using UnityEngine.Rendering.Universal;

public class MonsterAI : MonoBehaviour
{
    public enum TargetType { ElevatorOnly, PlayerOnly, ClosestTarget }

    [Header("Monster Settings")]
    public TargetType targetType = TargetType.ElevatorOnly;
    public float speed = 2f;

    private Transform player;
    private Transform elevator;
    private Transform currentTarget;

    void Start()
    {

        GameObject pObj = GameObject.FindGameObjectWithTag("Player");
        if (pObj != null) player = pObj.transform;

        GameObject eObj = GameObject.FindGameObjectWithTag("Elevator");
        if (eObj != null) elevator = eObj.transform;
    }

    void Update()
    {
        DetermineTarget(); 

        if (currentTarget != null)
        {

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

                    float distToPlayer = Vector2.Distance(transform.position, player.position);
                    float distToElevator = Vector2.Distance(transform.position, elevator.position);


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

            Debug.Log("มอนสเตอร์พุ่งชนผู้เล่น!");
            Destroy(gameObject);
        }
    }
}