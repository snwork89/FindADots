using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DotsChecker : MonoBehaviour
{
     public GameData currentGameData;
    private string uniqueCharacters="";
    public GameObject linePrefab;
    public Transform parentOfScrollList;
    public DataToSprite mappingData;
    public GameObject scoreButtonPrefab;
    private List<GameScore> gameScoresList = new List<GameScore>();
    private void OnEnable()
    {
        UserEvents.OnCheckSqaure += SquareSelected;
        UserEvents.OnBulkDeselect += BulkDeselect;
    }
    private void OnDisable()
    {
        UserEvents.OnCheckSqaure -= SquareSelected;
        UserEvents.OnBulkDeselect -= BulkDeselect;
    }

   private Color ReturnLineColor(string c)
    {
        Color color = Color.blue;
        if (c == "1")
        {
            color = new Color(92.0f/255.0f, 185.0f/255.0f, 233.0f/255.0f);
           

        }
        else if (c == "0")
        {
            color = new Color(60.0f/255.0f, 220.0f/255.0f, 173.0f/255.0f);
        }
      

        return color ;
    }
    void Awake()
    {

        for (int i = 0; i < currentGameData.selectedDotsData.Columns; i++)
        {
            for (int j = 0; j < currentGameData.selectedDotsData.Board[i].Size; j++)
            {
                if (!uniqueCharacters.Contains(currentGameData.selectedDotsData.Board[i].Row[j])){
                    uniqueCharacters += currentGameData.selectedDotsData.Board[i].Row[j];
                }
                
            }
                
        }
        for(int i = 0; i < uniqueCharacters.Length; i++)
        {
            DotsData.InitPosition  currentChar =  currentGameData.selectedDotsData.startPosition.Find(item => item.mappingLetter == uniqueCharacters[i].ToString());
            int totalLengthForCurrentLetter = 0;


            for (int index = 0; index < currentGameData.selectedDotsData.Columns; index++)
            {
                for (int j = 0; j < currentGameData.selectedDotsData.Board[index].Size; j++)
                {
                    if (currentGameData.selectedDotsData.Board[index].Row[j]==currentChar.mappingLetter)
                    {
                        totalLengthForCurrentLetter = totalLengthForCurrentLetter + 1;
                    }

                }

            }
            GameObject obj = Instantiate(scoreButtonPrefab, parentOfScrollList);
            var selectedLetterData = mappingData.selectedMappingDataList.Find(data => data.letter == uniqueCharacters[i].ToString());
            ScoreButton scoreButton = obj.GetComponent<ScoreButton>();
            scoreButton.ChangeText("1 / " + " " + totalLengthForCurrentLetter);
            scoreButton.SetSprite(selectedLetterData.letterImage);
            scoreButton.SetCompletedImage(selectedLetterData.letterCompleteImage);
            scoreButton.SetCurrentLetter(selectedLetterData.letter);
            GameObject lineGameObject =  Instantiate(linePrefab);
         
          

            lineGameObject.GetComponent<LineRenderer>().startColor = ReturnLineColor(selectedLetterData.letter);
            lineGameObject.GetComponent<LineRenderer>().endColor = ReturnLineColor(selectedLetterData.letter);


          


            gameScoresList.Add(new GameScore(currentChar.mappingLetter, new List<int> { currentChar.initialIndex}, lineGameObject,totalLengthForCurrentLetter,obj));
           
            
            
          
        }

        

       
     
    }

    private bool IsLastColumnFirstRowElement(int firstIndex,int secondIndex)
    {
        if((firstIndex % currentGameData.selectedDotsData.Rows == 3) && (secondIndex % currentGameData.selectedDotsData.Rows == 0))
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    private bool IsElementSideBySide(int firstElementIndex,int secondElementIndex)
    {
        Debug.Log("currentGameData.selectedDotsData.Rows" + currentGameData.selectedDotsData.Rows);


        if (Mathf.Abs(firstElementIndex-secondElementIndex)==currentGameData.selectedDotsData.Rows || (!IsLastColumnFirstRowElement(firstElementIndex,secondElementIndex) && !IsLastColumnFirstRowElement( secondElementIndex,firstElementIndex) && Mathf.Abs(secondElementIndex-firstElementIndex)==1))
        {

            return true;
        }
        else
        {
           
            return false;
        }
    }

    private void SquareSelected(string letter, Vector3 squarePosition, int squareIndex, ref bool isSelected)
    {


        GameScore elementForCurrentLetter = gameScoresList.Find(item => item.letter == letter);
        if (isSelected==false && IsElementSideBySide(elementForCurrentLetter.selectedSquareIndexes[^1], squareIndex))
        {

         
            isSelected = true;
            elementForCurrentLetter.selectedSquareIndexes.Add(squareIndex);
          
            elementForCurrentLetter.scoreVisual.GetComponent<ScoreButton>().ChangeText(elementForCurrentLetter.selectedSquareIndexes.Count +" / " + " " + elementForCurrentLetter.totalLetterLength);
            Debug.Log("inside if"+elementForCurrentLetter.selectedSquareIndexes.Count);
            UserEvents.SelectSquareMethod(squarePosition);
           
            UserEvents.DrawLineFromIndexMethod(elementForCurrentLetter.selectedSquareIndexes[elementForCurrentLetter.selectedSquareIndexes.Count - 2], elementForCurrentLetter.selectedSquareIndexes[elementForCurrentLetter.selectedSquareIndexes.Count - 1],elementForCurrentLetter.linePrefab);

            int selectedSquareCount = elementForCurrentLetter.selectedSquareIndexes.Count;
            if (selectedSquareCount == 2)
            {
                UserEvents.StopChangeColorFromIndexMethod(elementForCurrentLetter.selectedSquareIndexes[0]);
            }
            if (selectedSquareCount == elementForCurrentLetter.totalLetterLength)
            {
                elementForCurrentLetter.scoreVisual.GetComponent<ScoreButton>().ScoreCompleted();
            }

        }
      


    }

    private void BulkDeselect(string letter, Vector3 squarePosition, int squareIndex, bool isSelected)
    {
        GameScore elementForCurrentLetter = gameScoresList.Find(item => item.letter == letter);
        Debug.Log("value of isSelected" + isSelected+"squyas"+squareIndex);
        if (isSelected == true)
        {
         

            int indexForCurrentElement = elementForCurrentLetter.selectedSquareIndexes.FindIndex(item => item == squareIndex);

          
            if (indexForCurrentElement != -1)
            {


                List<int> deselectedIndex = elementForCurrentLetter.selectedSquareIndexes.GetRange(indexForCurrentElement+1, elementForCurrentLetter.selectedSquareIndexes.Count - indexForCurrentElement-1);
               
                elementForCurrentLetter.selectedSquareIndexes = elementForCurrentLetter.selectedSquareIndexes.GetRange(0, indexForCurrentElement + 1);

                elementForCurrentLetter.scoreVisual.GetComponent<ScoreButton>().ChangeText(elementForCurrentLetter.selectedSquareIndexes.Count + " / " + " " + elementForCurrentLetter.totalLetterLength);
                LineRenderer line = elementForCurrentLetter.linePrefab.GetComponent<LineRenderer>();
               
                line.positionCount = 0;
               /* line.positionCount = elementForCurrentLetter.selectedSquareIndexes.Count;*/

                for (int i = 0; i < elementForCurrentLetter.selectedSquareIndexes.Count; i++)

                {
                    if (i != elementForCurrentLetter.selectedSquareIndexes.Count - 1)
                    {
                        UserEvents.DrawLineFromIndexMethod(elementForCurrentLetter.selectedSquareIndexes[i], elementForCurrentLetter.selectedSquareIndexes[i + 1], elementForCurrentLetter.linePrefab);
                    }
                
                }


                for (int i = 0; i < deselectedIndex.Count; i++)

                {
                    UserEvents.DeSelectSquareFromIndexMethod(deselectedIndex[i]);
                }

            }
   

        }
        
    }


    private void DrawLines()
    {
        
     

     
        foreach (var item in gameScoresList)
        {
            for(int index = 0; index < item.selectedSquareIndexes.Count; index++)
            {
                if (index != item.selectedSquareIndexes.Count - 1)
                {
                    UserEvents.DrawLineFromIndexMethod(item.selectedSquareIndexes[index], item.selectedSquareIndexes[index + 1],item.linePrefab);
                }

                

            }
          

        }

    }




}
