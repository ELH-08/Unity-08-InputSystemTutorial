
# pragma warning disable IDE0051                    //경고 막는 코드 : 함수 선언 후 비호출시 뜨는 경고를 막음
using System.Collections;
using System.Collections.Generic;                   //비활성화 : 사용되지 않은 라이브러리
using UnityEngine;                                  //활성화 : 사용된 라이브러리
using UnityEngine.InputSystem;                      //InputSystem 라이브러리 호출



public class PlayerCtrlByEvent : MonoBehaviour
{
    //비활성화
    //private InputAction moveAction;

    private InputAction moveAction;
    private InputAction attackAction;
    private Animator anim;
    private Vector3 moveDir;



    void Start()    //1회 호출
    {
        anim = GetComponent<Animator>();
        
        //Move Action 생성 및 타입 설정
        moveAction = new InputAction("Move", InputActionType.Value);

        
        /*Main Action에서 일일이 수작업 하지 않고 대신 스크립트로 지정*/

        //Move Action의 복합 바인딩 정보 정의
        moveAction.AddCompositeBinding("2DVector")         //Move Action - Player Actions - Move - WASD - composite type - 2D Vector
            .With("Up", "<Keyboard>/w")
            .With("Down", "<Keyboard>/s")
            .With("left", "<Keyboard>/a")
            .With("right", "<Keyboard>/d");

        //Move Action의 performed canceled 이벤트 연결
        moveAction.performed += ctx => 
        {
            Vector2 dir = ctx.ReadValue<Vector2>();
            moveDir = new Vector3(dir.x, 0f, dir.y);        
            anim.SetFloat("Movement", dir.magnitude);

        };

        //Move Action의 performed canceled 이벤트 연결
        moveAction.canceled += ctx =>
        {
            moveDir = Vector3.zero;                     //움직임이 없음
            anim.SetFloat("Movement", 0f);

        };

        moveAction.Enable();                                                                     //Move 액션 활성화
        attackAction = new InputAction("Attack", InputActionType.Button, "<Keyboard>/space");    //Attack 액션의 활성화    (Tip : 매개변수에 ,를 넣어 파악) 

 
        attackAction.performed += ctx =>                        //Attack Action의 Performed 이벤트 연결
        {
            anim.SetTrigger("Attack");
        };
        attackAction.Enable();                                  //Attack Action의 활성화 


    }



    void Update()
    {
        if (moveDir != Vector3.zero)    //이동한다면 
        {
            transform.rotation = Quaternion.LookRotation(moveDir);         //바라보는 쪽으로 전환
            transform.Translate(Vector3.forward * Time.deltaTime * 4.0f);  //
        }
        
    }





}
