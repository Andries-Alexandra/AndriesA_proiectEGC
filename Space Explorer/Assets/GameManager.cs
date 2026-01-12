using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    //singleton ul care permite accesul global la GameManager celorlalte scripturi
    public static GameManager instance;

    public int score = 0;
    public int totalStars = 5;

    //interfata ui 
    public TMP_Text scoreText;
    public GameObject winMenu;
    public GameObject startMenu;

    public PlanetInfo planetScript;

    void Awake()
    {
        if (instance == null) instance = this;
    }

    void Start()
    {
        Time.timeScale = 0; //opreste jocul pana la apasarea butonului play

        //facem cursorul vizibil si liber pentru meniul de start
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;

        //gestionam meniurile de start si win
        if (startMenu != null) startMenu.SetActive(true);
        if (winMenu != null) winMenu.SetActive(false);

        //ascundem numele planetelor ca sa nu se suprapuna cu meniul
        if (planetScript != null)
        {
            planetScript.enabled = false;
        }

        // Cauta toate obiectele care au scriptul "Collectible" si ne zice cate sunt.
        totalStars = FindObjectsByType<Collectible>(FindObjectsSortMode.None).Length;

        UpdateScoreText(); //afisare scor initial
    }

    //functie apelata la apasarea butonului play
    public void StartGame()
    {
        Time.timeScale = 1; //porneste jocul

        //blocheaza cursorul in centru (pt controlul rachetei) si il ascunde pentru gameplay
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        //ascundem meniul de start
        if (startMenu != null) startMenu.SetActive(false);

        //afisam numele planetelor inapoi
        if (planetScript != null)
        {
            planetScript.enabled = true;
        }
    }

    //pt butonul de play again
    public void RestartLevel()
    {
        //reincarca scena curenta de la zero
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        Time.timeScale = 1;
    }

    //pt butonul de exit
    public void QuitGame()
    {
        Debug.Log("Jocul s-a inchis!");
        Application.Quit(); //inchide jocul
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#endif
    }

    public void AddScore()
    {
        score++; //creste scorul
        UpdateScoreText(); //actualizeaza textul scorului
        if (score >= totalStars) WinGame(); //verifica daca jucatorul a castigat
    }

    //actualizeaza textul scorului in UI
    void UpdateScoreText()
    {
        if (scoreText != null) scoreText.text = "Collected stars: " + score + " / " + totalStars;
    }

    //functie apelata de collectible.cs cand lovesc o stea
    void WinGame()
    {
        if (winMenu != null) winMenu.SetActive(true); //afiseaza meniul de win
        Time.timeScale = 0; //opreste jocul

        //Eliberam cursorul pentru meniul de win
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }
}