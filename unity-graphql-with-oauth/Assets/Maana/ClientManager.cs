using System;
using UnityEngine;
using GraphQL;
using OAuth;

public class ClientManager : MonoBehaviour
{
    public bool hasToken => fetcher.token != null;

    [SerializeField] private string clientURL;
    [SerializeField] private TextAsset credentials;

    private OAuthFetcher fetcher;
    private GraphQLClient client;

    private void Start()
    {
        fetcher = new OAuthFetcher(credentials.text);
        client = new GraphQLClient(clientURL);
    }

    public void Query(string query, object variables = null, Action<GraphQLResponse> callback = null)
    {
        if(hasToken)
            client.Query(query, fetcher.token.access_token, callback);
    }

    // Below are examples of using Query

    void TestQuery()
    {
        Query(@"query { test }", callback: QueryCallback);
    }

    void QueryCallback(GraphQLResponse response)
    {
        Debug.Log(response.Raw);
    }
}
