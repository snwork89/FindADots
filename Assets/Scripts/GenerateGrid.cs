using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class GenerateGrid : MonoBehaviour
{
    public GameData currentGameData;
    public GameObject gridSquarePrefab;
    public DataToSprite mappingData;

    public float squareOffset =  40.0f;
public float topPosition;
    
private List<GameObject> _squareList = new List<GameObject>();


    private void Start()
    {
        SpawnGridSquares();
        SetSquarePosition();
    }

    private void OnEnable()
    {
        UserEvents.OnDrawLineFromIndex += DrawLineFromIndex;
        UserEvents.OnStopChangeColorFromIndex += StopChangeColorFromIndex;
    }

    private void OnDisable()
    {
        UserEvents.OnDrawLineFromIndex -= DrawLineFromIndex;
        UserEvents.OnStopChangeColorFromIndex -= StopChangeColorFromIndex;
    }

    private void SetSquarePosition()
    {
        var squareRect = _squareList[0].GetComponent<SpriteRenderer>().sprite.rect;
        var squareTransform = _squareList[0].GetComponent<Transform>();
        string uniqueCharacters = "";
        var offSet = new Vector2
        {
            x = (squareRect.width * squareTransform.localScale.x + squareOffset) * 0.01f,
        y = (squareRect.height * squareTransform.localScale.y + squareOffset) * 0.01f
        };

        var startPosition = GetFirstSquarePosition();

        int columnNumber = 0;
        int rowNumber = 0;
        for(int i=0;i<_squareList.Count;i++)
        {


           
            if (rowNumber + 1 > currentGameData.selectedDotsData.Rows)
            {
                columnNumber++;
                rowNumber = 0;
            }
            var positionX = startPosition.x + offSet.x * columnNumber;
            var positionY = startPosition.y - offSet.y * rowNumber;


            
            _squareList[i].GetComponent<Transform>().position = new Vector2(positionX, positionY);
            rowNumber++;
        }



        for (int i = 0; i < currentGameData.selectedDotsData.Columns; i++)
        {
            for (int j = 0; j < currentGameData.selectedDotsData.Board[i].Size; j++)
            {
                if (!uniqueCharacters.Contains(currentGameData.selectedDotsData.Board[i].Row[j]))
                {
                  
                    uniqueCharacters += currentGameData.selectedDotsData.Board[i].Row[j];
                    DotsData.InitPosition currentChar = currentGameData.selectedDotsData.startPosition.Find(item => item.mappingLetter == currentGameData.selectedDotsData.Board[i].Row[j]);

                    Debug.Log("current Cjhar init index" + currentChar.initialIndex);
                    _squareList[currentChar.initialIndex].GetComponent<GridSquare>().SetIsInitIndex(true,currentChar.initialIndex);
                    _squareList[currentChar.initialIndex].GetComponent<GridSquare>().StartChangeColor();
                    /*_squareList[currentChar.initialIndex].GetComponent<GridSquare>().MakeInitialSelection();*/

                }

            }

        }
     
    }

    private Vector2 GetFirstSquarePosition()
    {
        var startPosition = new Vector2(0f, transform.position.y);
        var squareRect = _squareList[0].GetComponent<SpriteRenderer>().sprite.rect;
        var squareTransform = _squareList[0].GetComponent<Transform>();
        var squareSize = new Vector2(0f, 0f);

        squareSize.x = squareRect.width * squareTransform.localScale.x +squareOffset;
        squareSize.y = squareRect.height * squareTransform.localScale.y+squareOffset;
   

        var midWidthPostion = (((currentGameData.selectedDotsData.Columns - 1) * squareSize.x) / 2) * 0.01f;
        var midHeightPostion = (((currentGameData.selectedDotsData.Rows - 1 )* squareSize.y) / 2) * 0.01f;


        startPosition.x = (midWidthPostion != 0) ? midWidthPostion * -1 : midWidthPostion;
        startPosition.y += midHeightPostion;


        return startPosition;
    }
    private void SpawnGridSquares()
    {
        if (currentGameData != null)
        {

            var squareScale = GetSquareScale(new Vector3(1.5f,1.5f,0.1f));
            foreach(var squares in currentGameData.selectedDotsData.Board) 
            { 

                foreach(var squareLetter in squares.Row)
                {
                    Debug.Log("square letter is" + squareLetter);
                   
                    var normalLetterData = mappingData.mappingDataList.Find(data => data.letter == squareLetter);
                    var selectedLetterData = mappingData.selectedMappingDataList.Find(data => data.letter == squareLetter);
                    _squareList.Add(Instantiate(gridSquarePrefab));
                    _squareList[_squareList.Count - 1].GetComponent<GridSquare>().SetSprite(normalLetterData, selectedLetterData);
                    _squareList[_squareList.Count - 1].transform.SetParent(this.transform);
                    _squareList[_squareList.Count - 1].GetComponent<Transform>().position = new Vector3(0f, 0f, 0f);
                    _squareList[_squareList.Count - 1].transform.localScale = squareScale;
                    _squareList[_squareList.Count - 1].GetComponent<GridSquare>().SetIndex(_squareList.Count - 1);

               

                }
            }

           
        }
    }


    private void StopChangeColorFromIndex(int index)
    {
        _squareList[index].GetComponent<GridSquare>().StopChangeColor();
    }
    private void DrawLineFromIndex(int first,int second,GameObject linePrefab)
    {

        Debug.Log("drawline from index called" + first+"seee" + second);
        
        Vector3 firstPosition = _squareList[first].GetComponent<GridSquare>().transform.position;
        Vector3 secondPosition = _squareList[second].GetComponent<GridSquare>().transform.position;




        LineRenderer line = linePrefab.GetComponent<LineRenderer>(); ;
        Vector3[] currentLinePosition = new Vector3[line.positionCount];
        line.GetPositions(currentLinePosition);

        int count = line.positionCount;

        List<Vector3> currentLinePositionList =   currentLinePosition.ToList();

        if (currentLinePositionList.FindIndex(el => el.x == firstPosition.x && el.y == firstPosition.y)==-1)
        {
            currentLinePositionList.Add(firstPosition);
        }

        if (currentLinePositionList.FindIndex(el => el.x == secondPosition.x && el.y == secondPosition.y) == -1)
        {
            currentLinePositionList.Add(secondPosition);
        }


        line.positionCount = 0;
        line.positionCount = currentLinePositionList.Count;
        line.SetPositions(currentLinePositionList.ToArray());
      
        /*line.SetPosition(count, firstPosition);
        line.SetPosition(count + 1, secondPosition);*/


        line.useWorldSpace = true;



    }
    private Vector3 GetSquareScale(Vector3 defaultScale) {
        var finalScale = defaultScale;
        var adjustment = 0.01f;

        while (ShouldScaleDown(finalScale))
        {
            finalScale.x -= adjustment;
            finalScale.y -= adjustment;
            if (finalScale.x <= 0 || finalScale.y <= 0)
            {
                finalScale.x = adjustment;
                finalScale.y = adjustment;
                return finalScale;
            }
        }
        return finalScale;
    }

    private bool ShouldScaleDown(Vector3 targetScale)
    {
        var squareRect = gridSquarePrefab.GetComponent<SpriteRenderer>().sprite.rect; 
        var squareSize = new Vector2(0f, 0f);
        var startPosition = new Vector2(0f, 0f); 
        squareSize.x = (squareRect.width * targetScale.x) + squareOffset; 
        squareSize.y = (squareRect.height * targetScale.y) + squareOffset;

        var midWidthPosition = ((currentGameData.selectedDotsData.Columns * squareSize.x) / 2) * 0.01f;
        var midWidthHeight = ((currentGameData.selectedDotsData.Rows * squareSize.y) / 2) * 0.01f;

        startPosition.x = (midWidthPosition != 0) ? midWidthPosition * -1 : midWidthPosition;
        startPosition.y = midWidthHeight;


        return startPosition.x < GetHalfScreenWidth()*-1 || startPosition.y > topPosition;
    }

    private float GetHalfScreenWidth()
    {
        float height = Camera.main.orthographicSize*2;
       
        float width = (1.7f * height) * Screen.width / Screen.height;

        Debug.Log("width is"+ width);
        
        return width / 2;
    }
}
