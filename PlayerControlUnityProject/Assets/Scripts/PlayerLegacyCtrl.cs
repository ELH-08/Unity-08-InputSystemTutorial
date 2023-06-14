using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PlayerLegacyCtrl : MonoBehaviour
{
    public Animation animation;
    private float h, v;
    private Vector3 moveDir;
    public float speed = 5.0f;


    void Start()  //�������ڸ���
    {
        animation = GetComponent<Animation>();          //�ڽ��� ���۳�Ʈ���� �ڵ����� ������
        //animation = GetComponent<Animation>();        //���� �������� �־����

        animation.Play("Idle");                                            

    }


    void Update()
    {
        h = Input.GetAxis("Horizontal");
        v = Input.GetAxis("Vertical");

        moveDir = (h * Vector3.right) + (v * Vector3.forward);                  //�̵�����
        transform.Translate(moveDir.normalized * Time.deltaTime * speed);   //normalized(����ȭ) : �߸��� ���� ��ģ�� . wd�� ���ÿ� �������� ��Ÿ��� ���� 1.414�� ���°� �ƴ϶� �Ȱ��� 1�� ���� �Ѵ�. 




        #region     �ִϸ��̼� ����

        if (h > 0.1f)                           // ����̸�(D)
        {
            animation.CrossFade("RunR", 0.3f);  //CrossFade : ���� ���۰� ���� �ϴ� ������ �ִϸ��̼��� ȥ���ؼ� �ε巯�� �ִϸ��̼��� �����. ex) W�� D�� ��� �ε巴�� ǥ��

        }
        else if (h < -0.1f)                     //�����̸�(A) 
        {
            animation.CrossFade("RunL", 0.3f);

        }
        else if (v > 0.1f)                      //����̸�(W) 
        {
            animation.CrossFade("RunF", 0.3f);
        }
        else if (v < -0.1f)                     //�����̸�(S) 
        {
            animation.CrossFade("RunB", 0.3f);
        }
        else 
        {
            animation.CrossFade("Idle", 0.3f);
        }
        if (Input.GetKey(KeyCode.LeftShift) && Input.GetKey(KeyCode.W))     //shiftŰ�� W�� ���ÿ� ������ (����)
        {
            animation.CrossFade("SprintF", 0.3f);
            speed = 10f;                                                    //������Ʈ���� ��� �ӵ� ������
        }
        else if (Input.GetKeyUp(KeyCode.LeftShift))                         //shift Ű�� ���� (���� �ƴ� ��)
        {
            speed = 5f;
        }

        #endregion






    }


}
