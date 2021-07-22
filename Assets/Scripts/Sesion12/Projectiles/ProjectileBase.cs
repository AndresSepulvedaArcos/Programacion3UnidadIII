using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileBase : MonoBehaviour
{
    Rigidbody rb;

    Transform target;
    public float projectileSpeed = 10f;
    public float baseDamage = 5f;

    PlayerControllerCommand player;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();

    }
   
    public void ShootTo(Transform targetToFollow)
    {
        target = targetToFollow;
    }

    public void ShootToDirection(Vector3 Direction,PlayerControllerCommand Instigator=null)
    {
        rb.velocity = Direction * projectileSpeed;

        player = Instigator;
    }
    private void OnTriggerEnter(Collider other)
    {
        IDamageable damageable = other.GetComponent<IDamageable>();
        if (target != null)
        {
            if (other.transform.GetInstanceID() == target.GetInstanceID())
            {
                damageable?.ApplyDamage(baseDamage);
                DestroyProjectile();
            }
        }else if(player!=null && damageable!=null)
        {
            if(player.GetTeam()!= damageable?.GetTeam())
            {
                damageable?.ApplyDamage(baseDamage);
                DestroyProjectile();
            }
        }

       
    }

    void DestroyProjectile()
    {
        Destroy(gameObject);
    }
    void FollowTarget()
    {
        if (target == null) return;

        transform.LookAt(target);
        rb.velocity = transform.forward.normalized * projectileSpeed;
    }
    // Update is called once per frame
    void Update()
    {
        FollowTarget();
    }
}
