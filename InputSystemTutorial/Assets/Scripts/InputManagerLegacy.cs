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
        tr = GetComponent<Transform>();            
        animator = GetComponent<Animator>();        


    }

    void Update()
    {
        // Input Manager (구 버전) VS Input System (신 버전)
        //1. 구 버전 - 업데이트에서 계속 확인해야해서 메모리 낭비.
        //   신 버전 - 업데이트에서 확인하지 않고 1번만 확인한다.
        //2. 구 버전 - PC, Mobile을 일일이 따로 설정.
        //   신 버전 - PC, Mobile 구분없이 따로 설정하지 않아도 됨 

        h = Input.GetAxis("Horizontal");   
        v = Input.GetAxis("Vertical");





        //분리해서 이동 (WSAD)
        //tr.Translate(Vector3.forward * v * Time.deltaTime * 5f);
        //tr.Translate(Vector3.right * h * Time.deltaTime * 5f);


        //합쳐서 이동 (WSAD)
        Vector3 movedir = (h * Vector3.right) + (v * Vector3.forward);
        tr.Translate(movedir.normalized * Time.deltaTime * 5.0f);


        animator.SetFloat("Movement", movedir.magnitude);   //magnitude 눌렀던 세기 값, 크기, 깊게 누르거나 짧게 누르거나 


        //방향전환
        if (movedir != Vector3.zero)            //움직인다면  
        {
            tr.rotation = Quaternion.LookRotation(movedir);                    //입력한 키보드 진행방향으로 캐릭터 회전 
            //tr.Translate(Vector3.forward * Time.deltaTime * 4.0f);           //(회전한 후) 전진 방향으로 이동
            tr.Translate(Vector3.forward * Time.deltaTime * 5.0f);

            if (Input.GetKeyDown(KeyCode.Space))                               //스페이스 키 입력시
            {
                animator.SetTrigger("Attack");                                 //공격 애니메이션 호출
            }

        }






    }


}
