using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAnimator : MonoBehaviour
{
    private Enemy enemy;
    private Animator animator;
    private int moveType = 1;
    private int idleType = 1;

    public void Init(Enemy enemy)
    {
        this.enemy = enemy;
    }

    private void OnEnable()
    {
        moveType = Random.Range(1, 3);
        idleType = Random.Range(2, 4);

        animator = GetComponent<Animator>();
        animator.speed = Random.Range(0.9f, 1.1f);

        animator.SetInteger("idle", idleType);
    }
    public void Idle()
    {
        animator.SetInteger("idle", idleType);
        animator.SetInteger("walkType", 0);

    }
    public void StartHook()
    {
        animator.SetInteger("idle", 0);
        animator.SetTrigger("hook");
        animator.SetInteger("walkType", 0);
    }

    public void Move()
    {
        animator.SetInteger("idle", 0);
        animator.SetInteger("walkType", moveType);
    }

    public void AttackEnd()
    {
        enemy.AttackEnd();
    }

    public void SetAnimatorActive(bool value)
    {
        if (animator == null)
            animator = GetComponent<Animator>();

        animator.enabled = value;
    }
}
