using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//[RequireComponent(typeof(Rigidbody))]
//[RequireComponent(typeof(CapsuleCollider))]
public class Enemy : MonoBehaviour
{
    public bool IsLive => isLive;


    [SerializeField] private float activDistance = 5;
    [SerializeField] private float hookDistance = 1;
    [SerializeField] private float plyerZborder = -1.35f;
    [SerializeField] private GameObject money;
    private Rigidbody[] rigidbodiesRagDoll = new Rigidbody[] { };
    private Collider[] collidersRagDoll = new Collider[] { };

    private Rigidbody mainRigidbody;
    private Collider defaultColider;

    private Transform target;

    private EnemyAttack enemyAttack;
    private EnemyAnimator enemyAnimator;
    private bool isLive = false;

    private State state = State.Idle;
    private enum State
    {
        Move,
        Attack,
        Idle,
        RagDoll,
        Finish
    };
    private void OnEnable()
    {
        defaultColider = GetComponent<Collider>();
        mainRigidbody = GetComponent<Rigidbody>();
        enemyAttack = GetComponentInChildren<EnemyAttack>();
        enemyAnimator = GetComponentInChildren<EnemyAnimator>();
        rigidbodiesRagDoll = GetComponentsInChildren<Rigidbody>();
        collidersRagDoll = GetComponentsInChildren<Collider>();

        target = FindObjectOfType<Player>().transform;

        SetActiveRagDoll(false);

        enemyAttack.Init(this);
        enemyAnimator.Init(this);

        EventManager.Subscribe(eEventType.LevelStart, (arg) => levelStart());
        EventManager.Subscribe(eEventType.LevelComplete, (arg) => levelFinish());
        EventManager.Subscribe(eEventType.LevelLost, (arg) => levelFinish());

    }
    private void levelStart()
    {
        isLive = true;
    }
    private void levelFinish()
    {
        isLive = false;
    }

    private void Update()
    {

        if (!isLive)
        {
            if (state == State.Finish)
            {

                transform.position += Vector3.down * Time.deltaTime;

            }
            return;
        }

        switch (state)
        {
            case State.Idle:
                if (Vector3.Distance(transform.position, target.position) < activDistance && target.transform.position.z > plyerZborder)
                {
                    enemyAnimator.Move();
                    state = State.Move;
                }
                return;
            case State.Move:
                if (target.transform.position.z < plyerZborder)
                {
                    state = State.Idle;
                    enemyAnimator.Idle();
                    return;
                }
                if (Vector3.Distance(transform.position, target.position) < hookDistance)
                {
                    enemyAnimator.StartHook();

                    state = State.Attack;
                    return;
                }
                return;

        }
    }

    private void FixedUpdate()
    {

        if (state == State.Idle)
        {

            return;

        }

        if (state == State.Move)
        {
            transform.LookAt(new Vector3(target.position.x, transform.position.y, target.position.z));
            Vector3 dir = (target.position - mainRigidbody.position).normalized;

            mainRigidbody.MovePosition(mainRigidbody.position + dir * Time.fixedDeltaTime);
            return;

        }

        if (state == State.Attack)
        {
            return;
        }

    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.TryGetComponent<Bullet>(out Bullet bullet))
        {
            Destroy(bullet.gameObject);
            Destroy(gameObject,2);
            Die();
        }

    }

    public void AttackEnd()
    {
        enemyAnimator.Move();
        state = Enemy.State.Move;
    }


    public void Die()
    {
        isLive = false;

        SetActiveRagDoll(true);
        GetRagDollRigidbody().velocity = (transform.position - target.position + Vector3.up).normalized * 30f;


        if (Random.value > 0.5f)
            Instantiate(money, transform.position, Quaternion.identity);
    }
    private Rigidbody GetRagDollRigidbody()
    {
        return rigidbodiesRagDoll[1];
    }
    private void SetActiveRagDoll(bool value)
    {
        foreach (var item in collidersRagDoll)
        {
            item.enabled = value;
        }
        foreach (var item in rigidbodiesRagDoll)
        {
            item.isKinematic = !value;
        }
        defaultColider.enabled = !value;
        mainRigidbody.isKinematic = value;
        enemyAnimator.SetAnimatorActive(!value);
        if (value)
        {
            EventManager.OnEvent(eEventType.EnemyDie);

            state = State.RagDoll;
            enemyAttack.enabled = false;
            enemyAnimator.enabled = false;
        }
    }



}
