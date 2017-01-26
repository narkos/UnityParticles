using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoosterGlobeHandler : MonoBehaviour
{

    public Transform m_playerTransform;
    public float m_distance = 10.0f;
    private BoxCollider m_playerCollider;

    private SphereCollider m_particleCollider;
    public ParticleSystem m_emberSystem1;
    public ParticleSystem m_emberSystem2;
    private ParticleSystem m_globeSystem;
    ParticleSystem.MainModule m_globeMainModule;

    private Color m_activeParticleColor;
    public Color m_disabledParticleColor;

    // Use this for initialization
    void Start()
    {
        if (m_playerTransform == null)
        {
            Debug.Log("Player transform not set");
        }
        
        m_particleCollider = GetComponent<SphereCollider>();
        m_playerCollider = m_playerTransform.GetComponent<BoxCollider>();
        m_globeSystem = GetComponent<ParticleSystem>();

        m_globeMainModule = m_globeSystem.main;
        m_disabledParticleColor.a = 1.0f;
        m_activeParticleColor = m_globeSystem.main.startColor.color;
        m_globeMainModule.startColor = m_disabledParticleColor;

        SetOutOfRange();
    }

    void SetOutOfRange()
    {
        m_globeMainModule.startColor = m_disabledParticleColor;
        m_emberSystem1.Stop();
        m_emberSystem2.Stop();
    }

    void SetWithinRange()
    {
        m_globeMainModule.startColor = m_activeParticleColor;
        m_emberSystem1.Play();
        m_emberSystem2.Play();
    }

    void OnTriggerEnter()
    {
        SetWithinRange();
    }

    void OnTriggerExit()
    {
        SetOutOfRange();
        m_emberSystem1.Stop();
        m_emberSystem2.Stop();
    }

    // Update is called once per frame
    void Update()
    {

    }
}
