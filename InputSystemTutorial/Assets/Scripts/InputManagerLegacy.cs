using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManagerLegacy : MonoBehaviour
{


    float h = 0f;
    float v = 0f;
    Transform tr;
    Animator animator;


    void Start()
    {
        tr = GetComponent<Transform>();             //���������ϴϱ�
        animator = GetComponent<Animator>();        //�ִϸ����� ����ؾ��ϴϱ� 


    }

    void Update()
    {
        //������ ��ǲ�Ŵ��� VS �Ź��� ��ǲ�ý���
        //1. ������ - ������Ʈ���� ��� Ȯ���ϴϱ� �޸� ������.
        //   �Ź��� - ������Ʈ���� ������ Ȯ������ �ʰ� 1���� Ȯ���Ѵ�.
        //2. ������ - PC, Mob ��ile ������ ���� ����.
        //   �Ź��� - PC, Mobile ���о��� ���� �������� �ʾƵ� �� 
        h = Input.GetAxis("Horizontal");   
        v = Input.GetAxis("Vertical");



        //�и��ؼ� �̵� (WSAD)
        //tr.Translate(Vector3.forward * v * Time.deltaTime * 5f);
        //tr.Translate(Vector3.right * h * Time.deltaTime * 5f);
       

        //���ļ� �̵� (WSAD)
        Vector3 movedir = (h * Vector3.right) + (v * Vector3.forward);
        tr.Translate(movedir.normalized * Time.deltaTime * 5.0f);


        animator.SetFloat("Movement", movedir.magnitude);   //magnitude ������ ���� ��, ũ��, ��� �����ų� ª�� �����ų� 

        
        //
        if (movedir != Vector3.zero)            //�����δٸ�  
        {
            tr.rotation = Quaternion.LookRotation(movedir);                    //�Է��� Ű���� ����������� ȸ��
            //tr.Translate(Vector3.forward * Time.deltaTime * 4.0f);             //ȸ���� �� ���� �������� �̵�
            tr.Translate(Vector3.forward * Time.deltaTime * 5.0f);

            if (Input.GetKeyDown(KeyCode.Space)) // �����̽� Ű �Է½�
            {
                animator.SetTrigger("Attack");
                
            }



        }




    }


}
