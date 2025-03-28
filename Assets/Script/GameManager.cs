using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public GameObject Solider;
    public GameObject Enemy;
    public GameObject EnemyInstantPlace;
    [Space(5)]

    [Tooltip("生成第一個敵人的時間0~10s")]
    [Range(0, 10)] public float startTime = 3;
    float RandomRange; // 我要的隨機生成敵人的時間

    [Tooltip("敵人數量")]public int TotalNumberOfEnemies = 15; // 总共的敌人数量


    public static GameManager Instance;
    public int EnemyKill = 0;

    public GameObject WinPanel;
    public GameObject LosePanel;
    private void Awake()
    {
        Instance = this;
        DontDestroyOnLoad(this.gameObject);
        Instance.TotalNumberOfEnemies = TotalNumberOfEnemies;

    }

    void Start()
    {
        
        StartCoroutine(GenerateEnemy());
    }

    // Update is called once per frame
    void Update()
    {
        // 玩家点击鼠标左键，生成士兵
        if (Input.GetMouseButtonDown(0))
        {
            RaycastHit2D[] ray = Physics2D.RaycastAll(Camera.main.ScreenToWorldPoint(Input.mousePosition),Vector3.forward,15);
            foreach (var ray2 in ray)
            {
                if (ray2.collider.CompareTag("space"))
                {
                    Instantiate(Solider,ray2.transform.position,Quaternion.identity);
                    ray2.transform.gameObject.SetActive(false);
                    break;
                }
            }
        }


        Win();


    }

    //生成敵人的協程
    IEnumerator GenerateEnemy()
    {
        yield return new WaitForSeconds(startTime);
        for (int i = 0; i < Instance.TotalNumberOfEnemies; i++)
        {
            
            Instantiate(Enemy,EnemyInstantPlace.transform.GetChild(Random.Range(0, 5)).transform.position , Quaternion.identity);
            RandomRange = Random.Range(0f, 8.0f);
            yield return new WaitForSeconds(RandomRange);
        }
    }




    public void Win()
    {
        if(EnemyKill == TotalNumberOfEnemies)
        {
            SceneManager.LoadScene(1);
            WinPanel.SetActive(true);
        }
    }
    public void Lose()
    {

        SceneManager.LoadScene(1);
        LosePanel.SetActive(true);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))
        {
            GameManager.Instance.Lose();
        }
    }
}
