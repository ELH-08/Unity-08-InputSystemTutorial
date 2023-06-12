
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


    private void Start()
    {
        anim = GetComponent<Animator>();
        transform = GetComponent<Transform>();

        
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




    void OnMove(InputValue _value)      //On�� �ٿ��� ȣ����
    {
        Vector2 dir = _value.Get<Vector2>();            //�Է��� 2����
        moveDir = new Vector3(dir.x, 0f, dir.y);        //�����̴� �� 3����
        anim.SetFloat("Movement", dir.magnitude);

        //Debug.Log($"Move = ({dir.x},{dir.y})");   //�޸� ��Ƹ����Ƿ� ��� ������ �ּ� �Ǵ� ������ ��

    }

    void OnAttack()                     //On�� �ٿ��� ȣ����
    {

        //Debug.Log($"Attack");

        anim.SetTrigger("Attack");




    }














}
