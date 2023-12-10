using System.Collections;
using Unity.VisualScripting;
using UnityEditor.Timeline.Actions;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Rigidbody))]
public class Player : MonoBehaviour
{
    public bool isInventory = false;
    public bool isDead = false;
    public bool isAction = false;
    public bool isAttack = false;
    public bool isBlood = false;
    Vector2 inputVector;

    Transform cameratr;

    [Header("LockOnField")]
    [SerializeField]
    public bool IsLockOn;
    private Transform targetPos;
    Animator ani;
    [SerializeField]
    InputValue value;

    WaitForSeconds HittedDeley = new WaitForSeconds(1.5f);
    WaitForSeconds RollingDeley = new WaitForSeconds(1.3f);
    WaitForSeconds AttackDeley = new WaitForSeconds(1.1f);
    WaitForSeconds BloodingCount = new WaitForSeconds(30f);
    [SerializeField]
    public STD std;
    ParticleSystem weaponParti;
    ParticleSystem blooddrip;

    public float moveSpeed = 2f;
    public float rotSpeed = 1f;
    public float rollingSpeed = 4f;
    BoxCollider swoardColl;

    Vector3 dirc;
    public float yG;
    readonly private string MainCam = "MainCamera";
    readonly private int attack = Animator.StringToHash("attack");
    readonly private int HoriMove = Animator.StringToHash("HoriMove");
    readonly private int VertiMove = Animator.StringToHash("VertiMove");
    readonly private int isMove = Animator.StringToHash("isMove");
    readonly private int Reaction = Animator.StringToHash("Reaction");
    readonly private int rolling = Animator.StringToHash("Rolling");

    Rigidbody rb;
    [Tooltip("비워두세용 자동이랍니다")]
    public Transform enemyTr;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        cameratr = GameObject.FindWithTag("MainCamera").transform;
        rb.freezeRotation = true;
        ani = GetComponent<Animator>();
        swoardColl = GetComponentInChildren<BoxCollider>();
        swoardColl.enabled = false;
        std = GetComponent<STD>();
        weaponParti = swoardColl.GetComponentInChildren<ParticleSystem>();
        blooddrip = swoardColl.transform.GetChild(1).GetChild(0).GetComponent<ParticleSystem>();
    }


    private void OnRolling(InputAction.CallbackContext context)
    {
        if (context.action.name == "Rolling" && context.phase == InputActionPhase.Performed)
        {
            if (isAction) return;
        }
    }

    private void Update()
    {
        if (rb.velocity == Vector3.zero)
        {
            ani.SetBool(isMove, false);
        }
        if (Input.GetMouseButtonDown(0) && !isAction)
        {
            StartCoroutine(Attack());
        }
        Move(inputVector);
    }


    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("ENEMY") && !isAction)
        {
            StartCoroutine(Hitted());
            var enemystd = collision.transform.GetComponent<STD>();
            BattleBehaviour.Battle(enemystd, std);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("ENEMY"))
        {
            var enemystd = other.transform.GetComponent<STD>();
            BattleBehaviour.Battle(std, enemystd);
            weaponParti.Play();
            StartCoroutine(Blooding());
        }
    }

    void OnMove(InputValue value)
    {
        inputVector = value.Get<Vector2>();
    }
    void Move(Vector2 inputVector)
    {
        ani.SetBool(isMove, true);
        ani.SetFloat(HoriMove, inputVector.x);
        ani.SetFloat(VertiMove, inputVector.y);
        if (rb.velocity != Vector3.zero && !IsLockOn)
        {
            Vector3 vec = new Vector3(0f, cameratr.eulerAngles.y, 0f);
            Quaternion lookRotation = Quaternion.Euler(vec);
            if (!isAction)
            {
                transform.rotation = Quaternion.RotateTowards
                    (
                    transform.rotation,
                    lookRotation,
                    rotSpeed
                    );
            }
        }
        else if (IsLockOn)
        {
            Vector3 V3targetPos = enemyTr.position - transform.position;
            transform.LookAt(V3targetPos);
            V3targetPos.y = 0;
            Quaternion lookRotation =
                Quaternion.LookRotation(V3targetPos);
            if (!isAction)
            {
                transform.rotation = Quaternion.RotateTowards
                    (
                    transform.rotation,
                    lookRotation,
                    rotSpeed
                    );
            }
        }
        yG = StandingOnGround() ? 0 : -12f;
        Vector3 moveDirection = (transform.forward * inputVector.y) + (transform.right * inputVector.x) + (transform.up * yG);
        rb.velocity = moveDirection * moveSpeed;
    }

    IEnumerator Attack()
    {
        if (isAttack) yield break;
        isAttack = true;
        swoardColl.enabled = true;
        ani.SetTrigger(attack);

        yield return AttackDeley;

        swoardColl.enabled = false;
        isAttack = false;
    }

    bool StandingOnGround()
    {
        RaycastHit hit;
        float rayLength = 20.0f;
        Vector3 pos = this.transform.position;
        Vector3 cubePos = pos;

        Vector3 boxSize = new Vector3(0.5f, 0.5f, 0.5f);
        if (Physics.BoxCast(cubePos, boxSize / 2, Vector3.down, out hit, Quaternion.identity, rayLength))
        {
            if (hit.distance > 0.1f)
            {
                return false;
            }
        }
        return true;
    }


    void OnRolling(InputAction action)
    {
        Rolling(inputVector);
    }
    void Rolling(Vector2 value)
    {
        rb.velocity = Vector3.zero;
        isAction = true;
        Quaternion delQuat = transform.rotation;
        transform.position = transform.position;
        Vector3 inputPos = new Vector3(inputVector.x, 0, inputVector.y);
        dirc = transform.position + transform.rotation * inputPos;
        transform.LookAt(dirc);
        ani.SetTrigger(rolling);
        rb.AddForce(transform.forward * rollingSpeed, ForceMode.Impulse);
        
        rb.velocity = Vector3.zero;
        transform.rotation = delQuat;
        isAction = false;
    }

    IEnumerator Hitted()
    {
        isAction = true;
        rb.velocity = Vector3.zero;
        ani.Play(Reaction);

        yield return HittedDeley;
        isAction = false;
    }

    IEnumerator Blooding()
    {
        if (isBlood) yield break;
        isBlood = true;
        blooddrip.Play();

        yield return BloodingCount;

        blooddrip.Stop();
        isBlood = false;
    }
}