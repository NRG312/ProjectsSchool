using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class PlayerVsComputerMode : MonoBehaviour
{
    public int MinNumber;
    public int MaxNumber;

    private int RandomNumber;

    public TMP_Text TextGuessing;

    public TMP_Text WrongNumberMin;
    public TMP_Text WrongNumberMax;

    public Button TryAgain;
    public Button BackToMenu;
    public Button NextBut;
    public void EnterMinNumber(string number)
    {
        int.TryParse(number, out int result);
        MinNumber = result;
        if (result <= 0)
        {
            NextBut.gameObject.SetActive(false);
            WrongNumberMin.gameObject.SetActive(true);
        }
        else
        {
            NextBut.gameObject.SetActive(true);
            WrongNumberMin.gameObject.SetActive(false);
        }
    }
    public void EnterMaxNumber(string number)
    {
        int.TryParse(number, out int result);
        MaxNumber = result;
        if (result <= MinNumber)
        {
            NextBut.gameObject.SetActive(false);
            WrongNumberMax.gameObject.SetActive(true);
        }
        else
        {
            NextBut.gameObject.SetActive(true);
            WrongNumberMax.gameObject.SetActive(false);
        }
    }

    public void GuessingNumber(string number)
    {
        int.TryParse(number, out int result);
        if (result > RandomNumber)
        {
            TextGuessing.text = "Number Too High";
        }
        if (result < RandomNumber)
        {
            TextGuessing.text = "Number Too Low";
        }
        if (result == RandomNumber)
        {
            TextGuessing.text = "Correct Number!";
            TryAgain.gameObject.SetActive(true);
            BackToMenu.gameObject.SetActive(true);
        }
    }

    public void Randomnumber()
    {
       RandomNumber = Random.Range(MinNumber, MaxNumber);
    }

    public void Tryagain()
    {
        TryAgain.gameObject.SetActive(false);
        BackToMenu.gameObject.SetActive(false);
    }
}
