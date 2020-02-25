using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.Advertisements;

public class menuFunction : MonoBehaviour {
	public AudioClip[] sesDosyalari;
	public Button mainSettingsBtn, mainAdsBtn;
	public Text gameOverSkor;
	public Image screenShot,screenShotBackColor,mainLightBtn;
	public GameObject mainDisplayPanel,GameCanvas,sharePanelAnim,eysButton,adsButton,menuEysGameObj,settingsMainPanel;
	public bool timeScale = false,gameOver=false;
	public Button soundButton,pauseButtun;
	public Image lightImage,backPausePanel,cupImage,lightGameOverImage;
	public Sprite soundTurnOn, soundTurnOff,moonImage,sunImage;
	public bool firstSecond = false,firstSecond1 = false,firstSecond3 = false,settingsOpenBool=false;
	public Camera mainCamera;
	public Color[] cameraBackGround;
	public Text skor;

	void Start () {
		

		mainCamera.GetComponent<Camera> ().backgroundColor = cameraBackGround [PlayerPrefs.GetInt ("mainBackColor")];
		if (PlayerPrefs.GetInt ("mainBackColor") == 0) {

			firstSecond1 = false;
			lightImage.GetComponent<Image> ().sprite=moonImage;
			mainLightBtn.GetComponent<Image> ().sprite=moonImage;
			lightGameOverImage.GetComponent<Image> ().sprite=moonImage;
			pauseButtun.GetComponent<Image> ().color=cameraBackGround [2];
			soundButton.GetComponent<Image> ().color=cameraBackGround [2];
			backPausePanel.GetComponent<Image> ().color=cameraBackGround [3];
			cupImage.GetComponent<Image> ().color=cameraBackGround [6];
			skor.GetComponent<Text> ().color=cameraBackGround [6];
			mainDisplayPanel.GetComponent<Image> ().color = cameraBackGround [0];
			mainSettingsBtn.GetComponent<Image> ().color = cameraBackGround [6];
			mainAdsBtn.GetComponent<Image> ().color = cameraBackGround [6];
		} else if (PlayerPrefs.GetInt ("mainBackColor") == 1) {

			firstSecond1 = true;
			lightImage.GetComponent<Image> ().sprite=sunImage;
			mainLightBtn.GetComponent<Image> ().sprite=sunImage;
			lightGameOverImage.GetComponent<Image> ().sprite=sunImage;
			pauseButtun.GetComponent<Image> ().color=cameraBackGround [0];
			soundButton.GetComponent<Image> ().color=cameraBackGround [0];
			backPausePanel.GetComponent<Image> ().color=cameraBackGround [4];
			cupImage.GetComponent<Image> ().color=cameraBackGround [5];
			skor.GetComponent<Text> ().color=cameraBackGround [5];
			mainDisplayPanel.GetComponent<Image> ().color = cameraBackGround [1];
			mainSettingsBtn.GetComponent<Image> ().color = cameraBackGround [0];
			mainAdsBtn.GetComponent<Image> ().color = cameraBackGround [0];


		}

		if(PlayerPrefs.GetInt ("volumeGame")==1)
			soundButton.GetComponent<Image> ().sprite=soundTurnOn;
		else
			soundButton.GetComponent<Image> ().sprite=soundTurnOff;

	}

	public void startBtn(){
		timeScale = false;



		if (gameOver && PlayerPrefs.GetInt ("setMainMenu") == 0) {
			//GameObject.Find ("Panel").GetComponent<figureCreate> ().figureCreateObj ();
			PlayerPrefs.SetInt ("setMainMenu", 1);
			SceneManager.LoadScene ("puzzleGameDisplay");

		} else if (!gameOver) {
			if (PlayerPrefs.GetInt ("setMainMenu") == 0) {
				PlayerPrefs.SetInt ("setMainMenu", 1);
				GameObject.Find ("Panel").GetComponent<figureCreate> ().figureCreateObj ();
			}
			mainDisplayPanel.SetActive (false);
		} 


	}

	public void pauseBtn(){
		GetComponent<AudioSource>().PlayOneShot(sesDosyalari[0], PlayerPrefs.GetInt("volumeGame"));
		timeScale = true;
		backPausePanel.GetComponent<Image> ().enabled = true;
		gameObject.GetComponent<EasyTween> ().OpenCloseObjectAnimation ();
		soundButton.GetComponent<Image> ().enabled = true;
	}

	public void soundBtn(){
		if (PlayerPrefs.GetInt("volumeGame")==0) {
			firstSecond = false;
			soundButton.GetComponent<Image> ().sprite=soundTurnOn;
			PlayerPrefs.SetInt ("volumeGame",1);
		} else {
			firstSecond = true;
			soundButton.GetComponent<Image> ().sprite=soundTurnOff;
			PlayerPrefs.SetInt ("volumeGame",0);

		}

	}

