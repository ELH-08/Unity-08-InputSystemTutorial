using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowCamera : MonoBehaviour
{
    //주석            : 개발자가 적고 컴파일러는 읽지 못함.
    //속성(attribute) : 개발자가 적고 컴파일러가 읽음. 외부 스크립트에서는 접근 못하지만 inspector 창에서는 강제로 보이게 함.  ex) SerializeField
    //멤버필드 : 정보 은닉의 이유로 private 추천,  private 선언 -> SerializeField 선언 -> 개발이 다 끝나면 SerializeFiled를 삭제한다.

    [SerializeField] private Transform camTr;                   //객체 자신 : 카메라
    [SerializeField] private Transform target;                  //목표 : 플레이어
    [SerializeField][Range(2f,20f)] private float height;       //높이 조절
    [SerializeField][Range(2f,20f)] private float distance;     //거리 조절
    public float damping = 10f;                                 //카메라가 갑자기 점핑하는 걸 막기 위해, 카메라 안 흔들리게 하는 값



    void Start()                                                //(3) - (script 활성화시) 1회 호출 / coroutine 가능
    {
        //script 활성화시 초깃값이 설정되면서 inspector창에 잡힌다    생성자에 대한 개념 정리할 것
        camTr = transform;
        target = GameObject.FindWithTag("Player").transform;    //목표(플레이어 위치) = 하이러키 창의 객체 중 Player tag를 가진 객체의 위치 
                                                                //1 GameObject                            하이러키 창에 있는 객체
                                                                //2 Destroy(gameObject, 3.0f);            //자기자신 객체
                                                                //2 Destroy(this.gameObject, 3.0f);       //자기자신 객체 
    }


    void Update()
    {
        //카메라 이동이 부드럽지 않음 0에서 바로 100이되니까 
        //camTr.position = target.position - (Vector3.forward * distance) + (Vector3.up * height);   //카메라 위치 =  캐릭터 위치 - 거리(앞 방향x거리 = 빼면 뒤에 있음) + 높이(위 방향x높이)


        //보간(Interpolation) 함수 : 두 점을 연결하는 방법, 궤적을 생성 -> 가속 / 감속되는 과정이 필요
        //Vector3.Lerp()  : 직선(선형) 보간
        //Vector3.Slerp() : 곡면 보간
        Vector3 pos = target.position - (Vector3.forward * distance) + (Vector3.up * height);   //카메라 위치 =  캐릭터 위치 - 거리(앞 방향x거리 = 빼면 뒤에 있음) + 높이(위 방향x높이)
        camTr.position = Vector3.Slerp(camTr.position, pos, Time.deltaTime * damping);          //곡면 보간 함수 (from 시작위치, to 목표위치, 시간)

        camTr.LookAt(target.position); //카메라 위치 = 목표 플레이어를 쳐다보기 

 



    }






}
