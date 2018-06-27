using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenuManager : MonoBehaviour
{
	
	
	[Header("Panels")]
	[SerializeField]
	private RectTransform loginPanel;
	
	[SerializeField]
	private RectTransform gamePanel;
	
	[Header("Login panel")]
	[SerializeField]
	private Button startButton;
	
	[SerializeField]
	private Button loginQuitButton;
	
	[SerializeField]
	private InputField nameInputField;
	
	[SerializeField]
	private RectTransform errorPanel;

	[Header("Game panel")]
	[SerializeField]
	private Text usernameText;
	
	[SerializeField]
	private Text userXpText;
	
	[SerializeField]
	private Image userLevelProgressImage;
	
	[SerializeField]
	private Text userLevelText;
	
	[SerializeField]
	private Button playButton;
	
	[SerializeField]
	private Button quitButton;

	[SerializeField]
	private RectTransform matchesPanel;

	[SerializeField]
	private GameObject matchInfoPanelPrefab;

	[SerializeField]
	private Text noMatchesText;

	private void Awake()
	{
		startButton.onClick.AddListener(OnStartButtonClick);
		quitButton.onClick.AddListener(OnQuitButtonClick);
		loginQuitButton.onClick.AddListener(OnQuitButtonClick);
		playButton.onClick.AddListener(OnPlayButtonClick);
		errorPanel.gameObject.SetActive(false);
	}

	private void Start()
	{
		RefreshMainMenu();
	}

	private void OnStartButtonClick()
	{
		var username = nameInputField.text.Trim();
		if (string.IsNullOrEmpty(username))
			return;
		
		errorPanel.gameObject.SetActive(false);
		UserModel.GetUser(username, RefreshMainMenu,
		() =>
		{
			errorPanel.gameObject.SetActive(true);
		});
	}

	private void RefreshMainMenu()
	{
		var isLoggedIn = UserModel.User != null;
		loginPanel.gameObject.SetActive(!isLoggedIn);
		gamePanel.gameObject.SetActive(isLoggedIn);
		if (isLoggedIn)
		{
			usernameText.text = UserModel.User.username;
			userXpText.text = UserModel.User.xp + "/" + (UserModel.User.level * 10);
			userLevelProgressImage.fillAmount = UserModel.User.xp / (UserModel.User.level * 10f);
			userLevelText.text = (UserModel.User.level + 1).ToString();

			FetchBattles();
		}
	}

	private void FetchBattles()
	{
		noMatchesText.gameObject.SetActive(true);
		noMatchesText.text = "Loading...";
		
		var matchInfoPanels = matchesPanel.GetComponentsInChildren<MatchInfoPanel>().ToList();
		for (int i = 0; i < matchInfoPanels.Count; i++)
		{
			matchInfoPanels[i].gameObject.SetActive(false);
		}
		UserModel.GetBattles(RefreshBattles, null);
	}

	private void RefreshBattles()
	{
		var matchInfoPanels = matchesPanel.GetComponentsInChildren<MatchInfoPanel>().ToList();
		for (int i = 0; i < UserModel.Battles.Length; i++)
		{
			var battle = UserModel.Battles[i];
			// already existing, refresh it
			if (i < matchInfoPanels.Count)
			{
				matchInfoPanels[i].Refresh(battle);
				matchInfoPanels[i].gameObject.SetActive(true);
			}
			else
			{
				// instantiate new one
				var matchInfoPanel = Instantiate(matchInfoPanelPrefab, matchesPanel.transform).GetComponent<MatchInfoPanel>();
				matchInfoPanels.Add(matchInfoPanel);
				matchInfoPanel.Refresh(battle);
			}
		}

		// remove not used ones
		for (int i = UserModel.Battles.Length; i < matchInfoPanels.Count; i++)
		{
			matchInfoPanels[i].gameObject.SetActive(false);
		}

		// calculate the size of the container
		var height = matchInfoPanels.Count > 0 ? matchInfoPanels[0].GetComponent<RectTransform>().sizeDelta.y : 0f;
		var verticalGroup = matchesPanel.GetComponent<VerticalLayoutGroup>();
		var battleCount = UserModel.Battles.Length;
		var padding = verticalGroup.padding.top + verticalGroup.padding.bottom;
		// height is sum of all elements + spacing between them + padding on top and bottom
		matchesPanel.sizeDelta = new Vector2(matchesPanel.sizeDelta.x, height * battleCount + verticalGroup.spacing * (battleCount - 1) + padding);
		
		noMatchesText.gameObject.SetActive(battleCount == 0);
		noMatchesText.text = "No battles!";
	}

	private void OnPlayButtonClick()
	{
		SceneManager.LoadScene("CompletedScene");
	}

	private void OnQuitButtonClick()
	{
		Application.Quit();
	}
	
}
