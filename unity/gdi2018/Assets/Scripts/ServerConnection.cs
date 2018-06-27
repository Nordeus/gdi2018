using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class ServerConnection : MonoBehaviour
{

	private const string SERVER_URL = "http://localhost";
	
	private static ServerConnection instance;

	public static ServerConnection I
	{
		get
		{
			if (instance == null)
			{
				instance = FindObjectOfType<ServerConnection>();
			}
			if (instance == null)
			{
				var go = new GameObject();
				instance = go.AddComponent<ServerConnection>();
			}
			return instance;
		}
	}

	private void Awake()
	{
		DontDestroyOnLoad(I);
	}

	public void Get(string relativePath, Action<string> onSuccess = null, Action onError = null)
	{
		StartCoroutine(DoGetRequest(SERVER_URL + relativePath, onSuccess, onError));
	}

	public void Post(string relativePath, Dictionary<string, object> data, Action<byte[]> onSuccess = null, Action onError = null)
	{
		var formData = new List<IMultipartFormSection>();
		foreach (var keyValuePair in data)
		{
			formData.Add(new MultipartFormDataSection(keyValuePair.Key + "=" + keyValuePair.Value));
		}
		StartCoroutine(DoPostRequest(SERVER_URL + relativePath, formData, onSuccess, onError));
	}

	#region Private

	private IEnumerator DoGetRequest(string path, Action<string> onSuccess = null, Action onError = null)
	{
		UnityWebRequest www = UnityWebRequest.Get(path);
		yield return www.SendWebRequest();
 
		if(www.isNetworkError || www.isHttpError)
		{
			if (onError != null) 
				onError();
		}
		else
		{
			if (onSuccess != null) 
				onSuccess(www.downloadHandler.text);
		}
	}
	
	private IEnumerator DoPostRequest(string path, List<IMultipartFormSection> data, Action<byte[]> onSuccess = null, Action onError = null)
	{
		UnityWebRequest www = UnityWebRequest.Post(path, data);
		yield return www.SendWebRequest();
 
		if(www.isNetworkError || www.isHttpError)
		{
			if (onError != null)
				onError();
		}
		else
		{
			if (onSuccess != null) 
				onSuccess(www.downloadHandler.data);
		}
	}

	#endregion
}
