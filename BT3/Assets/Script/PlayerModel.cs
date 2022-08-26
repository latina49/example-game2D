using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
namespace AssemblyCsharp
{
    public class PlayerModel
    {

        public Model[] models;
        public PlayerModel (int count)
        {
            models = new Model[count];
        }
        [Serializable]
        public class Model
        {
            public int ID;
            public string name;
            public GameData[] gameDatas = new GameData[2];
        }
        [Serializable]
        public class GameData
        {
            public int GameID;
            public int score;
        }
        public static Model[] GetModelFromJson(string respense)
        {
            PlayerModel model = JsonUtility.FromJson<PlayerModel>(respense);
            return model.models;
        }
        public static string GetJsonFromModel(PlayerModel model, bool pretty)
        {
            return JsonUtility.ToJson(model, pretty);
        }
    }
}


