using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TraitMeleeAttack : MonoBehaviour {

    private float timeBtwAttack;

    public float startTimeBtwAttack;
    public float attackRange;
    public float attackDamage;
    public Transform attackPos;
    public LayerMask enemyLayer;

    public Animator attackAnimator;

	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
		if (timeBtwAttack <= 0)
        {
            timeBtwAttack = 0;
            attackAnimator.SetBool("isAttacking", false);
        } else
        {
            timeBtwAttack -= Time.deltaTime;
        }
	}

    public void OnMeleeAttackInputDown()
    {
        if (timeBtwAttack <= 0)
        {
            attackAnimator.SetBool("isAttacking", true);
            //animator.Play("Attack");
            timeBtwAttack = startTimeBtwAttack;

            Collider2D[] enemiesToDamage = Physics2D.OverlapCircleAll(attackPos.position, attackRange, enemyLayer);

            for (int i = 0; i < enemiesToDamage.Length; i++)
            {
                enemiesToDamage[i].GetComponent<EnemyController>().TakeDamage(attackDamage);
            }
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(attackPos.position, attackRange);
    }
}
