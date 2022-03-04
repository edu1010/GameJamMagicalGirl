using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent (typeof(CharacterController))]
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
    bool m_OnGround;
    float m_time = 0f;
    public float m_VerticalSpeed = 0.0f;
    
    // Start is called before the first frame update
    void Start()
    {
        m_rigidbody = GetComponent<Rigidbody>();
        m_CharacterController = GetComponent<CharacterController>();


    }

    // Update is called once per frame
    void Update()
    {
        MovePlayer();
        RotatePlayerWithCamera();
    }
    public void MovePlayer()
    {
        Vector3 l_Movement = m_dir * m_Speed * Time.deltaTime;
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
    }
    public void RotatePlayerWithCamera()
    {
        float targetAngle = m_Camera.transform.rotation.eulerAngles.y;
        Quaternion desiredRotation = Quaternion.EulerAngles(0, targetAngle, 0);
        transform.rotation = Quaternion.Lerp(transform.rotation, desiredRotation, rotationSpeed * Time.deltaTime);
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
                break;
            
        }
    }

    public void OnLook(InputAction.CallbackContext context)
    {
        throw new System.NotImplementedException();
    }

    public void OnFire(InputAction.CallbackContext context)
    {
        throw new System.NotImplementedException();
    }
    #endregion
}


