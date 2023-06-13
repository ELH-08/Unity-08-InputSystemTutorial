
# pragma warning disable IDE0051                    //�Լ��� �����ϰ� ȣ������ ������ ��� �ߴ� ���� ���� �ڵ�
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;                      //InputSystem ���̺귯�� �߰�




public class PlayerCtrl : MonoBehaviour
{

    private Animator anim;
    private new Transform transform;    //��ӹ��� �θ� Ŭ������ �Լ� ����⸦ �����ϱ� ���ؼ�?
    private Vector3 moveDir;            //�Է��� 2���������� ���� �����̴� �� 3����


    private PlayerInput playerInput;
    private InputActionMap mainActionMap;
    private InputAction moveAction;
    private InputAction attackAction;

    private void Start() //1ȸ ȣ��
    {
        anim = GetComponent<Animator>();
        transform = GetComponent<Transform>();

        playerInput = GetComponent<PlayerInput>();
        mainActionMap = playerInput.actions.FindActionMap("PlayerActions");
        moveAction = playerInput.actions.FindAction("Move");
        attackAction = playerInput.actions.FindAction("Attack");



        #region INVOKE_C_SHARP_EVENT            //INVOKE_C_SHARP_EVENT�� ����� �ʵ�
        //Move Action�� Performed �̺�Ʈ ���� (���ٽ�)
        moveAction.performed += ctx =>                  //context ����
        {
            Vector2 dir = ctx.ReadValue<Vector2>();
            moveDir = new Vector3(dir.x, 0f, dir.y);

            anim.SetFloat("Movement", dir.magnitude);    //dir.magnitude(= dir.x, dir.y�� ũ��)��ŭ �̵� 

        };


        //Move Action�� Canceled �̺�Ʈ ���� (���ٽ�)
        moveAction.canceled += ctx =>                   //context ����
        {
            moveDir = Vector3.zero;
            anim.SetFloat("Movement", 0.0f);

        };


        //Attack Action�� Performed �̺�Ʈ ���� (���ٽ�)
        attackAction.performed += ctx =>                   //context ����
        {
            Debug.Log("Attack by C# event");
            anim.SetTrigger("Attack");

        };
        #endregion




    }

    private void Update()
    {
        if (moveDir != Vector3.zero)            //�����δٸ�  
        {
            //�Է��� Ű���� ����������� ȸ�� 
            transform.rotation = Quaternion.LookRotation(moveDir);
            //ȸ���� �� ���� �������� �̵�
            transform.Translate(Vector3.forward * Time.deltaTime * 4.0f);
        }
        
    }



    #region SEND_MESSAGE                //SEND_MESSAGE�� ���õ� ���� �ʵ� 

    void OnMove(InputValue _value)      //Player Input���� SendMessage�� On�� �ٿ��� ȣ����
    {
        Vector2 dir = _value.Get<Vector2>();            //�Է��� 2����
        moveDir = new Vector3(dir.x, 0f, dir.y);        //�����̴� �� 3����
        anim.SetFloat("Movement", dir.magnitude);

        //Debug.Log($"Move = ({dir.x},{dir.y})");   //�޸� ��Ƹ����Ƿ� ��� ������ �ּ� �Ǵ� ������ ��

    }

    void OnAttack()                     //Player Input���� SendMessage�� On�� �ٿ��� ȣ����
    {

        //Debug.Log($"Attack");

        anim.SetTrigger("Attack");

    }

    #endregion





    #region INVOKE_UNITY_EVENTS         //INVOKE_UNITY_EVENTS�� ���õ� ���� �ʵ�

    
    //Invoke UnityEvents �ɼ��� ����� ������ �Լ��� �� 3�� ȣ��ȴ�.
    // �� Input Action�� ������ �׼��� ����started / ����performed / ���canceled �ݹ��Լ��� ���� �ѹ��� ȣ���ϸ�, � ���·� ȣ��ƴ����� ���� ������ CallbackContext.phase �Ӽ��� ���ؼ� �� �� �ִ�.
    // ����޽��� ��ĺ��� ��ȣ�ϴ� ���

    public void OnMove(InputAction.CallbackContext _context) 
    {
        Vector2 dir = _context.ReadValue<Vector2>();
        moveDir = new Vector3(dir.x, 0f, dir.y);
        anim.SetFloat("Movement", dir.magnitude);       //dir.magnitude(= dir.x, dir.y�� ũ��)��ŭ �̵� 

    }

    public void OnAttack(InputAction.CallbackContext _context) 
    {

        Debug.Log($"_context.phase = {_context.phase}");    //� ���� �ܰ�����, �������� �ܰ����� 


        if (_context.performed) //�������� ��
        {
            Debug.Log("Attack");
            anim.SetTrigger("Attack");
        }



    }

    #endregion















}
