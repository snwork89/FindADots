using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridSquare : MonoBehaviour
{
   private int squareIndex;


    private DataToSprite.MappingData _mappingData;
    private DataToSprite.MappingData _selectedMappingDataList;
    private SpriteRenderer _displayedImage;
    private int _index = -1;
    private bool _isInitIndex;
    private bool _selected;


    private void Awake()
    {
        _selected = false;
    
        _displayedImage = GetComponent<SpriteRenderer>();

        

    }


    public void SetIsInitIndex (bool value,int index)
    {
    
       
            _isInitIndex = value;
            _selected = true;
            Debug.Log("index for" + squareIndex + "value is" + _selected + "o" + value);
            GetComponent<SpriteRenderer>().sprite = _selectedMappingDataList.letterImage;
       
  
        
    }

    public void SetIndex(int index)
    {
        _index = index;
  
    }

    public int GetIndex()
    {
        return _index;
    }

    public void OnEnableSquareSelection()
    {
    
        _selected = false;
    }

    public void OnDisableSquareSelection()
    {
      
        _selected = false;
        
    }
    public void SelectSquare(Vector3 position)
    {

        if (this.gameObject.transform.position == position)

        {
           
            _displayedImage.sprite = _selectedMappingDataList.letterImage;
      

        }
           
    }

    public void SelectSqaureFromIndex(int index)


    {

      
        if(_index==index && _index != -1)
        {
            _selected = true;
            _displayedImage.sprite = _selectedMappingDataList.letterImage;
        }
    }

    public void DeSelectSqaureFromIndex(int index)
    {
        if (_index == index && _index != -1 && !_isInitIndex)
        {
            _selected = false;
            _displayedImage.sprite = _mappingData.letterImage;
        }
    }

    public void DeSelectSquare(Vector3 position)
    {

        if (this.gameObject.transform.position == position)

        {

            _displayedImage.sprite = _mappingData.letterImage;

        }

    }


    public void MakeInitialSelection()


    {

        
        _selected = true;
            GetComponent<SpriteRenderer>().sprite = _selectedMappingDataList.letterImage;
        

    }


    IEnumerator ChangeColor()
    {
        Color targetColor = new Color(0.80f, 0.80f, 0.80f);
    
        while (true)
        {
     
                _displayedImage.color = targetColor;

                yield return new WaitForSeconds(0.2f);

            _displayedImage.color = Color.white;

                yield return new WaitForSeconds(0.2f);
           










        }
    }

    public void StartChangeColor()
    {

        StartCoroutine("ChangeColor");
    }

    public void StopChangeColor()
    {
        if (_isInitIndex)
        {
            StopCoroutine("ChangeColor");
            _displayedImage.color = Color.white;
        }
    
    }
    private void OnEnable()
    {
        UserEvents.OnEnableSqaureSelection += OnEnableSquareSelection;
        UserEvents.OnDisableSquareSelection += OnDisableSquareSelection;
        UserEvents.OnSelectSquare += SelectSquare;
        UserEvents.OnDeSelectSquare += DeSelectSquare;
        UserEvents.OnSelectSquareFromIndex += SelectSqaureFromIndex;
        UserEvents.OnDeSelectSquareFromIndex += DeSelectSqaureFromIndex;
    }

    private void OnDisable()
    {
        UserEvents.OnEnableSqaureSelection -= OnEnableSquareSelection;
        UserEvents.OnDisableSquareSelection -= OnDisableSquareSelection;
        UserEvents.OnSelectSquare -= SelectSquare;
        UserEvents.OnDeSelectSquare -= DeSelectSquare;
        UserEvents.OnSelectSquareFromIndex -= SelectSqaureFromIndex;
        UserEvents.OnDeSelectSquareFromIndex -= DeSelectSqaureFromIndex;
    }


    public void SetSprite(DataToSprite.MappingData mappingData,DataToSprite.MappingData selectedMappingDataList)
    {
        _mappingData = mappingData;
        _selectedMappingDataList = selectedMappingDataList;
        GetComponent<SpriteRenderer>().sprite = mappingData.letterImage;


    }   

   
    public void CheckSquare()
    {
      

            UserEvents.CheckSqaureMethod(_mappingData.letter, gameObject.transform.position, _index,ref _selected);
      
    }
    // private void OnMouseDown()
    // {
    //     OnEnableSquareSelection();
    //     UserEvents.EnableSquareSelectionMethod();
    //     CheckSquare();
    //    _displayedImage.sprite = _selectedMappingDataList.letterImage;
    // }

    private void OnMouseDown()
    {
        UserEvents.BulkDeselectMethod(_mappingData.letter, gameObject.transform.position, _index, _selected);
    }


    private void OnMouseEnter()
    {
     
        CheckSquare();
    }

    
}
