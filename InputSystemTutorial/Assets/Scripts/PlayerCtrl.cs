
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


    private PlayerInput playerInput;
    private InputActionMap mainActionMap;
    private InputAction moveAction;
    private InputAction attackAction;

    private void Start() //1회 호출
    {
        anim = GetComponent<Animator>();
        transform = GetComponent<Transform>();

        playerInput = GetComponent<PlayerInput>();
        mainActionMap = playerInput.actions.FindActionMap("PlayerActions");
        moveAction = playerInput.actions.FindAction("Move");
        attackAction = playerInput.actions.FindAction("Attack");



        #region INVOKE_C_SHARP_EVENT            //INVOKE_C_SHARP_EVENT에 연결된 필드
        //Move Action의 Performed 이벤트 연결 (람다식)
        moveAction.performed += ctx =>                  //context 줄임
        {
            Vector2 dir = ctx.ReadValue<Vector2>();
            moveDir = new Vector3(dir.x, 0f, dir.y);

            anim.SetFloat("Movement", dir.magnitude);    //dir.magnitude(= dir.x, dir.y의 크기)만큼 이동 

        };


        //Move Action의 Canceled 이벤트 연결 (람다식)
        moveAction.canceled += ctx =>                   //context 줄임
        {
            moveDir = Vector3.zero;
            anim.SetFloat("Movement", 0.0f);

        };


        //Attack Action의 Performed 이벤트 연결 (람다식)
        attackAction.performed += ctx =>                   //context 줄임
        {
            Debug.Log("Attack by C# event");
            anim.SetTrigger("Attack");

        };
        #endregion




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



    #region SEND_MESSAGE                //SEND_MESSAGE에 관련된 지역 필드 

    void OnMove(InputValue _value)      //Player Input에서 SendMessage로 On만 붙여서 호출함
    {
        Vector2 dir = _value.Get<Vector2>();            //입력은 2차원
        moveDir = new Vector3(dir.x, 0f, dir.y);        //움직이는 건 3차원
        anim.SetFloat("Movement", dir.magnitude);

        //Debug.Log($"Move = ({dir.x},{dir.y})");   //메모리 잡아먹으므로 출시 전에는 주석 또는 지워야 함

    }

    void OnAttack()                     //Player Input에서 SendMessage로 On만 붙여서 호출함
    {

        //Debug.Log($"Attack");

        anim.SetTrigger("Attack");

    }

    #endregion





    #region INVOKE_UNITY_EVENTS         //INVOKE_UNITY_EVENTS에 관련된 지역 필드

    
    //Invoke UnityEvents 옵션을 사용해 연결한 함수는 총 3번 호출된다.
    // 즉 Input Action에 정의한 액션은 시작started / 실행performed / 취소canceled 콜백함수를 각각 한번씩 호출하며, 어떤 상태로 호출됐는지에 대한 정보는 CallbackContext.phase 속성을 통해서 알 수 있다.
    // 샌드메시지 방식보다 선호하는 방식

    public void OnMove(InputAction.CallbackContext _context) 
    {
        Vector2 dir = _context.ReadValue<Vector2>();
        moveDir = new Vector3(dir.x, 0f, dir.y);
        anim.SetFloat("Movement", dir.magnitude);       //dir.magnitude(= dir.x, dir.y의 크기)만큼 이동 

    }

    public void OnAttack(InputAction.CallbackContext _context) 
    {

        Debug.Log($"_context.phase = {_context.phase}");    //어떤 실행 단계인지, 실행중인 단계인지 


        if (_context.performed) //실행중일 때
        {
            Debug.Log("Attack");
            anim.SetTrigger("Attack");
        }



    }

    #endregion















}
