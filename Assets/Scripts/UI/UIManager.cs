using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public Text _cellType;

    public void SetCellTypeText(string text)
    {
        _cellType.text = text;
    }
}
