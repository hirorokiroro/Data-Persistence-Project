using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public static class GlobalVariables
{
    public static string playerName = "nanashi";
    public static string bestPlayerName;
    public static int bestScore = 0;

}

public class MainManager : MonoBehaviour
{
    public Brick BrickPrefab;
    public int LineCount = 6;
    public Rigidbody Ball;

    public Text ScoreText;
    public GameObject GameOverText;
    [SerializeField] TextMeshProUGUI bestScoreText;
    public Text playerName;

    private bool m_Started = false;
    private int m_Points;//����̃Q�[���̍��v���_
    
    private bool m_GameOver = false;

    
    // Start is called before the first frame update
    void Start()
    {
        GlobalVariables.bestPlayerName = PlayerPrefs.GetString("keyBestPlayer");
        GlobalVariables.bestScore = PlayerPrefs.GetInt("keyBestScore");


        playerName.text = "Player : "+ GlobalVariables.playerName;
        bestScoreText.SetText("Best Score : " + GlobalVariables.bestPlayerName + " : " + GlobalVariables.bestScore);

        const float step = 0.6f;//�����т̃u���b�N�̊Ԋu�Ɛ���ς���
        int perLine = Mathf.FloorToInt(4.0f / step);//step���傫���قǉ����т̃u���b�N�̐�������
        
        int[] pointCountArray = new [] {1,1,2,2,5,5};//3��ނ̃u���b�N�̐F���Ƃɓ_���̍�������
        for (int i = 0; i < LineCount; ++i)
        {
            for (int x = 0; x < perLine; ++x)
            {
                Vector3 position = new Vector3(-1.5f + step * x, 2.5f + i * 0.3f, 0);//(x,y)���W
                var brick = Instantiate(BrickPrefab, position, Quaternion.identity);
                brick.PointValue = pointCountArray[i];//�u���b�N�̓_����z��̒l�Ƃ���
                brick.onDestroyed.AddListener(AddPoint);//brick�������Ȃ����Ƃ���AddPoint�Ƃ����C�x���g�𔭍s����
            }
        }
    }

    private void Update()
    {
        if (!m_Started)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                m_Started = true;
                float randomDirection = Random.Range(-1.0f, 1.0f);
                Vector3 forceDir = new Vector3(randomDirection, 1, 0);
                forceDir.Normalize();

                Ball.transform.SetParent(null);
                Ball.AddForce(forceDir * 2.0f, ForceMode.VelocityChange);
            }
        }
        else if (m_GameOver)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
            }
        }
    }

    void AddPoint(int point)//�u���b�N�������Ȃ�x�ɌĂяo�����֐�
    {
        m_Points += point;
        ScoreText.text = $"Score : {m_Points}";
    }

    public void GameOver()
    {
        m_GameOver = true;
        GameOverText.SetActive(true);

        if (GlobalVariables.bestScore < m_Points)
        {
            GlobalVariables.bestScore = m_Points;
            GlobalVariables.bestPlayerName = GlobalVariables.playerName;
        }
        bestScoreText.SetText("Best Score : " + GlobalVariables.bestPlayerName + " : " + GlobalVariables.bestScore);

        PlayerPrefs.SetInt("keyBestScore", GlobalVariables.bestScore);
        PlayerPrefs.SetString("keyBestPlayer", GlobalVariables.bestPlayerName);

        PlayerPrefs.Save();

    }
}
