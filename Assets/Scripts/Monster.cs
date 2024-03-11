using UnityEngine;
using UnityEngine.AI;

public class Monster : MonoBehaviour
{
    public NavMeshAgent ai;
    public Transform player;
    public Camera playerCam;
    public float aiSpeed;

    private bool isInLineOfSight = true; // Flag to track line of sight
    private bool isInView = true; // Flag to track line of sight

    private void Update()
    {
        LineOScheck();
        InView();

        if (isInView && isInLineOfSight)
        {
            ai.speed = 0;
            ai.SetDestination(transform.position);
        }
        else
        {
            ai.speed = aiSpeed;
            ai.destination = player.position;
        }
    }

    private void LineOScheck()
    {
        RaycastHit hit;
        Vector3 direction = player.position - transform.position;

        if (Physics.Raycast(transform.position, direction, out hit))
        {
            if (hit.transform == player) 
            {
                isInLineOfSight = true;
            }
            else 
            {
                isInLineOfSight = false;
            }
        }
    }

    private void InView()
    {
        Plane[] planes = GeometryUtility.CalculateFrustumPlanes(playerCam);
        Renderer renderer = GetComponent<Renderer>();

        if (GeometryUtility.TestPlanesAABB(planes, renderer.bounds))
        {
            isInView = true;
        }
        else
        {
            isInView = false;
        }
    }
}
