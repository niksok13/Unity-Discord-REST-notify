using System;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Networking;

public class RestDiscord
{
    public RestDiscord(string token, string channelId)
    {
        URL = $"https://discordapp.com/api/channels/{channelId}/messages";
        AccessToken = token;
    }

    private string URL { get; }
    private string AccessToken { get; }

    public async Task Notify(string responseName)
    {
        var form = new WWWForm();
        form.AddField("content", responseName);
        var request = UnityWebRequest.Post(URL,  form);
        request.SetRequestHeader("Authorization", $"Bot {AccessToken}");

        request.SendWebRequest();
        while (!request.isDone) await Task.Delay(1000);    
        if (request.error != null)
        {
            request.Dispose();
            throw new Exception(request.error);
        }
        request.Dispose();
        Debug.Log("Discord notified");
    }
}
