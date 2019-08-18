﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class ShipType1 : Mobile
{
    /// <summary> Объект, на который будут установлены подсистемы. </summary>
    protected GameObject subModulesObj;
    /// <summary> Объект, на который будут установлены компоненты щитов. </summary>
    protected GameObject ShildsObj;
    
    /// <summary> Инициализация системы жизней, и объектов для подсистем и щитов </summary>
    public override void Awake()
    {
        base.Awake();
        frameSelection = true;
        healthSystem = true;
        
        _hs = gameObject.AddComponent<HealthSystem>();
        
        subModulesObj = new GameObject();
        subModulesObj.transform.parent = transform;
        subModulesObj.transform.localPosition = Vector3.zero;
        subModulesObj.name = "SubSystems";
        
        ShildsObj = new GameObject();
        ShildsObj.transform.parent = transform;
        ShildsObj.transform.localPosition = Vector3.zero;
        ShildsObj.name = "Shilds";
    }

    // Update is called once per frame
    public override void Update()
    {
        base.Update();
    }

    /// <summary> Инициализация щитов. </summary>
    protected void initShilds(int count, GameObject ShildObject, HealthSystem _hs, float shildForce, float shieldDelay, float shieldDivider)
    {
        _hs.Shilds = Enumerable.Range(0,count).Select(x=>ShildObject.AddComponent<NormalRaceShield>().InitShield(shieldDelay,shieldDivider,shildForce)).ToArray();
    }
}