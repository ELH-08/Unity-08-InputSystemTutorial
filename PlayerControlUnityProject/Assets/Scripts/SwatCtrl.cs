using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwatCtrl : MonoBehaviour
{
    float h, v, r; //rotation은 마우스 회전    
    Animator animator;
    Transform tr;
    public float moveSpeed = 5.0f;
    public float rotationSpeed = 90f;




    void Start()
    {
        animator = GetComponent<Animator>(); //자기자신 컴퍼넌트
        //tr = GetComponent<Transform>();
        tr = this.transform;

    }

    void Update()
    {

        h = Input.GetAxis("Horizontal");
        v = Input.GetAxis("Vertical");
        r = Input.GetAxis("Mouse X");   //마우스 상하는 X축, 마우스 좌우로 움직이면 Y축

        tr.Translate(Vector3.right * h * moveSpeed * Time.deltaTime);  //Time.deltaTime 프레임 부드럽게?
        {
            animator.SetFloat("PosX", h, 0.01f, Time.deltaTime);
        }
        tr.Translate(Vector3.forward * v * moveSpeed * Time.deltaTime);   
        {
            animator.SetFloat("PosY", v, 0.01f, Time.deltaTime);
        }
        tr.Rotate(Vector3.up * r * rotationSpeed * Time.deltaTime);



    }
}
