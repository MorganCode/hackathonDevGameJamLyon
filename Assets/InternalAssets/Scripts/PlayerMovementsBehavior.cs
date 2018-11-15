using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class PlayerMovementsBehavior : MonoBehaviour
{
    public float speed;
    public float dashSpeed;
    public float startDashTime;
    public float dashCooldownTime;
    public bool IsStunned;
    public bool IsAttacking;
    public AudioClip dashSfx;
    
    private Rigidbody rb;
    private float dashTime;
    private bool IsDashing;
    private float dashCooldown;
    private bool dashAllowed;
    private PlayerIdDistributor pid;
    private int innerWallLayerId;
    private int pnjLayerId;
    private int playerLayerId;
    private bool PLAYERMOVEMENT_DEBUG = false;
    private AudioSource audioSource;

    // Use this for initialization
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        dashTime = startDashTime;
        IsDashing = false;
        rb.velocity = Vector3.zero;
        innerWallLayerId = LayerMask.NameToLayer("InnerWalls");
        playerLayerId = LayerMask.NameToLayer("Players");
        pnjLayerId = LayerMask.NameToLayer("PNJ");
        IgnoreInnerWallsCollision(false);
        Physics.IgnoreLayerCollision(pnjLayerId, playerLayerId, true);
        dashCooldown = 0;
        dashAllowed = true;
        IsStunned = false;
        pid = GetComponent<PlayerIdDistributor>();
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (PLAYERMOVEMENT_DEBUG)
        {
            //Debug.Log("DashRequested : " + IsDashing);
            //Debug.Log("dashTime : " + dashTime);
            //Debug.Log("dashCooldown : " + dashCooldown);
            //Debug.Log("pid.PlayerId : " + pid.PlayerId);
            //if (InputsManager.playerInputsDictionary[pid.PlayerId] == null)
            //    Debug.Log("InputTable not available for player " + pid.PlayerId);
        }

        if (!dashAllowed) // KEEP IT FIRST TO DECREASE TIMER EVEN WHEN STUNNED / ATTACKING
        {
            dashCooldown -= Time.fixedDeltaTime;
            if (dashCooldown <= 0)
            {
                dashAllowed = true;
                dashCooldown = dashCooldownTime;
            }
        }

        if (IsStunned || IsAttacking)
        {
            rb.velocity = Vector3.zero;
            return;
        }

        if (InputsManager.playerInputsDictionary[pid.PlayerId].DashPressed && dashTime == startDashTime && dashAllowed)
        {
            IsDashing = true;
            audioSource.clip = dashSfx;
            audioSource.Play();
        }

        if (!IsDashing)
        {
            rb.velocity = Vector3.zero;
            rb.drag = 0.0f;

            // Basic Movements
            rb.velocity = (Vector3.forward * speed * InputsManager.playerInputsDictionary[pid.PlayerId].LeftAnalogForwardAxis
                         + Vector3.right * speed * InputsManager.playerInputsDictionary[pid.PlayerId].LeftAnalogStrafeAxis) * Time.fixedDeltaTime;

        }
        else if (IsDashing)
        {
            if (dashTime == startDashTime) // Give impulse
            {
                IgnoreInnerWallsCollision(true);

                rb.velocity = ((Vector3.forward * InputsManager.playerInputsDictionary[pid.PlayerId].LeftAnalogForwardAxis
                                     + Vector3.right * InputsManager.playerInputsDictionary[pid.PlayerId].LeftAnalogStrafeAxis).normalized * dashSpeed
                                     * Time.fixedDeltaTime);
                dashAllowed = false;
            }
            else if (dashTime <= 0) // Dash is Over
            {
                IsDashing = false;
                IgnoreInnerWallsCollision(false);
                dashTime = startDashTime;
                dashCooldown = dashCooldownTime;
                return;
            }

            dashTime -= Time.fixedDeltaTime;
        }

        // Face correct direction
        //transform.forward = (transform.position + rb.velocity);
        //transform.LookAt(transform.Find("Head").gameObject.GetComponent<Rigidbody>().+ rb.velocity);
    }

    public void StunForSeconds(float seconds)
    {
        IsStunned = true;
        Invoke("UnStun", seconds);
    }

    private void UnStun()
    {
        IsStunned = false;
    }

    private void IgnoreInnerWallsCollision(bool b)
    {
        if (PLAYERMOVEMENT_DEBUG)
            Debug.Log("Turning " + (b ? "On" : "Off") + "innerWalls collisions");
        Physics.IgnoreLayerCollision(innerWallLayerId, playerLayerId, b);
    }
}

