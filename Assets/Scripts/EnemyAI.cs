using UnityEngine;
using UnityEngine.AI;
public class EnemyAI: MonoBehaviour
{
    public Transform catForm;
    public NavMeshAgent cat;
    GameObject mouse;

    public float range;

    public Transform centrePoint;

    [SerializeField] LayerMask playerLayer;

    [SerializeField] float sightRange;
    bool playerInSight;

    HidingScript mouseHide;

    bool catIsWalking = false;

    bool chasing, patroling;

    Vector3 point;

    void Start()
    {
        cat = GetComponent<NavMeshAgent>();
        mouse = GameObject.Find("Player");

        mouseHide = GameObject.FindFirstObjectByType<HidingScript>();
    }

    void Patrol()
    {
        if (cat.remainingDistance <= cat.stoppingDistance)
        {
            if (RandomPoint(centrePoint.position, range, out point))
            {
                Debug.DrawRay(point, Vector3.up, Color.blue, 1.0f);
                cat.SetDestination(point);
            }
        }
    }

    public void Update()
    {
        if (mouseHide.MouseIsHiding())
        {
            playerInSight = false;
        }
        else
        {
            playerInSight = Physics.CheckSphere(transform.position, sightRange, playerLayer);
        }

        if(chasing) Chase();
        if (patroling) Patrol();



        if (playerInSight)
        {
            if (!mouseHide.MouseIsHiding())
            {
                chasing = true;
                patroling = false;
            }
            else
            {
                chasing = false;
                patroling = true;
            }
        }
        else
        {
            chasing = false;
            patroling = true;
        }

        if(catForm.position == point)
        {
            catIsWalking = false;
        }
        else
        {
            catIsWalking = true;
        }
    }

    void Chase()
    {
        cat.SetDestination(mouse.transform.position);

        catIsWalking = true;
    }

    bool RandomPoint(Vector3 center, float range, out Vector3 result)
    {
        Vector3 randomPoint = center + Random.insideUnitSphere * range;

        NavMeshHit hit;
        if (NavMesh.SamplePosition(randomPoint, out hit, 1.0f, NavMesh.AllAreas))
        {
            result = hit.position;
            return true;
        }

        result = Vector3.zero;
        return false;
    }

    public bool CatWalk()
    {
        return catIsWalking;
    }
}