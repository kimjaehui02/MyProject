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
    public List<DataOfParty> Party { get; set; }

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

    //TitleScene, TownScene, BattleScene
    private void Start()
    {
        //MoveScene("TownScene");
    }
    private void Awake()
    {
        singleAwake();
        FadeOutObjectImage = FadeOutObject.GetComponent<Image>();
        sceneMoving = false;
    }

    private void Update()
    {
        SceneFadeOut();
        SceneFadeIn();
    }

    public void MoveScene(string sname)
    {
        // ���� �̵����̸� �� �̵����� �ʰ� ����ϴ�
        if(sceneMoving == true)
        {
            return;
        }

        // ���� �̵����̶�� �˷��ݴϴ�
        sceneMoving = true;

        // 
        FadeOutObject.SetActive(true);
        stringOfSceneName = sname;
        intOfFadein++;
        //SceneManager.LoadScene(name);
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

