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

	[Header("Matches")]
	[SerializeField]
	private RectTransform matchesContainerPanel;
	
	[SerializeField]
	private Button matchesButton;

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
		matchesButton.onClick.AddListener(OnMatchesButtonClick);
		errorPanel.gameObject.SetActive(false);
		
		matchesContainerPanel.gameObject.SetActive(false);
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
		PlayerModel.GetPlayer(username, RefreshMainMenu,
		() =>
		{
			errorPanel.gameObject.SetActive(true);
		});
	}

	private void RefreshMainMenu()
	{
		var isLoggedIn = PlayerModel.Player != null;
		loginPanel.gameObject.SetActive(!isLoggedIn);
		gamePanel.gameObject.SetActive(isLoggedIn);
		if (isLoggedIn)
		{
			usernameText.text = PlayerModel.Player.username;
			userXpText.text = PlayerModel.Player.xp + "/" + (PlayerModel.Player.level * 50f);
			userLevelProgressImage.fillAmount = PlayerModel.Player.xp / (PlayerModel.Player.level * 50f);
			userLevelText.text = (PlayerModel.Player.level + 1).ToString();

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
		PlayerModel.GetBattles(RefreshBattles, () => { noMatchesText.text = "ERROR!"; });
	}

	private void RefreshBattles()
	{
		var matchInfoPanels = matchesPanel.GetComponentsInChildren<MatchInfoPanel>().ToList();
		for (int i = 0; i < PlayerModel.Battles.Length; i++)
		{
			var battle = PlayerModel.Battles[i];
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
		for (int i = PlayerModel.Battles.Length; i < matchInfoPanels.Count; i++)
		{
			matchInfoPanels[i].gameObject.SetActive(false);
		}

		// calculate the size of the container
		var height = matchInfoPanels.Count > 0 ? matchInfoPanels[0].GetComponent<RectTransform>().sizeDelta.y : 0f;
		var verticalGroup = matchesPanel.GetComponent<VerticalLayoutGroup>();
		var battleCount = PlayerModel.Battles.Length;
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

	private void OnMatchesButtonClick()
	{
		matchesContainerPanel.gameObject.SetActive(!matchesContainerPanel.gameObject.activeInHierarchy);
		if (matchesContainerPanel.gameObject.activeInHierarchy)
		{
			FetchBattles();
		}
	}
	
}
