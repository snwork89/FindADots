using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameScore 
{
    public string letter;
    public List<int> selectedSquareIndexes;
    public int totalLetterLength;
    public GameObject linePrefab;
    public GameObject scoreVisual;
    public GameScore(string letter,List<int> selectedSquare,GameObject line,int totalLetter,GameObject score)
    {
        this.letter = letter;
        this.selectedSquareIndexes = selectedSquare;
        this.linePrefab = line;
        this.scoreVisual = score;
        this.totalLetterLength = totalLetter;
        
    }
}
