using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowCamera : MonoBehaviour
{
    //주석 : 사람이 적고 컴파일러는 읽지 못함.
    //속성 attribute : 사람이 적고 컴파일러가 읽는다. 하지만 외부 스크립트에서는 접근 못하지만 인스펙터에서는 강제로 보이게 함.

    [SerializeField] private Transform camTr;    //객체(카메라) 자기 자신
    [SerializeField] private Transform target;   //목표(플레이어)
    [SerializeField][Range(2f,20f)] private float height;       //높이 조절
    [SerializeField][Range(2f,20f)] private float distance;     //거리 조절
    public float damping = 10f;         //카메라가 갑자기 점핑하는 걸 막기 위해, 카메라 안흔들리게 하는 값
    //멤버필드는 정보 은닉의 이유로 private 추천
    //private 선언 -> SerializeField 선언 -> 개발이 다 끝나면 SerializeFiled를 삭제한다.



    void Start()  //시작과 동시에
    {
        //초기화하면서 잡힌다    생성자에 대한 개념 
        camTr = transform;
        target = GameObject.FindWithTag("Player").transform;    //하이러키에서.태그를찾아.
                                                                //find 그냥 객체명 찾기

        //Destroy(gameObject, 3.0f);            //자기자신 객체
        //Destroy(this.gameObject, 3.0f);       //자기자신 객체 

    }

    void Update()
    {
        //카메라 이동이 부드럽지 않음 0에서 바로 100이되니까 
        //camTr.position = target.position - (Vector3.forward * distance) + (Vector3.up * height);   //카메라 위치 =  캐릭터 위치 - 거리(앞 방향x거리 = 빼면 뒤에 있음) + 높이(위 방향x높이)


        //보간 함수 : 가속 / 감속되는 과정이 필요
        //Vector3.Lerp() : 선형(직선) 보간
        //Vector3.Slerp() : 곡면 보간
        Vector3 pos = target.position - (Vector3.forward * distance) + (Vector3.up * height);   //카메라 위치 =  캐릭터 위치 - 거리(앞 방향x거리 = 빼면 뒤에 있음) + 높이(위 방향x높이)
        camTr.position = Vector3.Slerp(camTr.position, pos, Time.deltaTime * damping);          //곡면 보간 함수 (from 시작위치, to 목표위치, 시간)

        camTr.LookAt(target.position); //카메라 위치 = 목표 플레이어를 쳐다보기 

 



    }






}
