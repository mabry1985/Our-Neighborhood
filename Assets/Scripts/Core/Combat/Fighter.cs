using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fighter : MonoBehaviour, IAction
{
    [SerializeField] float weaponRange = 5f;
    [SerializeField] float timeBetweenAttack = 1f;
    [SerializeField] float weaponDamage = 1f;
    [SerializeField] GameObject projectilePrefab;
    [SerializeField] Transform projectileSpawnPoint;

    Health target;
    float timeSinceLastAttack = Mathf.Infinity;

    private void Start() {
        projectileSpawnPoint = GetComponentInChildren<ProjectileSpawnPoint>().transform ?? null;
    }

    private void Update()
    {
        timeSinceLastAttack += Time.deltaTime;

        if (target == null) return;
        if (target.IsDead()) return;

        bool isInRange = Vector3.Distance(transform.position, target.transform.position) < weaponRange;
        if (target != null && !isInRange)
        {
            GetComponent<Mover>().MoveTo(target.transform.position);
        }
        else
        {
            GetComponent<Mover>().Cancel();
            AttackBehaviour();
        }
    }

    private void AttackBehaviour()
    {
        transform.LookAt(target.transform);
        if (timeSinceLastAttack >= timeBetweenAttack)
        {
            TriggerAttack();
            timeSinceLastAttack = 0;
        }
    }

    private void TriggerAttack()
    {
        GetComponent<Animator>().SetTrigger("attack");
        GetComponent<Animator>().ResetTrigger("stopAttack");
    }

    //animation event
    void Hit()
    {
        if (target == null) return;
        target.TakeDamage(weaponDamage);
    }

    //animation event
    void LaunchProjectile()
    {
        if (target == null) return;
        Instantiate(projectilePrefab, projectileSpawnPoint.position, transform.rotation);
    }

    public bool CanAttack(GameObject combatTarget)
    {
        if (combatTarget == null) return false;

        Health targetToTest = combatTarget.GetComponent<Health>();
        return targetToTest != null && !targetToTest.IsDead();
    }

    public void Attack(GameObject combatTarget)
    {
        GetComponent<ActionScheduler>().StartAction(this);
        target = combatTarget.GetComponent<Health>();
    }

    public void Cancel()
    {
        GetComponent<Animator>().SetTrigger("stopAttack");
        GetComponent<Animator>().ResetTrigger("attack");
        target = null;
    }


}