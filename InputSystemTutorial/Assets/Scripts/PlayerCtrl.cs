
# pragma warning disable IDE0051                    //함수를 선언하고 호출하지 않으면 경고가 뜨는 것을 막는 코드
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;                      //InputSystem 라이브러리 추가




public class PlayerCtrl : MonoBehaviour
{

    private Animator anim;
    private new Transform transform;    //상속받은 부모 클래스의 함수 숨기기를 방지하기 위해서?
    private Vector3 moveDir;            //입력은 2차원이지만 실제 움직이는 건 3차원


    private void Start()
    {
        anim = GetComponent<Animator>();
        transform = GetComponent<Transform>();

        
    }

    private void Update()
    {
        if (moveDir != Vector3.zero)            //움직인다면  
        {
            //입력한 키보드 진행방향으로 회전 
            transform.rotation = Quaternion.LookRotation(moveDir);
            //회전한 후 전진 방향으로 이동
            transform.Translate(Vector3.forward * Time.deltaTime * 4.0f);
        }
        
    }




    void OnMove(InputValue _value)      //On만 붙여서 호출함
    {
        Vector2 dir = _value.Get<Vector2>();            //입력은 2차원
        moveDir = new Vector3(dir.x, 0f, dir.y);        //움직이는 건 3차원
        anim.SetFloat("Movement", dir.magnitude);

        //Debug.Log($"Move = ({dir.x},{dir.y})");   //메모리 잡아먹으므로 출시 전에는 주석 또는 지워야 함

    }

    void OnAttack()                     //On만 붙여서 호출함
    {

        //Debug.Log($"Attack");

        anim.SetTrigger("Attack");




    }














}
