using Pathfinding;
using System.Collections.Generic;
using UnityEngine;

public class PathfindingTargetList : MonoBehaviour
{
    public List<Transform> targets;
    [HideInInspector] public Transform currentTarget;
    [SerializeField] int currentTargetNumber;

    public Seeker seeker;
    public AiPlatformerPath platformerPath;

    private void Start()
    {
        if (targets.Count == 0)
        {
            Debug.LogError($"No targets have been assigned to {gameObject}");
        }

        seeker = GetComponent<Seeker>();
        platformerPath = GetComponent<AiPlatformerPath>();

        currentTargetNumber = 0;
    }

    private void Update()
    {
        if (targets.Count == 0)
        {
            return;
        }

        if (currentTargetNumber >= targets.Count)
        {
            currentTargetNumber = 0;
        }
        currentTarget = targets[currentTargetNumber];
    }

    public void IncreaseTargetNumber()
    {
        currentTargetNumber++;
    }

}
