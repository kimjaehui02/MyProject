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
    /// 깜박이는 시간
    /// </summary>
    public float floatOfWarningColorMax;

    /// <summary>
    /// 깜박임을 위해 시간값을 더해주는 플로트변수
    /// </summary>
    public float floatOfWarningColor;

    /// <summary>
    /// 체력을 더 잃을거라는 경고입니다
    /// </summary>
    public int intOfWarning;

    /// <summary>
    /// 깜박임을 위해서 쓰는 방향전환용 불
    /// </summary>
    public bool boolOfWarningColor;

    [SerializeField]
    private int intOfHp;

    [SerializeField]
    private int intOfSta;

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

    }

    private void Update()
    {
        BarColorChange();
    }

    /// <summary>
    /// 바들의 색상을 바꿔줍니다
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

            // 현재 false   ++   >   true  가 되야함
            // 현재 true    --   <   false 가 되야함
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

    /// <summary>
    /// 체력데미지를 입습니다
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
    }

    /// <summary>
    /// 스태미너 데미지를 입습니다
    /// </summary>
    /// <param name="damage"></param>
    public void StaDamage(int damage = 1)
    {
        intOfSta += damage;

        //intOfSta %= listColorOfStaminaBar.Count;
    }

    public void Animinput()
    {
        for(int i = 0; i < listAnimatorOfSprite.Count; i++)
        {
            listAnimatorOfSprite[i].SetTrigger("Attack");

        }
    }
}
