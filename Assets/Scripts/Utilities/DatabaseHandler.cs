using System;
using System.Collections.Generic;
using FullSerializer;
using Proyecto26;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Utilities
{
    public class DatabaseHandler : MonoBehaviour
    {
        
        public delegate void PostScoresCallback();
        public delegate void GetScoresCallback(Dictionary<string, string> scores);
        public delegate void GetScoresFallback();
        
        private static readonly string DatabaseUrl = "https://guess-my-phrase.firebaseio.com/";

        private static readonly fsSerializer serializer = new fsSerializer();
        
        public static void PostScore(string name, int score, PostScoresCallback callback)
        {
            RestClient.Get(DatabaseUrl + name + ".json").Then(value =>
            {
                if (value.Text != "null" && int.Parse(value.Text.Trim('"')) >= score)
                {
                    callback();
                    return;
                }
                var payLoad = "\"" + score + "\"";
                RestClient.Put(DatabaseUrl + name + ".json", payLoad).Then(newValue => { callback(); }).Catch(
                    error =>
                    {
                        Debug.Log("Error: " + error);
                        callback();
                    });
            }).Catch(error =>
            {
                Debug.Log("Error: " + error);
                callback();
            });
        }
        
        public static void GetScores(GetScoresCallback callback, GetScoresFallback fallback)
        {
            RestClient.Get(DatabaseUrl + ".json?orderBy=\"$value\"").Then(scoresRaw =>
            {
                fsData data = fsJsonParser.Parse(scoresRaw.Text);
                Dictionary<string, string> scores = null;
                serializer.TryDeserialize(data, ref scores);
                callback(scores);
            }).Catch(error =>
            {
                Debug.Log("Error: " + error);
                fallback();
            });
        }

    }
}
