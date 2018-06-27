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

	/// <summary>
	/// Makes GET web request to the server
	/// </summary>
	/// <param name="relativePath">Relative path for the request</param>
	/// <param name="onSuccess">Callback with text dana and status code</param>
	/// <param name="onError">Callbak with error status code</param>
	public void Get(string relativePath, Action<string, long> onSuccess = null, Action<long> onError = null)
	{
		StartCoroutine(DoGetRequest(SERVER_URL + relativePath, onSuccess, onError));
	}

	/// <summary>
	/// Makes POST wev request to the server
	/// </summary>
	/// <param name="relativePath">Relative path for the request</param>
	/// <param name="data">List of data parameters sent in body</param>
	/// <param name="onSuccess">Callback with text data and status code</param>
	/// <param name="onError">Callback with error status code</param>
	public void Post(string relativePath, Dictionary<string, object> data, Action<string, long> onSuccess = null, Action<long> onError = null)
	{
		var formData = new List<IMultipartFormSection>();
		if (data != null)
		{
			foreach (var keyValuePair in data)
			{
				formData.Add(new MultipartFormDataSection(keyValuePair.Key + "=" + keyValuePair.Value));
			}
		}
		StartCoroutine(DoPostRequest(SERVER_URL + relativePath, formData, onSuccess, onError));
	}

	#region Private

	private IEnumerator DoGetRequest(string path, Action<string, long> onSuccess = null, Action<long> onError = null)
	{
		UnityWebRequest www = UnityWebRequest.Get(path);
		yield return www.SendWebRequest();
 
		if(www.isNetworkError || www.isHttpError)
		{
			if (onError != null) 
				onError(www.responseCode);
		}
		else
		{
			if (onSuccess != null) 
				onSuccess(www.downloadHandler.text, www.responseCode);
		}
	}
	
	private IEnumerator DoPostRequest(string path, List<IMultipartFormSection> data, Action<string, long> onSuccess = null, Action<long> onError = null)
	{
		UnityWebRequest www = UnityWebRequest.Post(path, data);
		yield return www.SendWebRequest();
 
		if(www.isNetworkError || www.isHttpError)
		{
			if (onError != null)
				onError(www.responseCode);
		}
		else
		{
			if (onSuccess != null) 
				onSuccess(www.downloadHandler.text, www.responseCode);
		}
	}

	#endregion
}
