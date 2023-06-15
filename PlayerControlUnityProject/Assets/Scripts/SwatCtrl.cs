# pragma warning disable IDE0051                    //경고 막는 코드 : 함수 선언 후 비호출시 뜨는 경고를 막음
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class SwatCtrl : MonoBehaviour
{
    float h, v, r;                              //rotation : 마우스 회전    
    Animator animator;
    Transform _transform;
    public float moveSpeed = 5.0f;
    public float rotationSpeed = 90f;

    private Vector3 moveDirection;



    void Start()                                //(3) - (script 활성화시) 1회 호출 / coroutine 가능
    {   
        animator = GetComponent<Animator>();    //스크립트 시작되자 마자 이 스크립트가 달린 객체(플레이어)의 컴퍼넌트 중 Animation을 자동으로 저장, 만일 이 부분이 주석처리되면 직접 수동으로 넣어야함
        
        _transform = this.transform;            //tr = GetComponent<Transform>();   

    }

    void Update()
    {

        h = Input.GetAxis("Horizontal");         //A,D - Horizontal - X축
        v = Input.GetAxis("Vertical");           //W,S - Vertical - Z축
        r = Input.GetAxis("Mouse X");            //Mouse X - 좌우,   Mouse Y - 상하


        //이동
        _transform.Translate(Vector3.right * h * moveSpeed * Time.deltaTime);            
        {
            animator.SetFloat("PosX", h, 0.01f, Time.deltaTime);
        }
        _transform.Translate(Vector3.forward * v * moveSpeed * Time.deltaTime);          
        {
            animator.SetFloat("PosY", v, 0.01f, Time.deltaTime);
        }
        _transform.Rotate(Vector3.up * r * rotationSpeed * Time.deltaTime);


        //방향 전환
        if (moveDirection != Vector3.zero)                                       //움직인다면  
        {
            transform.rotation = Quaternion.LookRotation(moveDirection);         //입력한 키보드 진행방향으로 캐릭터 회전 
            transform.Translate(Vector3.forward * Time.deltaTime * 4.0f);  //(회전한 후) 전진 방향으로 이동
        }



        #region SEND_MESSAGE                                //Player Input - Send Message에 관련된 지역 

        void OnMove(InputValue _value)                      //Player Input에서 SendMessage로 On만 붙여서 호출함
        {
            Vector2 dir = _value.Get<Vector2>();            //입력은 2차원
                                                            //Debug.Log($"Move = ({dir.x},{dir.y})");       //모든 로그 기록은 메모리를 잡아먹으므로 출시 전에는 지우거나 주석으로만 남겨놔야 함

            moveDirection = new Vector3(dir.x, 0f, dir.y);        //움직이는 건 3차원
            animator.SetFloat("Movement", dir.magnitude);       //dir.magnitude(= dir.x, dir.y의 크기)만큼 이동 
        }

        void OnAttack()                                     //Player Input에서 SendMessage로 On만 붙여서 호출함
        {
            //Debug.Log($"Attack");                         

            animator.SetTrigger("Attack");
        }

        #endregion







    }
}
