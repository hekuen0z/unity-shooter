using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Com.Pt.SimpleHostile
{
    public class Motion : MonoBehaviour
    {
        public float speed;
        public float sprintModifier;
        public float jumpForce;
        public Camera normalCam;
        public Transform groundDetected;
        public LayerMask ground;

        private Rigidbody rig;

        private float baseFOV;
        private float sprintFov = 1.5f;

        private void Start()
        {
            baseFOV = normalCam.fieldOfView;
            rig = GetComponent<Rigidbody>();
        }

        void FixedUpdate()
        {
                float p_hmove = Input.GetAxisRaw("Horizontal");
                float p_vmove = Input.GetAxisRaw("Vertical");


                bool sprint = Input.GetKey(KeyCode.LeftShift);
                bool jump = Input.GetKeyDown(KeyCode.Space);


                bool isGrounded = Physics.Raycast(groundDetected.position, Vector3.down, 0.1f, ground);
                bool isJump = jump;
                bool isSprint = sprint && p_vmove > 0 && !isJump && isGrounded;


                if (isJump) rig.AddForce(Vector3.up * jumpForce);


                Vector3 p_direction = new Vector3(p_hmove, 0, p_vmove);
                p_direction.Normalize();

                float p_adjustedSpeed = speed;
                if (isSprint) p_adjustedSpeed *= sprintModifier;


                Vector3 p_targetVelocity = transform.TransformDirection(p_direction) * p_adjustedSpeed * Time.deltaTime;
                p_targetVelocity.y = rig.velocity.y;
                rig.velocity = p_targetVelocity;


                if (isSprint) { normalCam.fieldOfView = Mathf.Lerp(normalCam.fieldOfView, baseFOV * sprintFov, Time.deltaTime * 8f); }
                else { normalCam.fieldOfView = Mathf.Lerp(normalCam.fieldOfView, baseFOV, Time.deltaTime * 8f); }

            }
        }
    }
