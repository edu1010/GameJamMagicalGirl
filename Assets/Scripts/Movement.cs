using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;


public class Movement : MonoBehaviour
{
    
    Rigidbody m_rigidbody;
    public float m_force=1;
    private Vector3 movement;
    bool m_IsMoving = false;
    public GameObject m_Camera;
    public GameObject m_Renderer;


    // Start is called before the first frame update
    void Start()
    {
        m_rigidbody = GetComponent<Rigidbody>();
        
    }

    // Update is called once per frame
    void Update()
    {
        if (m_IsMoving)
        {
            m_rigidbody.AddForce(movement * m_force, ForceMode.Force);
        }
    }
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
            movement = Vector3.zero;
            if (context.ReadValue<Vector2>().x > 0)
            {
                    movement += l_Right;
            }
            else if (context.ReadValue<Vector2>().x < 0)
            {
                    movement -= l_Right;
            }

            if (context.ReadValue<Vector2>().y > 0)
                movement += l_Forward;
            else if (context.ReadValue<Vector2>().y < 0)
                movement -= l_Forward;

                movement.Normalize();
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
}


