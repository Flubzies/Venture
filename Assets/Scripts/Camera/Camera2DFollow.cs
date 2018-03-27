using System;
using System.Collections;
using UnityEngine;

// From Unity 2D Standard Assets Pack.
// Added - Look ahead factor now works on Y axis as well. 
// Added - Player search functionality. (Brackeys)
// But I changed it into an Enumerator.
namespace UnityStandardAssets._2D
{
    public class Camera2DFollow : MonoBehaviour
    {
        public Transform target;
        public float damping = 1;
        public float lookAheadFactor = 3;
        public float lookAheadReturnSpeed = 0.5f;
        public float lookAheadMoveThreshold = 0.1f;

        private Vector3 m_LastTargetPosition;
        private Vector3 m_CurrentVelocity;
        private Vector3 m_LookAheadPos;

        public float _nextTimeToSearch = 1.0f;
        bool setupComplete;

        // Use this for initialization
        private void Start ()
        {
            setupComplete = false;
            if (target == null) StartCoroutine (FindPlayer ());
        }

        private void Update ()
        {
            if (target == null) StartCoroutine (FindPlayer ());
            else
            {
                float xMoveDelta = (target.position - m_LastTargetPosition).x;
                float yMoveDelta = (target.position - m_LastTargetPosition).y;

                bool updateLookAheadTargetX = Mathf.Abs (xMoveDelta) > lookAheadMoveThreshold;
                bool updateLookAheadTargetY = Mathf.Abs (yMoveDelta) > lookAheadMoveThreshold;

                if (updateLookAheadTargetX || updateLookAheadTargetY && transform != null)
                {
                    m_LookAheadPos = (Vector2) target.transform.right * lookAheadFactor;
                }
                else
                    m_LookAheadPos = Vector3.MoveTowards (m_LookAheadPos, Vector3.zero, Time.deltaTime * lookAheadReturnSpeed);

                Vector3 aheadTargetPos = target.position + m_LookAheadPos + Vector3.forward * -10;
                Vector3 newPos = Vector3.SmoothDamp (transform.position, aheadTargetPos, ref m_CurrentVelocity, damping);

                transform.position = newPos;
                m_LastTargetPosition = target.position;
            }
        }

        IEnumerator FindPlayer ()
        {
            while (target == null)
            {
                GameObject searchResult = GameObject.FindGameObjectWithTag ("Player");
                if (searchResult != null) target = searchResult.transform;
                yield return new WaitForSeconds (_nextTimeToSearch);
            }
            if (!setupComplete)
            {
                m_LastTargetPosition = target.position;
                transform.parent = null;
                setupComplete = true;
            }
        }
    }
}