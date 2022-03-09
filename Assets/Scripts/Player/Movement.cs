using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public enum PlayerStates
{
    Shooter,
    HackAndSlash
}
[RequireComponent(typeof(CharacterController))]
public class Movement : MonoBehaviour
{
    
    Rigidbody m_rigidbody;
    public float m_Speed=10f;
    public float rotationSpeed = 5f;
    private Vector3 m_dir;
    bool m_IsMoving = false;
    public GameObject m_Camera;
    public GameObject m_Renderer;
    CharacterController m_CharacterController;
    float m_time = 0f;
    [Header("Jump")]
    public float m_VerticalSpeed = 0.0f;
    public float m_GravityMultiplayer = 10.0f;
    GravityController m_gravityController;
    public float m_JumpForce = 10f;
    bool m_OnGround;
    PlayerStates m_PlayerStates = PlayerStates.Shooter;
    int m_CurrentState = 0;
    bool m_attackPressed = false;
    [Header("Hack and Slash")]
    public GameObject m_LeftHand;
    public GameObject m_RightHand;
    Animator m_AnimatorLeft;
    Animator m_AnimatorRight;
    public int m_CurrentCombo = 0;
    // Start is called before the first frame update
    void Start()
    {
        m_rigidbody = GetComponent<Rigidbody>();
        m_CharacterController = GetComponent<CharacterController>();
        m_gravityController = GetComponent<GravityController>();
        m_PlayerStates = PlayerStates.Shooter;
        m_AnimatorLeft = m_LeftHand.GetComponent<Animator>();
        m_AnimatorRight = m_RightHand.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        switch (m_PlayerStates)
        {
            case (PlayerStates.Shooter):
                MovePlayer();
                RotatePlayerWithCamera();
                break;
            case (PlayerStates.HackAndSlash):
                MovePlayer();
                RotatePlayerWithCamera();
                if (m_attackPressed)
                {
                    AttackCombo();
                }
                break;
        }
        
    }
    public void MovePlayer()
    {
        Vector3 l_Movement = m_dir * m_Speed * Time.deltaTime;
        m_VerticalSpeed += Physics.gravity.y * m_GravityMultiplayer * Time.deltaTime;
        l_Movement.y = m_VerticalSpeed * Time.deltaTime ;
        CollisionFlags l_CollisionFlags = m_CharacterController.Move(l_Movement);
        if ((l_CollisionFlags & CollisionFlags.Below) != 0)//Colisiona con el suelo
        {
            m_OnGround = true;
            m_VerticalSpeed = 0.0f;
            m_time = Time.time;
        }
        else
        {
            if (Time.time - m_time > 0.3)
            {
                m_OnGround = false;
            }
        }
        if ((l_CollisionFlags & CollisionFlags.Above) != 0 && m_VerticalSpeed > 0.0f)
            m_VerticalSpeed = 0.0f;
    }
    public void RotatePlayerWithCamera()
    {
        float targetAngle = m_Camera.transform.rotation.eulerAngles.y;
        Quaternion desiredRotation = Quaternion.Euler(0, targetAngle, 0);
        transform.rotation = Quaternion.Lerp(transform.rotation, desiredRotation, rotationSpeed * Time.deltaTime);
       
    }
    public void SetGravityMultiplayer( float g)
    {
        m_GravityMultiplayer = g;
    }
    public void AttackCombo()
    {
       
        if (m_AnimatorLeft.GetCurrentAnimatorStateInfo(0).IsName("LeftHandstatic") 
            &&
            m_AnimatorRight.GetCurrentAnimatorStateInfo(0).IsName("RightHandStatic")
            )
        {
            m_CurrentCombo += 1;
            if (m_CurrentCombo % 2 != 0)
            {
                m_AnimatorLeft.SetTrigger("attack");
            }
            else
            {
                m_AnimatorRight.SetTrigger("attack");
            }
        }
        
    }
    #region inputs
    public void OnMove(InputAction.CallbackContext context)
    {
        Vector3 l_Forward = m_Camera.transform.forward;
        Vector3 l_Right = m_Camera.transform.right;
        l_Forward.y = 0.0f;
        l_Right.y = 0.0f;

        l_Forward.Normalize();
        l_Right.Normalize();
        switch (context)
        {
            case var value when !context.canceled:
            m_dir = Vector3.zero;
            if (context.ReadValue<Vector2>().x > 0)
            {
                    m_dir += l_Right;
            }
            else if (context.ReadValue<Vector2>().x < 0)
            {
                    m_dir -= l_Right;
            }

            if (context.ReadValue<Vector2>().y > 0)
                m_dir += l_Forward;
            else if (context.ReadValue<Vector2>().y < 0)
                m_dir -= l_Forward;

                m_dir.Normalize();
                m_IsMoving = true;
                    break;
            case var value when context.canceled:
                m_IsMoving = false;
                m_dir = Vector3.zero;
                break;
            
        }
    }
    public void OnJump(InputAction.CallbackContext context)
    {
        switch (context)
        {
            case var value when !context.canceled:
                if (m_OnGround)
                {
                    m_gravityController.GravityOnGround();  
                    m_VerticalSpeed = m_JumpForce;
                    m_gravityController.PressJump();
                }
                break;
            case var value when context.canceled:
                m_gravityController.RelesJump();
                break;

        }

    }
    public void OnLook(InputAction.CallbackContext context)
    {
        throw new System.NotImplementedException();
    }

    public void OnChangeMode(InputAction.CallbackContext context)
    {
        if (m_PlayerStates == 0)
        {
            m_PlayerStates = PlayerStates.HackAndSlash;
        }
        else
        {
            m_PlayerStates = PlayerStates.Shooter;
        }

        Debug.Log("mouse " + m_PlayerStates);
    }


    public void OnFire(InputAction.CallbackContext context)
    {
        switch (context)
        {
            case var value when !context.canceled:
                m_attackPressed = true;
                break;
            case var value when context.canceled:
                m_attackPressed = false;
                break;

        }
    }
    #endregion
}


