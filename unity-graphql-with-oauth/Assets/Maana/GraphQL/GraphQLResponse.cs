﻿using Newtonsoft.Json.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GraphQL
{
    public class GraphQLResponse
    {
        public string Raw { get; private set; }
        private readonly JObject data;
        public string Exception { get; private set; }

        public GraphQLResponse(string text, string ex = null)
        {
            Exception = ex;
            Raw = text;
            data = string.IsNullOrEmpty(text) ? null : JObject.Parse(text);
        }

        public T Get<T>(string key)
        {
            return GetData()[key].ToObject<T>();
        }

        public List<T> GetList<T>(string key)
        {
            return Get<JArray>(key).ToObject<List<T>>();
        }

        private JObject GetData()
        {
            return data == null ? null : JObject.Parse(data["data"].ToString());
        }
    }

}