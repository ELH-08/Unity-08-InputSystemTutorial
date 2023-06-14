using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PlayerLegacyCtrl : MonoBehaviour
{
    public Animation animation;
    private float h, v;
    private Vector3 moveDir;
    public float speed = 5.0f;


    void Start()  //시작하자마자
    {
        animation = GetComponent<Animation>();          //자신의 컴퍼넌트에서 자동으로 가져옴
        //animation = GetComponent<Animation>();        //직접 수동으로 넣어야함

        animation.Play("Idle");                                            

    }


    void Update()
    {
        h = Input.GetAxis("Horizontal");
        v = Input.GetAxis("Vertical");

        moveDir = (h * Vector3.right) + (v * Vector3.forward);                  //이동방향
        transform.Translate(moveDir.normalized * Time.deltaTime * speed);   //normalized(정규화) : 잘못된 것을 고친다 . wd를 동시에 눌렀을때 피타고라스 정리 1.414로 가는게 아니라 똑같이 1로 가야 한다. 




        #region     애니메이션 연동

        if (h > 0.1f)                           // 양수이면(D)
        {
            animation.CrossFade("RunR", 0.3f);  //CrossFade : 직전 동작과 지금 하는 동작을 애니메이션을 혼합해서 부드러운 애니메이션을 만든다. ex) W와 D를 섞어서 부드럽게 표현

        }
        else if (h < -0.1f)                     //음수이면(A) 
        {
            animation.CrossFade("RunL", 0.3f);

        }
        else if (v > 0.1f)                      //양수이면(W) 
        {
            animation.CrossFade("RunF", 0.3f);
        }
        else if (v < -0.1f)                     //음수이면(S) 
        {
            animation.CrossFade("RunB", 0.3f);
        }
        else 
        {
            animation.CrossFade("Idle", 0.3f);
        }
        if (Input.GetKey(KeyCode.LeftShift) && Input.GetKey(KeyCode.W))     //shift키와 W를 동시에 누르면 (질주)
        {
            animation.CrossFade("SprintF", 0.3f);
            speed = 10f;                                                    //업데이트에서 계속 속도 누적됨
        }
        else if (Input.GetKeyUp(KeyCode.LeftShift))                         //shift 키를 떼면 (질주 아닐 때)
        {
            speed = 5f;
        }

        #endregion






    }


}
