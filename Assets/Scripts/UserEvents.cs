using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class UserEvents 
{
    public delegate void EnableSquareSelection();
    public static event EnableSquareSelection OnEnableSqaureSelection;


    public static void EnableSquareSelectionMethod()
    {
        if (OnEnableSqaureSelection != null)
            OnEnableSqaureSelection();
    }

    //----------------------------------

    public delegate void DisableSquareSelection();
    public static event DisableSquareSelection OnDisableSquareSelection;


    public static void DisableSquareSelectionMethod()
    {
        if (OnDisableSquareSelection != null)
            OnDisableSquareSelection();
    }

    //----------------------------------




    public delegate void SelectSquare(Vector3 position);
    public static event SelectSquare OnSelectSquare;


    public static void SelectSquareMethod(Vector3 position)
    {
        if (OnSelectSquare != null)
            OnSelectSquare(position);
    }

    //----------------------------------


    public delegate void SelectSquareFromIndex(int index);
    public static event SelectSquareFromIndex OnSelectSquareFromIndex;


    public static void SelectSquareFromIndexMethod(int index)
    {
        if (OnSelectSquareFromIndex != null)
            OnSelectSquareFromIndex(index);
    }


    public delegate void DeSelectSquare(Vector3 position);
    public static event DeSelectSquare OnDeSelectSquare;


    public static void DeSelectSquareMethod(Vector3 position)
    {
        if (OnDeSelectSquare != null)
            OnDeSelectSquare(position);
    }



    //----------------------------------



    public delegate void DeSelectSquareFromIndex(int index);
    public static event DeSelectSquareFromIndex OnDeSelectSquareFromIndex;


    public static void DeSelectSquareFromIndexMethod(int index)
    {
        if (OnDeSelectSquareFromIndex != null)
            OnDeSelectSquareFromIndex(index);
    }


    public delegate void BulkDeselect(string letter, Vector3 squarePosition, int squareIndex,  bool isSelected);
    public static event BulkDeselect OnBulkDeselect;


    public static void BulkDeselectMethod(string letter, Vector3 squarePosition, int squareIndex,  bool isSelected)
    {
        if (OnBulkDeselect != null)
            OnBulkDeselect(letter, squarePosition, squareIndex, isSelected);
    }


    public delegate void CheckSqaure(string letter,Vector3 squarePosition,int squareIndex, ref bool isSelected);
    public static event CheckSqaure OnCheckSqaure;


    public static void CheckSqaureMethod(string letter, Vector3 squarePosition, int squareIndex,ref bool isSelected)
    {
        if (OnCheckSqaure != null)
            OnCheckSqaure(letter, squarePosition,squareIndex,ref isSelected);
    }

    //----------------------------------



    public delegate void ClearSelection();
    public static event ClearSelection OnClearSelection;


    public static void ClearSelectionMethod()
    {
        if (OnClearSelection != null)
            OnClearSelection();
    }

    //----------------------------------

    public delegate void DrawLineFromIndex(int first,int second,GameObject linePrefab);
    public static event DrawLineFromIndex OnDrawLineFromIndex;


    public static void DrawLineFromIndexMethod(int first, int second, GameObject linePrefab)
    {
        if (OnDrawLineFromIndex != null)
            OnDrawLineFromIndex( first,  second,linePrefab);
    }



    //----------------------------------

    public delegate void StopChangeColorFromIndex(int index);
    public static event StopChangeColorFromIndex OnStopChangeColorFromIndex;


    public static void StopChangeColorFromIndexMethod(int index)
    {
        if (OnStopChangeColorFromIndex != null)
            OnStopChangeColorFromIndex(index);
    }



}
