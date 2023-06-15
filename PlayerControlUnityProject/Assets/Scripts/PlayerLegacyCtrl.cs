using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PlayerLegacyCtrl : MonoBehaviour
{
    public Animation animation;
    private float h, v;
    private Vector3 moveDir;
    public float speed = 5.0f;


    void Start()                                        //(3) - (script 활성화시) 1회 호출 / coroutine 가능
    {
        animation = GetComponent<Animation>();          //스크립트 시작되자 마자 이 스크립트가 달린 객체(플레이어)의 컴퍼넌트 중 Animation을 자동으로 저장
        //animation = GetComponent<Animation>();        //만일 이 부분이 주석처리되면 직접 수동으로 넣어야함

        animation.Play("Idle");                         //Loop처리된 Idle Animation이라 한번만 호출해도 됨                                         

    }


    void Update()
    {
        h = Input.GetAxis("Horizontal");                                    //A,D
        v = Input.GetAxis("Vertical");                                      //W,S

        moveDir = (h * Vector3.right) + (v * Vector3.forward);              //이동방향
        transform.Translate(moveDir.normalized * Time.deltaTime * speed);   //normalized(정규화) : wd를 동시에 눌렀을때 피타고라스 정리 1.414로 가는게 아니라 똑같이 1로 가야 한다. 



        #region     Animation 연동

        if (h > 0.1f)                           // h축이 양수이면(D)
        {
            animation.CrossFade("RunR", 0.3f);  //RunR 클립으로 서서히 전환
                                                //CrossFade(클립명, 전환에 걸리는 시간) : 이전 동작과 지금 동작의 애니메이션을 부드럽게 블렌딩해서 서서히 전환. ex) W 중에 D를 섞어서 부드럽게 표현

        }
        else if (h < -0.1f)                     // h축이 음수이면(A) 
        {
            animation.CrossFade("RunL", 0.3f);

        }
        else if (v > 0.1f)                      //v축이 양수이면(W) 
        {
            animation.CrossFade("RunF", 0.3f);
        }
        else if (v < -0.1f)                     //v축이 음수이면(S) 
        {
            animation.CrossFade("RunB", 0.3f);
        }
        else                                    //아무 입력 키가 없을 경우
        {
            animation.CrossFade("Idle", 0.3f);
        }
        if (Input.GetKey(KeyCode.LeftShift) && Input.GetKey(KeyCode.W))     //shift키와 W를 동시에 누르면 (질주)
        {
            animation.CrossFade("SprintF", 0.3f);                           
            speed = 10f;                                                    //이대로만 놔두면 Update() 영역에서 계속 누적됨
        }
        else if (Input.GetKeyUp(KeyCode.LeftShift))                         //shift 키를 떼면 (질주 아닐 때)
        {
            speed = 5f;
        }

        #endregion


    }


}
