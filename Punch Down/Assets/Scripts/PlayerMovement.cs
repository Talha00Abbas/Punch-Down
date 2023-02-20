using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{

    PlayerControls controls;
    PlayerControls.PlayerActions actions;
    private Vector2 move;

    public Rigidbody rb;
    public Collider coll;
    public GameObject bullet;
    public GameObject puncher;
    public Transform firePoint;
    public enum Abilities { Fade, Hyper, RapidFire, PunchDown };

    [Header("Stats")]
    public float moveSpeed = 11f;
    public float maxForce = 1;  //max Move Speed
    public float steerPower = 3f;
    public int maxHealth = 5;
    public int health;
    

    [Header("Bullet Quality")]
    public float bulletTime;
    public float startTime = 4f;

    [Header("Abilities")]
    public Abilities abilities;
    public float abilityShutdownTime = 3f;
    public float abilityTime;
    public float abilityStartTime = 10f;

    public void OnMove(InputAction.CallbackContext context)
    {
        move = context.ReadValue<Vector2>();
    }

    private void Awake()
    {

        controls = new PlayerControls();
        actions = controls.Player;
        health = maxHealth;

        actions.Fire.started += _ => StartFiring();
        actions.Fire.canceled += _ => StopFiring();
        actions.Ability.started += _ => StartAbility();

    }
    private void Start()
    {
        bulletTime = startTime;
        abilityTime = abilityStartTime;
    }
    private void Update()
    {
        bulletTime -= Time.deltaTime;
        abilityTime -= Time.deltaTime;
        if (health <= 0)
        {
            Destroy(this.gameObject);
        }
    }
    private void FixedUpdate()
    {
        Move();
    }

    void StartAbility() 
    {
        if (abilityTime <= 0)
        {
            AbilitySystem(abilities);
            abilityTime = abilityStartTime;
        }
    }

    void AbilitySystem(Abilities abi) 
    {
        if (abi == Abilities.Fade)
        {
            rb.useGravity = false;
            coll.isTrigger = true;
            Debug.Log("Ability Activated");
            Invoke("N_Fade", abilityShutdownTime);
        }

        if (abi == Abilities.Hyper)
        {
            moveSpeed = 12f;
            Debug.Log("Ability Activated");
            Invoke("N_Hyper", abilityShutdownTime);
        }

        if (abi == Abilities.RapidFire)
        {
            startTime = 0.5f;
            Debug.Log("Ability Activated");
            Invoke("N_Rapid", abilityShutdownTime);
        }

        if (abi == Abilities.PunchDown)
        {
            Debug.Log("Ability Activated");
            Instantiate(puncher, transform.position, firePoint.rotation);
        }
        
    }

    void N_Fade() 
    {
        rb.useGravity = true;
        coll.isTrigger = false;
    }

    void N_Hyper() 
    {
        moveSpeed = 6f;
    }

    void N_Rapid() 
    {
        startTime = 4f;
    }


    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Bullet")
        {
            health -= 1;
            Destroy(collision.gameObject);
        }
    }

    void Move() 
    {
        Vector3 currentVelocity = rb.velocity;
        Vector3 targetVelocity = new Vector3(0, 0, move.y);

        transform.Rotate(Vector3.up * move.x * steerPower);

        targetVelocity *= moveSpeed;

        targetVelocity = transform.TransformDirection(targetVelocity);

        Vector3 velocityChange = (targetVelocity - currentVelocity);
        velocityChange = new Vector3(velocityChange.x, 0, velocityChange.z);

        //Limit force
        Vector3.ClampMagnitude(velocityChange, maxForce);
        rb.AddForce(velocityChange, ForceMode.VelocityChange);
    }
    void StartFiring()
    {
        if (bulletTime <= 0)
        {
            Instantiate(bullet, firePoint.position, firePoint.rotation);
            bulletTime = startTime;
        }
    }
    void StopFiring()
    {
        Debug.Log("Firing Stopped");
    }
    private void OnEnable()
    {
        controls.Enable();
    }

    private void OnDestroy()
    {
        controls.Disable();
    }

}
