using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GravityController : MonoBehaviour
{
    [Header("General settings")]
    public float m_MaxGravityMultiplayer = 10f;
    public float m_MinGravityMultiplayer = 10f;
    [Header("Jump settings")]
    public float m_GravityLongJump = 5f;
    public float m_GravityShortJump = 10f;
    private PlayerController m_movement;
    [Tooltip("Tiempo manteniendo el boton para que siga siendo long jump")]
    public float m_TimePreesLong = 2f;
    public float m_CurrentTimeInAir = 0f;
    bool m_IsJumping = false;
    private void Awake()
    {
        m_movement = GetComponent<PlayerController>();
    }
    private void Update()
    {
        if (m_IsJumping)
        {
            m_CurrentTimeInAir += Time.deltaTime;
        }
    }
    public void PressJump()
    {
        m_IsJumping = true;
        m_CurrentTimeInAir = 0f;
        GravityForLongJump();
    }
    public void RelesJump()
    {
        if(m_IsJumping && m_CurrentTimeInAir < m_TimePreesLong)
        {
            GravityForShortJump();
        }
    }


    public void GravityForLongJump()
    {
        m_movement.SetGravityMultiplayer(m_GravityLongJump);

    }
    public void GravityForShortJump()
    {
        m_movement.SetGravityMultiplayer(m_GravityShortJump);
    }
    public void AboveAirPlatform()
    {
        m_movement.SetGravityMultiplayer(m_MinGravityMultiplayer);
    }
    public void GravityOnGround()
    {
        m_movement.SetGravityMultiplayer(m_MaxGravityMultiplayer);
        m_IsJumping = false;
    }
}
