using UnityEngine;
using UnityEngine.UI;

public class SoliderController : MonoBehaviour
{
    //士兵有屬性:攻擊力、生命值
    [Tooltip("攻擊力")]public static int attack = 20;
    [Tooltip("生命值")]public static int Max_hp = 200;
    public int currentHP;


    Animator anim;

    public GameObject Arrow;
    private GameObject TempArrow;
    Vector3 ArrowStartPos = new Vector3(0.4f, 0, 0);

    //控制血條的ui
    public GameObject HPBar;
    Slider HPSlider;


    public void getDamage(int damage)
    {
        this.currentHP -= damage;
        IfYouDie();
    }
    private void Awake()
    {
        anim = GetComponent<Animator>();
        TempArrow = Instantiate(Arrow,this.transform.position+ArrowStartPos,Quaternion.identity);
        TempArrow.SetActive(false);

        this.currentHP = Max_hp;
        
        HPSlider = HPBar.GetComponent<Slider>();
    }

    private void FixedUpdate()
    {
        ///檢測是否有敵人
        RaycastHit2D[] hit = Physics2D.RaycastAll(transform.position, Vector2.right, 5);
        anim.SetBool("FindEnemy", false);
        foreach (var hit2d in hit) 
        {
            if(hit2d.collider.CompareTag("Enemy"))
            {
                anim.SetBool("FindEnemy", true);

                break;
            }
        }

        UPdateHPUI();
    }

    public void Shoot()
    {
        TempArrow.transform.position = transform.position + ArrowStartPos;
        TempArrow.SetActive(true);
    }

    private void IfYouDie()
    {
        if (currentHP <= 0)
        {
            Destroy(TempArrow);
            Destroy(this.gameObject.transform.parent.gameObject);
        }
    }

    private void UPdateHPUI()
    {
        HPSlider.value = (float)currentHP / Max_hp;
    }

}
