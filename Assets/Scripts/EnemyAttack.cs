using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(EnemyAnimator))]
public class EnemyAttack : MonoBehaviour
{
    private Transform bullet;
    private Player target;
    private float projSpeed = 15f;
    private float gravity = 10;
    private Animator animator;

    private EnemyAnimator enemyAnimator;

    private Enemy enemy;
    private void OnEnable()
    {
        enemyAnimator = GetComponent<EnemyAnimator>();
        target = FindObjectOfType<Player>();

    }

    public void Init(Enemy enemy)
    {
        this.enemy = enemy;
    }
    public void setBuletAndTarget(Transform newBullet, Player target)
    {
        bullet = newBullet;
        this.target = target;
    }

    public void Hook()
    {
        if (this.enabled)
            target.AppllyDamage(10);
    }

    public void AttackEnd()
    {
        enemy.AttackEnd();
    }
}
