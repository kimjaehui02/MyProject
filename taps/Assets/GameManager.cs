using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    #region �̱���
    /* // �̱��� //
 * instance��� ������ static���� ������ �Ͽ� �ٸ� ������Ʈ ���� ��ũ��Ʈ������ instance�� �ҷ��� �� �ְ� �մϴ� 
 */
    public static GameManager instance = null;

    private void singleAwake()
    {
        if (instance == null) //instance�� null. ��, �ý��ۻ� �����ϰ� ���� ������
        {
            instance = this; //���ڽ��� instance�� �־��ݴϴ�.
            DontDestroyOnLoad(gameObject); //OnLoad(���� �ε� �Ǿ�����) �ڽ��� �ı����� �ʰ� ����
        }
        else
        {
            if (instance != this) //instance�� ���� �ƴ϶�� �̹� instance�� �ϳ� �����ϰ� �ִٴ� �ǹ�
                Destroy(this.gameObject); //�� �̻� �����ϸ� �ȵǴ� ��ü�̴� ��� AWake�� �ڽ��� ����
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

    #region ȭ����ȯ ó����

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

    #region ���̵� �� ���� ��뿡 ���̴� �Լ�

    public void MoveScene(string sname ,int input)
    {
        // ���� �̵����̸� �� �̵����� �ʰ� ����ϴ�
        if(sceneMoving == true)
        {
            return;
        }

        // ���� �̵����̶�� �˷��ݴϴ�
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

        // ���� �ð��� ������ �̵��մϴ�
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

        // ���� �ð��� ������ �̵��մϴ�
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

    #region �ڷ�ƾ
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

