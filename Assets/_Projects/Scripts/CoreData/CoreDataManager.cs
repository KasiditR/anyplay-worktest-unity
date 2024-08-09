using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoreDataManager : Singleton<CoreDataManager>
{
    [SerializeField] private UserData _userData;

    public UserData UserData { get => _userData; set => _userData = value; }

    public void ClearUserData()
    {
        _userData = new UserData();
    }
}
