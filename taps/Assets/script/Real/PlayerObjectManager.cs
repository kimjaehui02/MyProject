using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PlayerObjectManager : MonoBehaviour
{
    public List<Animator> listAnimatorOfSprite;

    public List<SpriteRenderer> listSpriteRendererOfHpBar;

    public List<SpriteRenderer> listSpriteRendererOfStaminaBar;

    public List<Color> listColorOfHpBar;

    public List<Color> listColorOfStaminaBar;

    public Color ColorOfWarning;

    /// <summary>
    /// �����̴� �ð�
    /// </summary>
    public float floatOfWarningColorMax;

    /// <summary>
    /// �������� ���� �ð����� �����ִ� �÷�Ʈ����
    /// </summary>
    public float floatOfWarningColor;

    /// <summary>
    /// ü���� �� �����Ŷ�� ����Դϴ�
    /// </summary>
    public int intOfWarning;

    /// <summary>
    /// �������� ���ؼ� ���� ������ȯ�� ��
    /// </summary>
    public bool boolOfWarningColor;

    [SerializeField]
    public int intOfHp;

    [SerializeField]
    public int intOfSta;

    public int number;

    public List<GameObject> gameObjects;
    public GameObject gameObjects2;

    public bool enemytest;

    private void Awake()
    {
        listAnimatorOfSprite.Add(null);
        listAnimatorOfSprite.Add(null);
        listAnimatorOfSprite.Add(null);
        listAnimatorOfSprite.Add(null);
        listAnimatorOfSprite.Add(null);

        listAnimatorOfSprite[0] = transform.GetChild(0).GetComponent<Animator>();
        listAnimatorOfSprite[1] = transform.GetChild(0).GetChild(0).GetComponent<Animator>();
        listAnimatorOfSprite[2] = transform.GetChild(0).GetChild(1).GetComponent<Animator>();
        listAnimatorOfSprite[3] = transform.GetChild(0).GetChild(2).GetComponent<Animator>();
        listAnimatorOfSprite[4] = transform.GetChild(0).GetChild(3).GetComponent<Animator>();
        Vector33 = game.transform.localPosition;
    }

    private void Update()
    {
        //BarColorChange();

        if(enemytest == false)
        {
            Color3();
        }
        else
        {
            BarColorChange();

        }
        Animinput2Move();
    }

    /// <summary>
    /// �ٵ��� ������ �ٲ��ݴϴ�
    /// </summary>
    public void BarColorChange()
    {
        for (int i = 0; i < listSpriteRendererOfHpBar.Count; i++)
        {
            listSpriteRendererOfHpBar[i].color = listColorOfHpBar[0];
        }

        for (int i = 0; i < intOfHp; i++)
        {
            listSpriteRendererOfHpBar[i].color = listColorOfHpBar[1];

        }

        for (int i = 0; i < listSpriteRendererOfStaminaBar.Count; i++)
        {
            listSpriteRendererOfStaminaBar[i].color = listColorOfStaminaBar[0];

        }

        for (int i = 0; i < intOfSta; i++)
        {
            listSpriteRendererOfStaminaBar[i].color = listColorOfStaminaBar[1];

        }

        intOfWarning = (intOfSta - (intOfSta % 2)) / 2;

        if (intOfWarning > 0)
        {
            if(boolOfWarningColor == false)
            {
                floatOfWarningColor += Time.deltaTime;

            }
            else
            {
                floatOfWarningColor -= Time.deltaTime;

            }

            // ���� false   ++   >   true  �� �Ǿ���
            // ���� true    --   <   false �� �Ǿ���
            if (floatOfWarningColor > floatOfWarningColorMax -0.2f)
            {
                boolOfWarningColor = true;
            }

            if (floatOfWarningColor < 0.2f)
            {
                boolOfWarningColor = false;
            }

            ColorOfWarning = new Color(listSpriteRendererOfHpBar[0].color.r * (floatOfWarningColorMax - floatOfWarningColor)
                                     , listSpriteRendererOfHpBar[0].color.g * (floatOfWarningColorMax - floatOfWarningColor)
                                     , listSpriteRendererOfHpBar[0].color.b * (floatOfWarningColorMax - floatOfWarningColor));

            int warning = intOfWarning < intOfHp ? intOfWarning : intOfHp;

            for (int i = 0; i <warning; i++)
            {
                listSpriteRendererOfHpBar[i].color = ColorOfWarning;
            }
        }


    }

    public void Color2()
    {
        if (GameManager.instance.listRealPlayer[number].floatOfHp == 0)
        {
            gameObjects2.GetComponent<SpriteRenderer>().color = ColorOfWarning;


        }
        else
        {
            gameObjects2.GetComponent<SpriteRenderer>().color = new Color(255 / 255f, 255 / 255f, 255 / 255f, 255 / 255f);

        }

        for (int i = 0; i < gameObjects.Count; i++)
        {
            gameObjects[i].SetActive(false);
        }

        for (int i = 0; i < GameManager.instance.listRealPlayer[number].LEVEL + 3; i++)
        {
            gameObjects[i].SetActive(true);
            gameObjects[i].GetComponent<SpriteRenderer>().color = listColorOfHpBar[0];
        }

        for (int i = 0; i < GameManager.instance.listRealPlayer[number].floatOfHp; i++)
        {
            gameObjects[i].GetComponent<SpriteRenderer>().color = listColorOfHpBar[1];
        }


    }

    public void Color3()
    {

    }

    /// <summary>
    /// ü�°� ���¹̳ʸ� �����մϴ�
    /// </summary>
    public void Setting(float hp, float sta)
    {
        intOfHp = (int)hp;
        intOfSta = (int)sta;
    }

    /// <summary>
    /// ü�µ������� �Խ��ϴ�
    /// </summary>
    /// <param name="damage"></param>
    public void HpDamage(int damage = 1)
    {
        intOfHp -= damage;
        if(intOfWarning > 0)
        {
            intOfHp -= intOfWarning;
            intOfSta = 0;
        }
        Animinput2();
    }

    /// <summary>
    /// ���¹̳� �������� �Խ��ϴ�
    /// </summary>
    /// <param name="damage"></param>
    public void StaDamage(int damage = 1)
    {
        intOfSta += damage;

        //intOfSta %= listColorOfStaminaBar.Count;
    }

    public void Animinput()
    {
        for (int i = 0; i < listAnimatorOfSprite.Count; i++)
        {
            //listAnimatorOfSprite[i].SetTrigger("Attack");
            listAnimatorOfSprite[i].SetTrigger("Hit");
            boolOfMove = true;
        }
    }

    public void Animinput2()
    {
        for (int i = 0; i < listAnimatorOfSprite.Count; i++)
        {
            listAnimatorOfSprite[i].SetTrigger("Hit");
            boolOfMove = true;
        }
    }

    public bool boolOfMove;
    private float floatOfMoveMax =0.5f;
    public float floatOfMove;

    public Vector3 Vector33;
    public GameObject game;
    public void Animinput2Move()
    {
        if(boolOfMove == false)
        {
            return;
        }


        if (0 < floatOfMove && floatOfMove < 0.05f)
        {
            game.transform.localPosition = new Vector3(0.2f + Vector33.x, Vector33.y, Vector33.z);
        }

        if (0.05f< floatOfMove && floatOfMove < 0.1f)
        {
            game.transform.localPosition = new Vector3(-0.2f + Vector33.x, Vector33.y, Vector33.z);
        }

        if (0.1f < floatOfMove && floatOfMove < 0.15f)
        {
            game.transform.localPosition = new Vector3(0.2f + Vector33.x, Vector33.y, Vector33.z);
        }

        if (0.15f < floatOfMove && floatOfMove < 0.20f)
        {
            game.transform.localPosition = new Vector3(-0.2f + Vector33.x, Vector33.y, Vector33.z);
        }

        if (0.20f < floatOfMove && floatOfMove < 0.25f)
        {
            game.transform.localPosition = new Vector3(0.2f + Vector33.x, Vector33.y, Vector33.z);
        }

        game.GetComponent<SpriteRenderer>().color = new Color(255/255f, 65/255f, 65/255f, 255/255f);

        floatOfMove += Time.deltaTime;

        if(floatOfMoveMax < floatOfMove)
        {
            floatOfMove = 0;
            boolOfMove = false;
            game.transform.localPosition = Vector33;
            game.GetComponent<SpriteRenderer>().color = new Color(255 / 255f, 255 / 255f, 255 / 255f, 255 / 255f);
            Debug.Log(332);
        }
    }
}
