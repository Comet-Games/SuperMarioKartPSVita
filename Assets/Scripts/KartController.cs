using System.Collections;
using UnityEngine;

public class KartController : MonoBehaviour
{

    [SerializeField] float driftingf, groundCheckHeight;
    [SerializeField] Rigidbody sphere;
    [SerializeField] Animator spriteVisual;
    [SerializeField] AudioSource audioSource;

    [Header("Kart Stuff")]
    [SerializeField] private float speed, currentSpeed;
    private float rotate, currentRotation;
    private int driftDir;
    public float driftPower;
    public int driftMode = 0;
    public int jumpAmount;
    public bool first, second, third;
    public Color c;

    public float driftTimer;
    public float requiredDriftTime = 3f;
    [Header("Sounds")]
    public AudioClip idleEngine;
    public AudioClip engine1;
    public AudioClip engine2;

    [Header("Bools")]
    public bool drifting;

    [Header("Parameters")]
    public float acceleration;
    public float steering;
    public float gravity;
    public float boost;

    [Header("Testing")]
    public bool testing;
    public bool grounded;

    void Awake()
    {
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        audioSource.Play();
        DriftCheck();
        transform.position = sphere.transform.position;//Follow sphere
        #region - Acceleration -
        //Acceleration
        if (Input.GetButton("Fire1") || (Input.GetButton("Cross")) && grounded)
        {
            speed = acceleration;
        }
        #endregion

        #region - Steering -
        //Steering
        if (testing == false)
        {
            if ((Input.GetAxis("Left Stick Horizontal") != 0.2f))
            {
                int dir = Input.GetAxis("Left Stick Horizontal") > 0f ? 1 : -1;
                float amount = Mathf.Abs(Input.GetAxis("Left Stick Horizontal"));
                Steer(dir, amount);
            }
        }
        if (testing == true)
        {
            if ((Input.GetAxis("Horizontal") != 0f))
            {
                int dir = Input.GetAxis("Horizontal") > 0f ? 1 : -1;
                float amount = Mathf.Abs(Input.GetAxis("Horizontal"));
                Steer(dir, amount);
            }
        }

        Visuals();

        currentSpeed = Mathf.SmoothStep(currentSpeed, speed, 12f * Time.deltaTime);
        speed = 0f;
        currentRotation = Mathf.Lerp(currentRotation, rotate, 4f * Time.deltaTime);
        rotate = 0f;
        #endregion

        #region - Drifting -
        if ((Input.GetButton("Right Shoulder") || Input.GetKey(KeyCode.UpArrow)) && !drifting && ((Input.GetAxis("Left Stick Horizontal") != 0) || (Input.GetAxis("Horizontal")) != 0))
        {
            drifting = true;
            sphere.AddForce(Vector3.up * jumpAmount, ForceMode.Impulse);
            if (testing)
            {
                driftDir = Input.GetAxis("Horizontal") > 0 ? 1 : -1;
            }
            if (!testing)
            {
                driftDir = Input.GetAxis("Left Stick Horizontal") > 0 ? 1 : -1;
            }
        }
        else if((Input.GetButtonDown("Right Shoulder") || Input.GetKeyDown(KeyCode.UpArrow)) && !drifting)
        {
            sphere.AddForce(Vector3.up * jumpAmount, ForceMode.Impulse);
        }

        if (drifting)
        {
            float control;
            float powerControl;
            if (testing)
            {
                control = (driftDir == 1) ? ExtensionMethods.Remap(Input.GetAxis("Horizontal"), -1, 1, 0, 2) : ExtensionMethods.Remap(Input.GetAxis("Horizontal"), -1, 1, 2, 0);
                powerControl = (driftDir == 1) ? ExtensionMethods.Remap(Input.GetAxis("Horizontal"), -1, 1, .2f, 1) : ExtensionMethods.Remap(Input.GetAxis("Horizontal"), -1, 1, 1, .2f);
                Steer(driftDir, control);
                driftPower += powerControl;
            }
            if (!testing)
            {
                control = (driftDir == 1) ? ExtensionMethods.Remap(Input.GetAxis("Left Stick Horizontal"), -1, 1, 0, 2) : ExtensionMethods.Remap(Input.GetAxis("Left StickHorizontal"), -1, 1, 2, 0);
                powerControl = (driftDir == 1) ? ExtensionMethods.Remap(Input.GetAxis("Left Stick Horizontal"), -1, 1, .2f, 1) : ExtensionMethods.Remap(Input.GetAxis("Left Stick Horizontal"), -1, 1, 1, .2f);
                Steer(driftDir, control);
                driftPower += powerControl;
            }

        }

        if (Input.GetButtonUp("Right Shoulder") || Input.GetKeyUp(KeyCode.UpArrow) && drifting && grounded)
        {
            if (driftTimer > requiredDriftTime)
            {
                StartBoost(0, boost);
            }
            driftTimer = 0;
            drifting = false;// (Boosting goes here)
        }
        if (Input.GetButtonUp("Right Shoulder") || Input.GetKeyUp(KeyCode.UpArrow) && drifting && !grounded)
        {
            if (driftTimer > requiredDriftTime)
            {
                StartBoost(0, boost);
            }
            driftTimer = 0;
            drifting = false;// (Boosting goes here)
        }
        #endregion

        if (!grounded)
        {
            //gravity = 1;
            //acceleration = 1;
        }
        if (grounded)
        {
            //gravity = 20;
            acceleration = 15;
        }
    }

    private void FixedUpdate()
    {
        GroundCheck();

        sphere.AddForce(transform.forward * currentSpeed, ForceMode.Acceleration);

        sphere.AddForce(Vector3.down * gravity, ForceMode.Acceleration);

        transform.eulerAngles = Vector3.Lerp(transform.eulerAngles, new Vector3(0f, transform.eulerAngles.y + currentRotation, 0f), 5f * Time.deltaTime);
    }

    void DriftCheck()
    {
        if(drifting)
        {
            driftTimer += Time.deltaTime;
        }
    }

    void Steer(int dir, float amount)
    {
        rotate = (steering * dir) * amount;
    }

    void GroundCheck()
    {
        if (Physics.Raycast(sphere.transform.position, Vector3.down, groundCheckHeight))
        {
            grounded = true;
        }
        else
        {
            grounded = false;
        }
        Debug.DrawRay(sphere.transform.position, Vector3.down * groundCheckHeight, Color.green);
    }

    public void StartBoost(float amount, float boost)
    {
        StartCoroutine(Boost(amount, boost));
    }

    public IEnumerator Boost(float time, float boost)
    {
        drifting = false;
        acceleration += boost;
        yield return new WaitForSeconds(time);
        acceleration = Mathf.Lerp(acceleration, acceleration - boost, Time.deltaTime);
    }

    void Visuals()
    {
        if (testing)
        {
            spriteVisual.SetFloat("Horizontal", Input.GetAxis("Horizontal"));
        }
        if (!testing)
        {
            spriteVisual.SetFloat("Horizontal", Input.GetAxis("Left Stick Horizontal"));
        }
    }

}
