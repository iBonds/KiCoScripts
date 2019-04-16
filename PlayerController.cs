using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class PlayerController : MonoBehaviour, IDamageable
{
    public float runningMultipler = 15;
    public float jumpHeight = 1f;
    public bool toJump;
    public bool isRunning;
    public bool isWalking;
    //public Controller kitten;
    private Vector3 dirVector;
    public int health = 3;
    public float timeLastHit = 0f;
    public bool is_dead = false;
    public FlashCanvas cv;

    private Vector3 local_scale;
    private Rigidbody rb;
    private BoxCollider boxCollider;
    private float distanceToGround;
    private const float gravity = 9.8f;
    private Animator animator;
    private int idle_hash = Animator.StringToHash("Idle");
    private int walk_hash = Animator.StringToHash("Walk");
    private int jump_hash = Animator.StringToHash("Jump");
    private bool is_grounded = false;


    private UnityEngine.UI.Slider slider;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        boxCollider = GetComponent<BoxCollider>();
        distanceToGround = boxCollider.size.y + 0.3f;
        local_scale = transform.localScale;
        //kitten = GameObject.FindGameObjectWithTag("kitten").GetComponent<Controller>();
        cv = GameObject.Find("Flash").GetComponent<FlashCanvas>();
        slider = GameObject.Find("HealthSlider").GetComponent<UnityEngine.UI.Slider>();
        slider.value = health;
        slider.onValueChanged.AddListener(delegate { sliderChange(); });
        animator = GetComponentInChildren<Animator>();
    }

    bool IsGrounded()
    {
        return Physics.BoxCast(new Vector3(transform.position.x, transform.position.y, transform.position.z),
                            new Vector3((boxCollider.size.x * 0.9f), 0.3f, (boxCollider.size.z * 0.9f)) * 0.5f,
                            -Vector3.up,
                            boxCollider.transform.rotation,
                            distanceToGround
                            );
    }

    void FixedUpdate()
    {
        //dirVector = new Vector3(-1 * Input.GetAxis("Horizontal"), 0, -1 * Input.GetAxis("Vertical")).normalized;
        dirVector = Input.GetAxis("Vertical") * (transform.rotation * Vector3.forward); // Why doesn't dirVector*= transform.rotation work?

        rb.MovePosition(transform.position + dirVector * Time.deltaTime * (isRunning ? runningMultipler : 5));

            Vector3 rotVector = new Vector3(0, Input.GetAxis("Horizontal"), 0);
        if(rotVector != Vector3.zero)
            transform.Rotate(rotVector);

        if (toJump)// && !kitten.is_picked_up
            {
            rb.AddForce(transform.up * Mathf.Sqrt(2f * jumpHeight * gravity), ForceMode.VelocityChange);
            toJump = false;
        }
    }

    void Update()
    {
        if (transform.position.y < -20f)
            is_dead = true;
        if(health <= 0) {
            onDeath();
        }
        isWalking = (Input.GetAxis("Vertical") != 0);
        toJump = (Input.GetKeyDown("space") && IsGrounded()) || toJump;
        isRunning = Input.GetKey(KeyCode.LeftShift);
        updateAnimation();
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Dog"))
            doDamage();
    }
    void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Dog"))
            doDamage();
    }

    public void doDamage() {
        if (Time.fixedTime - timeLastHit > 5f || timeLastHit == 0)
        {
            cv.playerDamaged();
            health--;
            timeLastHit = Time.fixedTime;
            slider.value = health;
        }
    }

    public void onDeath() {
        //cv.playerDamaged();
        //Application.LoadLevel("Start Menu");
        is_dead = true;
    }

    private void sliderChange()
    {
        GameObject slider_bar = slider.transform.GetChild(1).GetChild(0).gameObject;
        GameObject slider_icon = slider.transform.GetChild(2).GetChild(0).gameObject;
        Image bar_image = slider_bar.GetComponent<Image>();
        Image icon_image = slider_icon.GetComponent<Image>();
        Color color;

        if (slider.value == 3) // green
        {
            color = Color.green;
        }
        else if (slider.value == 2) // yellow
        {
            color = Color.yellow;
        }
        else if (slider.value == 1) // red
        {
            color = Color.red;
        }
        else // black
        {
            color = Color.black;
        }

        bar_image.color = color;
        icon_image.color = color;
    }

    void updateAnimation()
    {
        if (toJump || !IsGrounded())
        {
            animator.Play(jump_hash);
        } else if (isWalking)
        {
            animator.Play(walk_hash);
            if (isRunning)
            {
                animator.SetFloat("Speed", 2);
            }
            else
            {
                animator.SetFloat("Speed", 1);
            }
        } else
        {
            animator.Play(idle_hash);
        }
    }
}
