using Gonity;
using UnityEngine;
using UnityEngine.AI;

public class EnemyComponent : ECSComponent
{
    public NavMeshAgent nav;
    public Animator anim;
    public AudioSource audioSource;
    public ParticleSystem hitParticles;
    public CapsuleCollider capsuleCollider;
    public Rigidbody rigidBody;
    public EnemyDO enemyDO;
    public TriggerBehaviour trigger;
    public float attackTimer;
    public int currentHealth;
}