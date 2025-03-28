using System;
using UnityEngine;
using UnityEngine.UI;

public class EnemyController : MonoBehaviour
{
    //敵人有屬性:速度、攻擊力、生命值
    [Tooltip("速度")]public static float speed = 0.5f;
    [Tooltip("攻擊力")]public static int attack = 60;
    [Tooltip("生命值")]public static int Max_hp = 100;
    public int currentHP;

    bool findSolider;
    Animator anim;

    public GameObject AttackRange;
    Collider2D AtkCol;

    //製作血條的UI
    public GameObject HPBar;
    Slider HPSlider;



    public void getDamage(int damage)
    {
        this.currentHP -= damage;
        UPdateHPUI();
        IfYouDie();
    }
    private void Awake()
    {
        AtkCol =AttackRange.GetComponent<Collider2D>();
        AttackRange.SetActive(false);
        this.currentHP = Max_hp;
        anim = GetComponent<Animator>();

        HPSlider = HPBar.GetComponent<Slider>();

    }

    private void FixedUpdate()
    {
        ///檢測是否有士兵靠近
        RaycastHit2D[] ray = Physics2D.RaycastAll(this.transform.position, Vector2.left,0.5f);
        anim.SetBool("FindSolider", false);
        foreach(var ray2 in ray)
        {
            if(ray2.collider.CompareTag("Solider"))
            {
                anim.SetBool("FindSolider" ,true);
                findSolider = true;
                break;
            }
            else
            {
                
                findSolider = false;
            }

           
        }

        ///敵人的移動
        if (!findSolider)
        {
            this.transform.parent.transform.position += Vector3.left * Time.fixedDeltaTime * speed;
        }

        EnemyWin();
    }

    public void AttackStart()
    {
        AttackRange.SetActive(true);
    }
    public void AttackEnd()
    {
        AttackRange.SetActive(false);
    }
    
    private void IfYouDie()
    {
        if (currentHP <= 0)
        {
            GameManager.Instance.EnemyKill++;
            GameManager.Instance.WinOrLose();
            Destroy(this.transform.parent.gameObject);
        }
    }

    private void UPdateHPUI()
    {
        HPSlider.value = (float)currentHP / Max_hp;
    }

    void EnemyWin()
    {
        if(transform.position.x <= -4.5f)
        {
            GameManager.Instance.WinOrLose();
        }
    }

}


