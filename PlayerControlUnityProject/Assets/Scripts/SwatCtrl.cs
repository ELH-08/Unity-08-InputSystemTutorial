using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwatCtrl : MonoBehaviour
{
    float h, v, r; //rotation�� ���콺 ȸ��    
    Animator animator;
    Transform tr;
    public float moveSpeed = 5.0f;
    public float rotationSpeed = 90f;




    void Start()
    {
        animator = GetComponent<Animator>(); //�ڱ��ڽ� ���۳�Ʈ
        //tr = GetComponent<Transform>();
        tr = this.transform;

    }

    void Update()
    {

        h = Input.GetAxis("Horizontal");
        v = Input.GetAxis("Vertical");
        r = Input.GetAxis("Mouse X");   //���콺 ���ϴ� X��, ���콺 �¿�� �����̸� Y��

        tr.Translate(Vector3.right * h * moveSpeed * Time.deltaTime);  //Time.deltaTime ������ �ε巴��?
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