	public void playBtn(){
		GetComponent<AudioSource>().PlayOneShot(sesDosyalari[0], PlayerPrefs.GetInt("volumeGame"));
		timeScale = false;
		backPausePanel.GetComponent<Image> ().enabled = false;
		gameObject.GetComponent<EasyTween> ().OpenCloseObjectAnimation ();
		soundButton.GetComponent<Image> ().enabled = false;
	}

	public void lightBtn(){
		if (firstSecond1) {
			firstSecond1 = false;
			lightImage.GetComponent<Image> ().sprite=moonImage;
			mainLightBtn.GetComponent<Image> ().sprite=moonImage;
			lightGameOverImage.GetComponent<Image> ().sprite=moonImage;

			mainCamera.GetComponent<Camera> ().backgroundColor = cameraBackGround [0];
			pauseButtun.GetComponent<Image> ().color=cameraBackGround [2];
			soundButton.GetComponent<Image> ().color=cameraBackGround [2];
			if(gameOver)
				backPausePanel.GetComponent<Image> ().color=cameraBackGround [0];
			else
				backPausePanel.GetComponent<Image> ().color=cameraBackGround [3];
			
			cupImage.GetComponent<Image> ().color=cameraBackGround [6];
			skor.GetComponent<Text> ().color=cameraBackGround [6];
			eysButton.GetComponent<Image>().color=cameraBackGround[8];
			adsButton.GetComponent<Image>().color=cameraBackGround[8];
			mainDisplayPanel.GetComponent<Image> ().color = cameraBackGround [0];
			mainSettingsBtn.GetComponent<Image> ().color = cameraBackGround [6];
			mainAdsBtn.GetComponent<Image> ().color = cameraBackGround [6];
			PlayerPrefs.SetInt ("mainBackColor",0);
		} else {
			firstSecond1 = true;
			lightImage.GetComponent<Image> ().sprite=sunImage;
			mainLightBtn.GetComponent<Image> ().sprite=sunImage;
			lightGameOverImage.GetComponent<Image> ().sprite=sunImage;
			mainCamera.GetComponent<Camera> ().backgroundColor = cameraBackGround [1];
			pauseButtun.GetComponent<Image> ().color=cameraBackGround [0];
			soundButton.GetComponent<Image> ().color=cameraBackGround [0];
			if(gameOver)
				backPausePanel.GetComponent<Image> ().color=cameraBackGround [1];
			else
				backPausePanel.GetComponent<Image> ().color=cameraBackGround [4];
			
			cupImage.GetComponent<Image> ().color=cameraBackGround [5];
			skor.GetComponent<Text> ().color=cameraBackGround [5];
			eysButton.GetComponent<Image>().color=cameraBackGround[7];
			adsButton.GetComponent<Image>().color=cameraBackGround[7];
			mainDisplayPanel.GetComponent<Image> ().color = cameraBackGround [1];
			mainSettingsBtn.GetComponent<Image> ().color = cameraBackGround [0];
			mainAdsBtn.GetComponent<Image> ().color = cameraBackGround [0];
			PlayerPrefs.SetInt ("mainBackColor",1);
		}
	}

	public void mainmenuBtn(){
		GetComponent<AudioSource>().PlayOneShot(sesDosyalari[0], PlayerPrefs.GetInt("volumeGame"));
		if (gameOver) {
			sharePanelAnim.GetComponent<EasyTween> ().OpenCloseObjectAnimation ();
			//PlayerPrefs.SetInt ("setMainMenu",0);
			//gameOver = false;
		}

		timeScale = true;
		backPausePanel.GetComponent<Image> ().enabled = false;
		gameObject.GetComponent<EasyTween> ().OpenCloseObjectAnimation ();
		soundButton.GetComponent<Image> ().enabled = false;

		mainDisplayPanel.SetActive (true);
	}

	public void restartBtn(){
		PlayerPrefs.SetInt ("setMainMenu",1);
		gameOver = false;
		SceneManager.LoadScene("puzzleGameDisplay");
	}

	public void settingBtn(){

		GetComponent<AudioSource>().PlayOneShot(sesDosyalari[0], PlayerPrefs.GetInt("volumeGame"));
		if (settingsOpenBool) {
			settingsOpenBool = false;
			settingsMainPanel.transform.GetComponent<EasyTween> ().OpenCloseObjectAnimation ();
		} else {
			settingsOpenBool = true;
			settingsMainPanel.transform.GetComponent<EasyTween> ().OpenCloseObjectAnimation ();
		
		}

	}
}
