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
    public List<RealPlayer> listRealPlayer { get; set; }

    public int Gold;

    public int HpItem;

    public int mapnumber;

    public List<int> map1;
    public List<int> map2;
    public List<int> map3;
    public List<int> map4;
    public List<int> map5;

    #region 화면전환 처리용

    public GameObject FadeOutObject;
    public Image FadeOutObjectImage;

    public bool sceneMoving;


    [SerializeField]
    private float floatOfFadeTimeMax;

    [SerializeField]
    private float floatOfFadeTime;

    [SerializeField]
    private int intOfFadein;

    private string stringOfSceneName;

    #endregion

    //TitleScene, TownScene, BattleScene
    private void Start()
    {
        //MoveScene("TownScene");
        PlayerStart();
        PlayerStart();
        PlayerStart(2);
        PlayerStart(3);


        map1[0] = (Random.Range(0, 2));
        map1[1] = (Random.Range(0, 3));
        map1[2] = (Random.Range(0, 3));

        map2[0] = (Random.Range(0, 2));
        map2[1] = (Random.Range(0, 3));
        map2[2] = (Random.Range(0, 3));

        map3[0] = (Random.Range(0, 2));
        map3[1] = (Random.Range(0, 3));
        map3[2] = (Random.Range(0, 3));

        map4[0] = (Random.Range(0, 2));
        map4[1] = (Random.Range(0, 3));
        map1[2] = (Random.Range(0, 3));

        map5[0] = (Random.Range(0, 2));
        map5[1] = (Random.Range(0, 3));
        map5[2] = (Random.Range(0, 3));


    }
    private void Awake()
    {
        singleAwake();
        FadeOutObjectImage = FadeOutObject.GetComponent<Image>();
        sceneMoving = false;
        listRealPlayer = new();
        Gold += 1000;
    }

    private void Update()
    {
        SceneFadeOut();
        SceneFadeIn();
    }

    #region 씬이동 및 게임 운용에 쓰이는 함수

    public void MoveScene(string sname ,int input)
    {
        // 씬이 이동중이면 또 이동하지 않게 만듭니다
        if(sceneMoving == true)
        {
            return;
        }

        // 씬이 이동중이라고 알려줍니다
        sceneMoving = true;
        mapnumber += input;
        // 
        FadeOutObject.SetActive(true);
        stringOfSceneName = sname;
        intOfFadein++;
        //SceneManager.LoadScene(name);
    }

    public void PlusScene()
    {

    }

    public void MinusScene()
    {

    }


    private void SceneFadeOut()
    {
        if(intOfFadein != 1)
        {
            return;
        }

        floatOfFadeTime += Time.deltaTime;

        float colora = floatOfFadeTime / floatOfFadeTimeMax;
        FadeOutObjectImage.color = new Color(0 / 255f, 0 / 255f, 0 / 255f, (255 * colora) / 255f);

        // 지정 시간이 넘으면 이동합니다
        if (floatOfFadeTimeMax < floatOfFadeTime)
        {
            SceneManager.LoadScene(stringOfSceneName);
            intOfFadein++;
        }
    }

    private void SceneFadeIn()
    {
        if (intOfFadein != 2)
        {
            return;
        }

        floatOfFadeTime -= Time.deltaTime;

        float colora = floatOfFadeTime / floatOfFadeTimeMax;

        FadeOutObjectImage.color = new Color(0 / 255f, 0 / 255f, 0 / 255f, (255 * colora) / 255f);

        // 지정 시간이 넘으면 이동합니다
        if (floatOfFadeTime < 0)
        {
            FadeOutObject.SetActive(false);
            intOfFadein = 0;
            sceneMoving = false;
        }
    }

    #endregion

    public void PlayerStart(int intint =1)
    {


        RealPlayer realPlayer = new();
        realPlayer.NewPlayer(intint, listRealPlayer.Count, listRealPlayer.Count);

        listRealPlayer.Add(realPlayer);
        Debug.Log(listRealPlayer.Count);
        HpObserver hpObserver = new HpObserver();
    }

    #region 코루틴
    IEnumerator FadeOut(string sname, float colora = 0)
    {
        //Debug.Log(2);
        yield return new WaitForSecondsRealtime(0.001f);

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
        yield return new WaitForSecondsRealtime(0.001f);

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

