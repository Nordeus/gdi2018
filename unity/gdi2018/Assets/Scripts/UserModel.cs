using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class UserModel
{

    [Serializable]
    public class UserData
    {
        public string username;
        public int xp;
        public int level;
    }

    [Serializable]
    public class BattleData
    {
        public string timestamp;
        public int score;
        public bool haveWon;
    }

    public static UserData User { get; private set; }
    public static BattleData[] Battles { get; private set; }

    public static void GetUser(string username, Action onSuccess, Action onError)
    {
        User = new UserData {username = username, xp = 5, level = 1};
        onSuccess();
        return;
        ServerConnection.I.Get("/player/" + username, data =>
        {
            try
            {
                User = JsonUtility.FromJson<UserData>(data);
            }
            catch (Exception e)
            {
                onError();
            }
        }, onError);
    }

    public static void PostScore(int score, bool hasWon)
    {
        if (User == null)
        {
            Debug.LogError("User not logged in!");
            return;
        }
        
        var data = new Dictionary<string, object>
        {
            { "score", score },
            { "hasWon", hasWon },
        };
        ServerConnection.I.Post("/player/" + User.username, data);
    }

    public static void GetBattles(Action onSuccess, Action onError)
    {
        if (User == null)
        {
            Debug.LogError("User not logged in!");
            return;
        }
        
        Battles = new BattleData[]
        {
            new BattleData { timestamp = "26-6-2018 17:34:52", score = 15, haveWon = true },
            new BattleData { timestamp = "25-6-2018 16:44:27", score = 5, haveWon = false },
            new BattleData { timestamp = "25-6-2018 11:32:62", score = 1, haveWon = false },
            new BattleData { timestamp = "22-6-2018 19:14:12", score = 25, haveWon = true },
        };
        onSuccess();
        return;
        ServerConnection.I.Get("/player/" + User.username + "/battle", data =>
        {
            try
            {
                Battles = JsonUtility.FromJson<BattleData[]>(data);
            }
            catch (Exception e)
            {
                onError();
            }
        }, onError);
    }
}
