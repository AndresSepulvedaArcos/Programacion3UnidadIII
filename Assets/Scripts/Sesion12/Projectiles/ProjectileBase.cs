using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileBase : MonoBehaviour
{
    Rigidbody rb;

    Transform target;
    public float projectileSpeed = 10f;
    public float baseDamage = 5f;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();

    }
    // Start is called before the first frame update
    void Start()
    {

    }
    public void ShootTo(Transform targetToFollow)
    {
        target = targetToFollow;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (target == null) return;

        if (other.transform.GetInstanceID() == target.GetInstanceID())
        {
            other.GetComponent<IDamageable>()?.ApplyDamage(baseDamage);
            DestroyProjectile();
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
