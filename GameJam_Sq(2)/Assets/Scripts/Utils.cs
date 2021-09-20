using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public static class Utils
{
    public struct TMProName
    {
        public string name;
        public Color32 color;
        public int numOfWords;
        public int firstWordIdx;
        public TMProName(string _name, Color32 _color, int _numOfWords, int _firstWordIdx) 
            { name = _name;  color = _color; numOfWords = _numOfWords; firstWordIdx = _firstWordIdx; }
        public void SetFirstWordIdx(int _idx)
        {
            firstWordIdx = _idx;
        }
         
    }
    public struct TMProWord
    {
        public Color32 color;
        public int idx;
        public int firstCharIdx;
        public TMProWord(Color32 _color, int _idx, int _firstCharIdx)
            { color = _color; idx = _idx; firstCharIdx = _firstCharIdx; }
    };
    public struct TMProChar
    {
        public Color32 color;
        public int idx;
        public TMProChar(Color32 _color, int _idx)
            { color = _color; idx = _idx; }
    }

    public static int RandomizePositiveNegative()
    {
        float rnd = Random.RandomRange(-1, 1);

        if (rnd < 0)
            return -1;
        else
            return 1;
    }
    
    public static int FindInList<T>(List<T> listToSearch, T elementToFind)
    {
        for(int i = 0; i < listToSearch.Count; i++)
        {
            if (listToSearch[i].Equals(elementToFind))
                return i;
        }

        return -1;
    }


    public static void PaintTMProChars(TextMeshProUGUI _text, Color32 _color, int _from, int _to)
    {
        // Recordar poner antes de la funcion
        //_text.ForceMeshUpdate();

        if (_from >= 0 && _to <= _text.textInfo.characterCount)
        {
            // Per si fa falta solucionar el bug amb el segon if... 
            Color32 original0Color = _text.textInfo.characterInfo[0].color;

            for (int i = _from; i < _to; i++)
            {
                TMP_CharacterInfo charInfo = _text.textInfo.characterInfo[i];
                int vertexIdx = charInfo.vertexIndex;

                for (int idx = 0; idx < 4; idx++)
                {
                    _text.textInfo.meshInfo[charInfo.materialReferenceIndex].colors32[charInfo.vertexIndex + idx] = _color;
                }

            }

            // Per a solucionar un bug que pinta la primera lletra per algun motiu
            if (_from > 0)
            {
                TMP_CharacterInfo charInfo0 = _text.textInfo.characterInfo[0];
                for (int idx = 0; idx < 4; idx++)
                {
                    _text.textInfo.meshInfo[charInfo0.materialReferenceIndex].colors32[charInfo0.vertexIndex + idx] = original0Color;
                }
            }

        }

        // Recordar meter después de la funcion
        //_text.UpdateVertexData();

    }

    public static void PaintTMProWords(TextMeshProUGUI _text, Color32 _color, int _from, int _to)
    {
        // Recordar poner antes de la funcion
        //_text.ForceMeshUpdate();

        if (_from >= 0 && _to <= _text.textInfo.wordCount)
        {
            for (int i = _from; i < _to; i++)
            {
                TMP_WordInfo wordInfo = _text.textInfo.wordInfo[i];
                for (int j = 0; j < wordInfo.characterCount; j++)
                {
                    TMP_CharacterInfo charInfo = _text.textInfo.characterInfo[wordInfo.firstCharacterIndex + j];
                    int vertexIdx = charInfo.vertexIndex;

                    for (int idx = 0; idx < 4; idx++)
                    {
                        _text.textInfo.meshInfo[charInfo.materialReferenceIndex].colors32[charInfo.vertexIndex + idx] = _color;
                    }
                }
            }

        }

        // Recordar meter después de la funcion
        //_text.UpdateVertexData();

    }
    public static void PaintTMProWords(TextMeshProUGUI _text, List<TMProName> _namesToChange)
    {
        // Recordar poner antes de la funcion
        //_text.ForceMeshUpdate();

        //if (_from >= 0 && _to <= _text.textInfo.wordCount)
        for (int i = 0; i < _namesToChange.Count; i++)
        {
            for (int j = 0; j < _namesToChange[i].numOfWords; j++)
            {
                TMP_WordInfo wordInfo = _text.textInfo.wordInfo[_namesToChange[i].firstWordIdx + j];
                for (int k = 0; k < wordInfo.characterCount; k++)
                {
                    TMP_CharacterInfo charInfo = _text.textInfo.characterInfo[wordInfo.firstCharacterIndex + k];
                    int vertexIdx = charInfo.vertexIndex;

                    for (int idx = 0; idx < 4; idx++)
                    {
                        _text.textInfo.meshInfo[charInfo.materialReferenceIndex].colors32[charInfo.vertexIndex + idx] = _namesToChange[i].color;
                    }
                }
            }
        }

        // Recordar meter después de la funcion
        //_text.UpdateVertexData();

    }


    public static int GetWordsAmmount(string _text)
    {
        string[] tmpText = _text.Split();

        if (tmpText[tmpText.Length - 1] == "")
            return tmpText.Length - 1;
        else
            return tmpText.Length;
    }
    public static int GetWordsAmmount(string[] _text)
    {
        int totalAmmount = 0;
        if(_text[0] != "Plate")
        {
            int a = 0;
        }
        for (int i = 0; i < _text.Length; i++)
        {
            string[] tmpText = _text[i].Split();

            if (tmpText[tmpText.Length - 1] == "")
                totalAmmount += tmpText.Length - 1;
            else
                totalAmmount += tmpText.Length;
        }

        return totalAmmount;
    }


}
