using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class SceneController : MonoBehaviour
{
    // Fields to create the cards
    [SerializeField] private MemoryCard originalCard;
    [SerializeField] private Sprite[] images;

    // Field to add score
    //---[SerializeField] private TextMesh scoreLabel;
    //---[SerializeField] private TextMeshProUGUI scoreLabel;

    // Create grid to put the cards in
    public int gridRows = 2;
    public int gridCols = 4;
    public float header = 2f;
    public float margin = 1f;

    // Pair logic
    private MemoryCard _firstRevealed;
    private MemoryCard _secondRevealed;

    // Player score
    private int _score = 0, _pairsFound = 0;
    public float timeOutSeconds = 3.0f;
    float currentTime;
    private bool _paused;
    private UIController ui;

    

    // Start is called before the first frame update
    void Start()
    {   
        // Select random card
        //---int id = Random.Range(0, images.Length);
        //---originalCard.SetCard(id, images[id]);

        // Create grid to put cards in
        float totalheight = Camera.main.orthographicSize * 2;
        float totalwidth =  totalheight * Camera.main.aspect;
        float spacewidth = (totalwidth - 2 * margin) / gridCols;
        float spaceheight = (totalheight - header - margin) / gridRows;
        // Only pairs instead of completely random cards
        int[] ids = ShufflePairs(gridCols * gridRows);
        int index = 0;
        for (int i = 0; i < gridRows; i++) {
            for (int j = 0; j < gridCols; j++) {
                var card =  Instantiate<MemoryCard>(originalCard);
                int id = ids[index++];
                card.SetCard(id, images[id]);
                float posX = (j + 0.5f) * spacewidth - totalwidth / 2 + margin;
                float posY = (i + 0.5f) * spaceheight - totalheight / 2 + margin;
                card.transform.position = new Vector3(posX, posY, originalCard.transform.position.z);
            }
        }
    }

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape)) {
            paused = !paused;
        }
        if (paused) {
            return;
        }
        currentTime -= Time.deltaTime;
        if(currentTime < 0) {
            if(_score > 0) {
                _score--;
                ui.SetScore(_score);
            }
            ResetTimeOut();
        }
        ui.SetTimeOutValue(currentTime / timeOutSeconds);
    }

    public bool paused 
    {
        get { return _paused; }
        set { _paused = value;
              Time.timeScale = value ? 0.0f : 1.0f; 
              ui.ShowPauseMenu(value); }
    }

    public bool gameover
    {
        get { return _pairsFound == gridCols * gridRows / 2; }
    }

    public bool canReveal 
    {
        get { return _secondRevealed == null && !paused; }
    }

    void ResetTimeOut()
    {
        currentTime = timeOutSeconds;
    }

    private void Awake()
    {
        ui = GetComponent<UIController>();
        ResetTimeOut();
        ui.ShowPauseMenu(false);
    }

    public void CardRevealed(MemoryCard card) 
    {
        if(_firstRevealed == null) {
            ResetTimeOut();
            _firstRevealed = card;
        } 
        else {
            _secondRevealed = card;
            StartCoroutine(CheckMatch());
            //Debug.Log(_firstRevealed.id == _secondRevealed.id ? "Pair!" : "Try again!");
        }
    }

    // Method to make the pairs shuffled
    private int[] ShufflePairs(int numCards)
    {
        int[] ids =  new int[numCards];
        for (int i = 0; i < numCards; i++) {
            ids[i] = i / 2;
        }
        for (int i = 0 ; i < numCards - 1; i++) {
            int r =  Random.Range(i, numCards);
            int tmp = ids[i];
            ids[i] = ids[r];
            ids[r] = tmp;
        }
        return ids;
    }

    // Method to see if two cards match
    private IEnumerator CheckMatch()
    {
        if(_firstRevealed.id == _secondRevealed.id) {
            _score++;
            //---scoreLabel.text = "Score: " +  _score;
            //---scoreLabel.text = _score.ToString();
            ui.SetScore(_score);
            _pairsFound++;
            if(gameover) {
                ui.EnableTimeOutBar(false);
                paused = true;
            }
        }
        else {
            yield return new WaitForSeconds(.5f);
            _firstRevealed.Unreveal();
            _secondRevealed.Unreveal();
        }
        _firstRevealed = _secondRevealed = null;
    }

    // Restart game
    public void Restart() 
    {
        SceneManager.LoadScene("MemoryScene");
        paused = false;
    }

    // Quit game
    public void Quit() 
    {
        #if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
        #else
            Application.Quit();
        #endif
    }
}
