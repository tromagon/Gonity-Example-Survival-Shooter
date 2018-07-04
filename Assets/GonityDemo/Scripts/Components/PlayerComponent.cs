using Gonity;
using UnityEngine;

public class PlayerComponent : ECSComponent
{
    public Vector3 movement;
    public Animator anim;
    public Rigidbody rigidBody;
    public AudioSource audioSource;
    public PlayerHealthModel healthModel;
}