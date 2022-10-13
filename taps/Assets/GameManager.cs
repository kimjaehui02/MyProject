using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    #region 싱글톤
    /* // 싱글톤 //
 * instance라는 변수를 static으로 선언을 하여 다른 오브젝트 안의 스크립트에서도 instance를 불러올 수 있게 합니다 
 */
    public static GameManager instance = null;

    private void singleAwake()
    {
        if (instance == null) //instance가 null. 즉, 시스템상에 존재하고 있지 않을때
        {
            instance = this; //내자신을 instance로 넣어줍니다.
            DontDestroyOnLoad(gameObject); //OnLoad(씬이 로드 되었을때) 자신을 파괴하지 않고 유지
        }
        else
        {
            if (instance != this) //instance가 내가 아니라면 이미 instance가 하나 존재하고 있다는 의미
                Destroy(this.gameObject); //둘 이상 존재하면 안되는 객체이니 방금 AWake된 자신을 삭제
        }
    }
    #endregion

    //[field: SerializeField]

    [field: SerializeField]
    public List<DataOfParty> Party { get; set; }

    public GameObject FadeOutObject;

    public bool sceneMoving;

    //TitleScene, TownScene, BattleScene
    private void Start()
    {
        //MoveScene("TownScene");
    }
    private void Awake()
    {
        singleAwake();
    }

    public void MoveScene(string sname)
    {
        if(sceneMoving == true)
        {
            return;
        }
        sceneMoving = true;
        FadeOutObject.SetActive(true);
        StartCoroutine(FadeOut(sname));
        //SceneManager.LoadScene(name);
    }


    #region 코루틴
    IEnumerator FadeOut(string sname, float colora = 0)
    {
        //Debug.Log(2);
        yield return new WaitForSeconds(0.001f);

        if (colora >= 255)
        {
            SceneManager.LoadScene(sname);
            StartCoroutine(Fadeint());

        }
        else
        {
            
            //colora = FadeOutObject.GetComponent<Image>().color.a;
            //Debug.Log(FadeOutObject.GetComponent<Image>().color.a * 255);
            FadeOutObject.GetComponent<Image>().color = new(0 / 255f, 0 / 255f, 0 / 255f, (colora + 2) / 255f);
            colora += 2;
            //Debug.Log(FadeOutObject.GetComponent<Image>().color.a*255);
            StartCoroutine(FadeOut(sname, colora));
        }
    }

    IEnumerator Fadeint(float colora = 255)
    {
        //Debug.Log(2);
        yield return new WaitForSeconds(0.001f);

        if (colora <= 0)
        {

            //SceneManager.LoadScene(name);
            FadeOutObject.SetActive(false);
            sceneMoving = false;

        }
        else
        {

            //colora = FadeOutObject.GetComponent<Image>().color.a;
            //Debug.Log(FadeOutObject.GetComponent<Image>().color.a * 255);
            FadeOutObject.GetComponent<Image>().color = new(0 / 255f, 0 / 255f, 0 / 255f, (colora - 2) / 255f);
            colora -= 2;
            //Debug.Log(FadeOutObject.GetComponent<Image>().color.a*255);
            StartCoroutine(Fadeint(colora));
        }
    }


    #endregion

}

