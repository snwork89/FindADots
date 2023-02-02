using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ScoreButton : MonoBehaviour
{

    public TextMeshProUGUI scoreText;
    public Image image;
    private Sprite completedImage;
    private string currentLetter;
    
  
    // Start is called before the first frame update
    void Start()
    {
       
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetCompletedImage(Sprite i)
    {
        completedImage = i;
    }
    public void SetCurrentLetter(string s)
    {
        currentLetter = s;
    }

    public void ChangeText(string text)
    {
        scoreText.text = text;
    }

    public void SetSprite(Sprite i)
    {
        image.sprite = i;
    }

    public void ScoreCompleted()
    {
        image.sprite = completedImage;
        image.GetComponent<RectTransform>().localScale = new Vector3(1.4f, 1.4f, 1.4f);
        scoreText.enabled = false;
    }
}
