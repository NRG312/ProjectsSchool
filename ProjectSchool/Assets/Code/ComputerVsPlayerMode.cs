using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class ComputerVsPlayerMode : MonoBehaviour
{
    public int MinNumber;
    public int MaxNumber;
    public int NumberToGuess;

    public Button CorrectBut;
    public Button TryAgain;
    public Button BackToMenu;
    public Button NextBut;

    public int GeneratingNumber;

    public TMP_Text NumberGuessing;

    public TMP_Text WrongNumberMin;
    public TMP_Text WrongNumberMax;
    public TMP_Text WrongNumberYourNum;
    public void GenerateRandomNumber()
    {
        GeneratingNumber = Random.Range(MinNumber, MaxNumber);
        NumberGuessing.text = "Your Number is " + GeneratingNumber +"?";
        if (GeneratingNumber == NumberToGuess)
        {
            CorrectBut.gameObject.SetActive(true);
        }
    }

    public void Minnumber(string number)
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
    public void Maxnumber(string number)
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
    public void EnterNumberFromPlayer(string number)
    {
        int.TryParse(number, out int result);
        NumberToGuess = result;
        if (result > MaxNumber || result < MinNumber)
        {
            NextBut.gameObject.SetActive(false);
            WrongNumberYourNum.gameObject.SetActive(true);
        }
        else
        {
            NextBut.gameObject.SetActive(true);
            WrongNumberYourNum.gameObject.SetActive(false);
        }
    }
    public void TolowNumberFunction()
    {
        if (!CorrectBut.gameObject.activeInHierarchy)
        {
            MinNumber = GeneratingNumber;
            GenerateRandomNumber();
        }
    }
    public void ToHighNumberFunction()
    {
        if (!CorrectBut.gameObject.activeInHierarchy)
        {
            MaxNumber = GeneratingNumber;
            GenerateRandomNumber();
        }
    }
    public void CorrectFunction()
    {
        NumberGuessing.text = ":)";
        TryAgain.gameObject.SetActive(true);
        BackToMenu.gameObject.SetActive(true);
    }
    public void Tryagain()
    {
        TryAgain.gameObject.SetActive(false);
        BackToMenu.gameObject.SetActive(false);
    }
}
