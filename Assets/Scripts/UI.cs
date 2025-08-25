using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UI : MonoBehaviour
{
    public GameObject player1;
    public GameObject player2;
    public HurtBox hurtbox;
    // Start is called before the first frame update
    void Start()
    {
    
        Time.timeScale=1;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Restart() //Restart scene 
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void Menu() //Load in main menu
    {
        SceneManager.LoadSceneAsync(0);
    }
}
