using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[System.Serializable]
[CreateAssetMenu]
public class DataToSprite : ScriptableObject
{
    [System.Serializable]
    public class MappingData
    {
        public string letter;
        public Sprite letterImage;
        public Sprite letterCompleteImage;
        public Color circleColor;

    }

    public List<MappingData> mappingDataList = new List<MappingData>();
    public List<MappingData> selectedMappingDataList = new List<MappingData>();

  
}
