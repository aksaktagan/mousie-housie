using UnityEngine;

public class CatAnimations : MonoBehaviour
{
    private const string IS_WALKING = "IsWalking";

    [SerializeField] private EnemyAI cat;

    private Animator animator;

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    private void Update()
    {
        animator.SetBool(IS_WALKING, cat.CatWalk());
    }
}
