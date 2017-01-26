using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace ParticleSystems
{

    [ExecuteInEditMode]
    [System.Serializable]
    public class MouseFollow : MonoBehaviour
    {
        public float m_speed = 8.0f;
        public float m_distanceFromCamera = 5.0f;
        // Use this for initialization
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {
            Vector3 mousePosition = Input.mousePosition;
            mousePosition.z = m_distanceFromCamera;

            Vector3 mouseScreenToWorld = Camera.main.ScreenToWorldPoint(mousePosition);
            Vector3 position = Vector3.Lerp(transform.position, mouseScreenToWorld, 1.0f - Mathf.Exp(-m_speed * Time.deltaTime));

            transform.position = position;
        }
    }

}

