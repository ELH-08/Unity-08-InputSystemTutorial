using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowCamera : MonoBehaviour
{
    //�ּ� : ����� ���� �����Ϸ��� ���� ����.
    //�Ӽ� attribute : ����� ���� �����Ϸ��� �д´�. ������ �ܺ� ��ũ��Ʈ������ ���� �������� �ν����Ϳ����� ������ ���̰� ��.

    [SerializeField] private Transform camTr;    //��ü(ī�޶�) �ڱ� �ڽ�
    [SerializeField] private Transform target;   //��ǥ(�÷��̾�)
    [SerializeField][Range(2f,20f)] private float height;       //���� ����
    [SerializeField][Range(2f,20f)] private float distance;     //�Ÿ� ����
    public float damping = 10f;         //ī�޶� ���ڱ� �����ϴ� �� ���� ����, ī�޶� ����鸮�� �ϴ� ��
    //����ʵ�� ���� ������ ������ private ��õ
    //private ���� -> SerializeField ���� -> ������ �� ������ SerializeFiled�� �����Ѵ�.



    void Start()  //���۰� ���ÿ�
    {
        //�ʱ�ȭ�ϸ鼭 ������    �����ڿ� ���� ���� 
        camTr = transform;
        target = GameObject.FindWithTag("Player").transform;    //���̷�Ű����.�±׸�ã��.
                                                                //find �׳� ��ü�� ã��

        //Destroy(gameObject, 3.0f);            //�ڱ��ڽ� ��ü
        //Destroy(this.gameObject, 3.0f);       //�ڱ��ڽ� ��ü 

    }

    void Update()
    {
        //ī�޶� �̵��� �ε巴�� ���� 0���� �ٷ� 100�̵Ǵϱ� 
        //camTr.position = target.position - (Vector3.forward * distance) + (Vector3.up * height);   //ī�޶� ��ġ =  ĳ���� ��ġ - �Ÿ�(�� ����x�Ÿ� = ���� �ڿ� ����) + ����(�� ����x����)


        //���� �Լ� : ���� / ���ӵǴ� ������ �ʿ�
        //Vector3.Lerp() : ����(����) ����
        //Vector3.Slerp() : ��� ����
        Vector3 pos = target.position - (Vector3.forward * distance) + (Vector3.up * height);   //ī�޶� ��ġ =  ĳ���� ��ġ - �Ÿ�(�� ����x�Ÿ� = ���� �ڿ� ����) + ����(�� ����x����)
        camTr.position = Vector3.Slerp(camTr.position, pos, Time.deltaTime * damping);          //��� ���� �Լ� (from ������ġ, to ��ǥ��ġ, �ð�)

        camTr.LookAt(target.position); //ī�޶� ��ġ = ��ǥ �÷��̾ �Ĵٺ��� 

 



    }






}
