using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class eight : MonoBehaviour
{
    //제대로 갔다면 다음 무작위 씬으로 넘어감. 번호 +1
    //틀렸다면 다음 무작위 씬으로 넘어가고, 번호가 초기화 됨 
    //만약 이상현상이 씬 4, 6, 7, 9 에 있으면 걔네 어떻게 할거야? 
    //4,6,7,9에만 게임 오브젝트를 두고 그 게임 오브젝트가 있다면 없다면으로 체크해서 하는건?

    public bool isTrue;     //제대로 갔나염?

    public int scene;   //씬 랜덤

    public int exit = 0;                        //출구
    public TMP_Text dayText;                    //출구 나오는 채팅

    public GameObject bed;                      //침대 (잠)
    public GameObject door;                     //문   (밖으로 나감)

    void Start()
    {
        scene = Random.Range(0, 10);
    }

    void Update()
    {
        if(isTrue == true)                      //맞췄을 때
        {
            exit++;                             //출구 번호 +1
            dayText.text = exit.ToString(exit + "Days");
        }

        else if ( isTrue == false)              //틀렸을 때
        {
            exit = 0;                           //출구 번호 0번. (초기화)
            dayText.text = exit.ToString(exit + "Days");
        }

        if(isTrue)                              //일단 어디든 갔을 때
        {
           SceneManager.LoadScene(scene);      //랜덤 0~10 씬 출력
        }

        Scene nowscene = SceneManager.GetActiveScene();     //현재 씬을 가져오고
        if (nowscene.buildIndex == 4)                       //현재 씬이 4번이면 ( 이상한 씬 )
        {
            //앞 뒤로 트리거 설정할 게임 오브젝트 한 개씩 두고 그 게임 오브젝트를 지나갔는지 판별하면 될 것 같네요.
        }
    }
}
