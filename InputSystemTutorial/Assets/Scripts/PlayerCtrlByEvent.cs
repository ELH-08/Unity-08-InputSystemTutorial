
# pragma warning disable IDE0051                    //��� ���� �ڵ� : �Լ� ���� �� ��ȣ��� �ߴ� ��� ����
using System.Collections;
using System.Collections.Generic;                   //��Ȱ��ȭ : ������ ���� ���̺귯��
using UnityEngine;                                  //Ȱ��ȭ : ���� ���̺귯��
using UnityEngine.InputSystem;                      //InputSystem ���̺귯�� ȣ��



public class PlayerCtrlByEvent : MonoBehaviour
{
    //��Ȱ��ȭ
    //private InputAction moveAction;

    private InputAction moveAction;
    private InputAction attackAction;
    private Animator anim;
    private Vector3 moveDir;



    void Start()    //1ȸ ȣ��
    {
        anim = GetComponent<Animator>();
        
        //Move Action ���� �� Ÿ�� ����
        moveAction = new InputAction("Move", InputActionType.Value);

        
        /*Main Action���� ������ ���۾� ���� �ʰ� ��� ��ũ��Ʈ�� ����*/

        //Move Action�� ���� ���ε� ���� ����
        moveAction.AddCompositeBinding("2DVector")         //Move Action - Player Actions - Move - WASD - composite type - 2D Vector
            .With("Up", "<Keyboard>/w")
            .With("Down", "<Keyboard>/s")
            .With("left", "<Keyboard>/a")
            .With("right", "<Keyboard>/d");

        //Move Action�� performed canceled �̺�Ʈ ����
        moveAction.performed += ctx => 
        {
            Vector2 dir = ctx.ReadValue<Vector2>();
            moveDir = new Vector3(dir.x, 0f, dir.y);        
            anim.SetFloat("Movement", dir.magnitude);

        };

        //Move Action�� performed canceled �̺�Ʈ ����
        moveAction.canceled += ctx =>
        {
            moveDir = Vector3.zero;                     //�������� ����
            anim.SetFloat("Movement", 0f);

        };

        moveAction.Enable();                                                                     //Move �׼� Ȱ��ȭ
        attackAction = new InputAction("Attack", InputActionType.Button, "<Keyboard>/space");    //Attack �׼��� Ȱ��ȭ    (Tip : �Ű������� ,�� �־� �ľ�) 

 
        attackAction.performed += ctx =>                        //Attack Action�� Performed �̺�Ʈ ����
        {
            anim.SetTrigger("Attack");
        };
        attackAction.Enable();                                  //Attack Action�� Ȱ��ȭ 


    }



    void Update()
    {
        if (moveDir != Vector3.zero)    //�̵��Ѵٸ� 
        {
            transform.rotation = Quaternion.LookRotation(moveDir);         //�ٶ󺸴� ������ ��ȯ
            transform.Translate(Vector3.forward * Time.deltaTime * 4.0f);  //
        }
        
    }





}
