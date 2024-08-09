using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoreDataManager : Singleton<CoreDataManager>
{
    [SerializeField] private UserData userData;

    public UserData UserData { get => userData; set => userData = value; }

    public void ClearUserData()
    {
        userData = new UserData();
    }
}
