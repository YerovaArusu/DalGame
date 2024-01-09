using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class WeaponHandler : MonoBehaviour
{
    [SerializeField] public Animator animator;
    [SerializeField] public float delay = 0.3f;
    private bool attackBlocked = false;
    
    public bool isAttacking { get; private set; }
    public Transform circleOrigin;
    [SerializeField] public float radius = 2f;

    public void resetIsAttacking()
    {
        isAttacking = false;
    }

    private void Update()
    {

    }


    public void attack()
    {
        if (attackBlocked)
        {
            return;
        }

        isAttacking = true;
        animator.SetTrigger("Attack");
        attackBlocked = true;
        StartCoroutine(DelayAttack());
        detectCollidersAndDamage();
    }

    private IEnumerator DelayAttack()
    {
        yield return new WaitForSeconds(delay);
        attackBlocked = false;
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.cyan;
        Vector3 position = circleOrigin == null ? Vector3.zero : circleOrigin.position;
        Gizmos.DrawWireSphere(position, radius);
    }

    public void detectCollidersAndDamage()
    {
        foreach (Collider2D collider in Physics2D.OverlapCircleAll(circleOrigin.position, radius))
        {
                if (isAttacking && !collider.transform.IsChildOf(transform.parent) && collider.transform.GetComponent<Collider2D>().TryGetComponent<StatsSystem>(out StatsSystem system))
                {
                    system.receiveDamage(20, transform.parent.gameObject, false);
                }
        }
    }
}
