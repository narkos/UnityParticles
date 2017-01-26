using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(ParticleSystem))]
public class ParticleSeek : MonoBehaviour
{
    public Transform    m_target;
    public float        m_force = 10.0f;

    ParticleSystem m_particleSystem;
    ParticleSystem.Particle[] particles;

    ParticleSystem.MainModule m_psMainModule;
    // Use this for initialization
    void Start()
    {
        m_particleSystem = GetComponent<ParticleSystem>();
        m_psMainModule = m_particleSystem.main;
    }

    // Update is called once per frame
    void LateUpdate()
    {
        int maxParticles = m_psMainModule.maxParticles;
        if(particles == null || particles.Length < maxParticles)
        {
            particles = new ParticleSystem.Particle[maxParticles];
            //m_particleSystem.GetParticles(particles);
        }
        //ParticleSystem.Particle[] particles = new ParticleSystem.Particle[m_particleSystem.particleCount];
        m_particleSystem.GetParticles(particles);

        float forceDeltaTime = Time.deltaTime * m_force;

        Vector3 targetTransformPosition;
        switch(m_psMainModule.simulationSpace)
        {
            case ParticleSystemSimulationSpace.Local:
                {
                    targetTransformPosition = transform.InverseTransformPoint(m_target.position);
                    break;
                }
            case ParticleSystemSimulationSpace.Custom:
                {
                    targetTransformPosition = m_psMainModule.customSimulationSpace.InverseTransformPoint(m_target.position);
                    break;
                }
            case ParticleSystemSimulationSpace.World:
                {
                    targetTransformPosition = m_target.position;
                    break;
                }
            default:
                {
                    throw new System.NotSupportedException(
                        string.Format("Unsupported simulation space: '{0}'.",
                        System.Enum.GetName(typeof(ParticleSystemSimulationSpace), m_psMainModule.simulationSpace)));
                }
                
            
        }
        for (int i = 0; i < particles.Length; i++)
        {


            

            Vector3 directionTarget = Vector3.Normalize(targetTransformPosition - particles[i].position);

            Vector3 seekForce = directionTarget * forceDeltaTime;

            particles[i].velocity += seekForce;

        }

        m_particleSystem.SetParticles(particles, particles.Length);
    }
}
