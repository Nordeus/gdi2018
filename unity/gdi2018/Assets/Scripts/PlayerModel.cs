using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class PlayerModel
{

    [Serializable]
    public class PlayerData
    {
        public int id;
        public string username;
        public int xp;
        public int level;
    }

    [Serializable]
    public class BattleData
    {
        public int playerId;
        public string endedAt;
        public int score;
        public bool won;
    }

    public static PlayerData Player { get; private set; }
    public static BattleData[] Battles { get; private set; }

    public static void GetPlayer(string username, Action onSuccess, Action onError)
    {     
        ServerConnection.I.Get("/player?username=" + username, (data, status) =>
        {
            try
            {
                // special case, not existing user
                if (status == 204)
                {
                    CreateUser(username, onSuccess, onError);
                    return;
                }
                
                // Decode user data to model
                Player = JsonUtility.FromJson<PlayerData>(data);
                onSuccess();
            }
            catch (Exception e)
            {
                onError();
            }
        }, status =>
        {
            Debug.LogError("ERROR: " + status);
            onError();
        });
    }

    public static void CreateUser(string username, Action onSuccess, Action onError)
    {
        ServerConnection.I.Post("/player?username=" + username, null, (data, status) =>
        {
            try
            {
                // Decode user data to model
                Player = JsonUtility.FromJson<PlayerData>(data);
                onSuccess();
            }
            catch (Exception e)
            {
                onError();
            }
        }, status =>
        {
            Debug.LogError("ERROR: " + status);
            onError();
        });
    }

    public static void PostScore(int score, bool hasWon)
    {
        if (Player == null)
        {
            Debug.LogError("User not logged in!");
            return;
        }
        
        var requestData = new Dictionary<string, object>
        {
            { "score", score },
            { "won", hasWon },
        };
        ServerConnection.I.Post("/player/" + Player.id + "/battle", requestData, (data, status) =>
        {
            try
            {
                // Decode user data to model
                Player = JsonUtility.FromJson<PlayerData>(data);
            }
            catch
            {
                Debug.LogError("Can't post score!");
            }
        });
    }

    public static void GetBattles(Action onSuccess, Action onError)
    {
        if (Player == null)
        {
            Debug.LogError("User not logged in!");
            onError();
            return;
        }

        
        ServerConnection.I.Get("/player/" + Player.id + "/battle", (data, status) =>
        {
            try
            {
                Battles = JsonUtility.FromJson<BattleData[]>(data);
                onSuccess();
            }
            catch (Exception e)
            {
                onError();
            }
        }, status =>
        {
            Debug.LogError("ERROR: " + status);
            onError();
        });
    }
}
