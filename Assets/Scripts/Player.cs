using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private Joystick joystick;
    [SerializeField] private int HP = 7;
    [SerializeField] private float attackDistance = 7f;
    [SerializeField] private float attackTime = 0.5f;
    [SerializeField] private float plyerZborder = -1.35f;
    [SerializeField] private Bullet bullet;

    private float attackTimer = 0f;
    private int money = 0;
    private Rigidbody rb;
    private Animator animator;
    private bool isLive = false;

    private void OnEnable()
    {
        rb = GetComponent<Rigidbody>();
        animator = GetComponentInChildren<Animator>();
        EventManager.Subscribe(eEventType.LevelStart, (arg) => levelStart());
    }

    private void Update()
    {
        if (isLive)
        {
            Vector3 Movement = new Vector3(joystick.Horizontal / 2f, 0.0f, joystick.Vertical);
            rb.MovePosition(rb.position + Movement * 3f * Time.deltaTime);

            Vector3 direction = new Vector3(joystick.Horizontal, 0, joystick.Vertical);

            Vector3 newDirection = Vector3.RotateTowards(animator.transform.forward, direction, 10f * Time.deltaTime, 0.0f);

            animator.transform.rotation = Quaternion.LookRotation(newDirection);
            if (direction.magnitude > 0)
                animator.SetBool("Walk", true);
            else
                animator.SetBool("Walk", false);

            if (transform.position.z > plyerZborder)
            {
                attackTimer += Time.deltaTime;
                if (attackTimer >= attackTime)
                {

                    float minDistance = 999;
                    attackTimer = 0;
                    Enemy target = null;
                    Enemy[] enemies = FindObjectsOfType<Enemy>();
                    if (enemies.Length > 0)
                    {
                        for (int i = 0; i < enemies.Length; i++)
                        {
                            if (enemies[i].IsLive)
                            {
                                if (Vector3.Distance(transform.position, enemies[i].transform.position) < minDistance)
                                {
                                    target = enemies[i];
                                    minDistance = Vector3.Distance(transform.position, enemies[i].transform.position);
                                }
                            }
                        }
                        if (target != null)
                        {
                            GameObject bulletTemp = Instantiate(bullet, transform.position + Vector3.forward, Quaternion.identity).gameObject;
                            bulletTemp.transform.LookAt(target.transform.position + Vector3.up);
                            Destroy(bulletTemp, 3f);
                        }
                    }
                    return;
                }
            }

        }
    }

    private void levelStart()
    {
        isLive = true;
    }

    public void AppllyDamage(int damage)
    {
        HP -= damage;
        EventManager.OnEvent(eEventType.PlayerDamag, HP);
        if (HP <= 0)
        {
            isLive = false;
            EventManager.OnEvent(eEventType.LevelLost);
        }
    }


    private void OnDisable()
    {
        EventManager.Unsubscribe(eEventType.LevelStart, (arg) => levelStart());

    }
}
