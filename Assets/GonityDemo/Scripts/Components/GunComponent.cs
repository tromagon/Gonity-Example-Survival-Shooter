using Gonity;
using UnityEngine;

public class GunComponent : ECSComponent
{
    public float timer;
    public Ray shootRay = new Ray();
    public RaycastHit shootHit;
    public int shootableMask;
    public ParticleSystem gunParticles;
    public LineRenderer gunLine;
    public AudioSource gunAudio;
    public Light gunLight;
    public Light faceLight;
}