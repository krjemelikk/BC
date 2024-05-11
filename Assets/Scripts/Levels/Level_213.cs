﻿using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Level_213 : BaseLevel
{
    [SerializeField]
    private bool _hold1;
    [SerializeField]
    private bool _hold2;
    [SerializeField]
    private bool _hold3;

    [SerializeField]
    private int countClick;

    private bool isEnd;

    public Image imgLight;
    public Sprite spLight;

    public List<Transform> lsTFObject = new List<Transform>();
    private List<Vector3> lsPos = new List<Vector3>();

    protected override void Start()
    {
        base.Start();
        countClick = 0;
        isEnd = _hold1 = _hold2 = _hold3 = false;

        foreach (Transform tf in lsTFObject)
        {
            lsPos.Add(tf.position);
        }
    }

    protected override void Update()
    {
        base.Update();
    }

    public override void StartLevel()
    {
        base.StartLevel();
    }

    public override void CompleteLevel()
    {
        base.CompleteLevel();
    }

    public override void WrongAnswer()
    {
        for (int i = 0; i < lsPos.Count; i++)
        {
            lsTFObject[i].position = lsPos[i];
        }
        base.WrongAnswer();
    }

    public override void RightAnswer()
    {
        imgLight.sprite = spLight;
        imgLight.SetNativeSize();
        base.RightAnswer();
    }

    public override void UseHint()
    {
        base.UseHint();
    }

    public void CheckAnswer()
    {
        if (isEnd)
            return;
        if (countClick >= 3)
        {
            RightAnswer();
            isEnd = true;
        }
        else
        {
            WrongAnswer();
        }
    }

    public void OnPointDown(int indexHold)
    {
        if (indexHold == 1 && !_hold1)
        {
            _hold1 = true;
            countClick++;
        }
        if (indexHold == 2 && !_hold2)
        {
            _hold2 = true;
            countClick++;
        }
        if (indexHold == 3 && !_hold3)
        {
            _hold3 = true;
            countClick++;
        }
    }

    public void OnPointUp(int indexHold)
    {
        CheckAnswer();
        if (indexHold == 1 && _hold1)
        {
            _hold1 = false;
            countClick--;
            if (countClick < 0)
                countClick = 0;
        }
        if (indexHold == 2 && _hold2)
        {
            _hold2 = false;
            countClick--;
            if (countClick < 0)
                countClick = 0;
        }
        if (indexHold == 3 && _hold3)
        {
            _hold3 = false;
            countClick--;
            if (countClick < 0)
                countClick = 0;
        }
    }
}
