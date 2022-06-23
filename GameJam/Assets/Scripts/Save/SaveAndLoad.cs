using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using System.Xml.Serialization;
using Riutilizzabile;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SaveAndLoad : SingletonDDOL<SaveAndLoad> {
	[TextArea] public string serializedData;
	public Action StartSave;
	public Data ToBeSaved = new Data();
	public int slot;
	private AsyncOperation operation;
	public List<GeneralCard> AllCard;

	private void Update() {
		// if (canSave)
		// {
		//     if (player.GetButtonDown(RewiredConsts.Action.Interact))
		//     {
		//         StartCoroutine(DialogueSystem.Instance.ShowText("Hai Salvato", 3));
		//         Save();
		//     }
		// }

		if (Input.GetKeyDown(KeyCode.F4)) {
			Debug.Log("load");
			load();
		}

		if (Input.GetKeyDown(KeyCode.F2)) {
			Debug.Log("save");
			Save();
		}
	}

	public void load() {
		if (File.Exists(Application.dataPath + $"/saves/saveEncrypted{slot}.xml")) {
			Time.timeScale = 1f;
			var data = ReadFile();
			FindObjectOfType<GameManager>().money = data.money;
			FindObjectOfType<GameManager>().cardsInDeck = new Dictionary<GeneralCard, int>();
			foreach (var alluid in AllCard) {
				foreach (var uid in data.cardUids) {
					if (uid==alluid.uid) {
						FindObjectOfType<GameManager>().cardsInDeck.Add(alluid,data.cardNumber[data.cardUids.IndexOf(uid)]);
					}
				}
			}
		}
	}

	public void Save() {
		ToBeSaved.cardNumber.Clear();
		ToBeSaved.cardUids.Clear();
		StartSave();
		XmlSerializer serializer = new XmlSerializer(typeof(Data));
		var encoding = Encoding.GetEncoding("UTF-8");
		if (!Directory.Exists(Application.dataPath + $"/saves")) {
			System.IO.Directory.CreateDirectory(Application.dataPath + $"/saves");
		}
		StreamWriter stream = new StreamWriter(Application.dataPath + $"/saves/save{slot}.xml", false, encoding);
		serializer.Serialize(stream, ToBeSaved);

		// serializedData = stream.ToString();
		// string filePath = Application.dataPath + "/save.xml";
		// File.WriteAllText(filePath, serializedData);

		stream.Dispose();
		EncryptFile(Application.dataPath + $"/saves/save{slot}.xml", Application.dataPath + $"/saves/saveEncrypted{slot}.xml");
		//File.Delete(Application.dataPath + "/save.xml");
		Time.timeScale = 1f;
	}

	public Data ReadFileInSlot(int i) {
		DecryptFile(Application.dataPath + $"/saves/saveEncrypted{i}.xml", Application.dataPath + $"/saves/save{i}.xml");
		string filePath = Application.dataPath + $"/saves/save{i}.xml";
		serializedData = File.ReadAllText(filePath);
		XmlSerializer serializer = new XmlSerializer(typeof(Data));

		StringReader stream = new StringReader(serializedData);
		object deserializedObject = serializer.Deserialize(stream);

		Data data = (Data) deserializedObject;
		stream.Dispose();
		EncryptFile(Application.dataPath + $"/saves/save{i}.xml", Application.dataPath + $"/saves/saveEncrypted{i}.xml");
		return data;
	}

	public Data ReadFile() {
		DecryptFile(Application.dataPath + $"/saves/saveEncrypted{slot}.xml", Application.dataPath + $"/saves/save{slot}.xml");
		string filePath = Application.dataPath + $"/saves/save{slot}.xml";
		serializedData = File.ReadAllText(filePath);
		XmlSerializer serializer = new XmlSerializer(typeof(Data));

		StringReader stream = new StringReader(serializedData);
		object deserializedObject = serializer.Deserialize(stream);

		Data data = (Data) deserializedObject;
		stream.Dispose();
		EncryptFile(Application.dataPath + $"/saves/save{slot}.xml", Application.dataPath + $"/saves/saveEncrypted{slot}.xml");
		return data;
	}

	public void DeleteSave() {
		if (File.Exists(Application.dataPath + $"/saves/save{slot}.xml")) {
			File.Delete(Application.dataPath + $"/saves/save{slot}.xml");
		}
		if (File.Exists(Application.dataPath + $"/saves/saveEncrypted{slot}.xml")) {
			File.Delete(Application.dataPath + $"/saves/saveEncrypted{slot}.xml");
		}
		if (File.Exists(Application.dataPath + $"/saves/screen{slot}.png")) {
			File.Delete(Application.dataPath + $"/saves/screen{slot}.png");
		}
	}

	private void EncryptFile(string inputFile, string outputFile) {
		try {
			//needs to be 8 char
			string password = @"MikFra12"; // Your Key Here
			UnicodeEncoding UE = new UnicodeEncoding();
			byte[] key = UE.GetBytes(password);

			string cryptFile = outputFile;
			FileStream fsCrypt = new FileStream(cryptFile, FileMode.Create);

			RijndaelManaged rmCrypto = new RijndaelManaged();

			CryptoStream cs = new CryptoStream(fsCrypt,
				rmCrypto.CreateEncryptor(key, key),
				CryptoStreamMode.Write);

			FileStream fsIn = new FileStream(inputFile, FileMode.Open);

			int data;
			while ((data = fsIn.ReadByte()) != -1)
				cs.WriteByte((byte) data);


			fsIn.Close();
			cs.Close();
			fsCrypt.Close();
		}
		catch {
			Debug.Log("Encryption failed!");
		}
	}

	private void DecryptFile(string inputFile, string outputFile) {
		{
			//needs to be 8 char
			string password = @"MikFra12"; // Your Key Here

			UnicodeEncoding UE = new UnicodeEncoding();
			byte[] key = UE.GetBytes(password);

			FileStream fsCrypt = new FileStream(inputFile, FileMode.Open);

			RijndaelManaged RMCrypto = new RijndaelManaged();

			CryptoStream cs = new CryptoStream(fsCrypt,
				RMCrypto.CreateDecryptor(key, key),
				CryptoStreamMode.Read);

			FileStream fsOut = new FileStream(outputFile, FileMode.Create);

			int data;
			while ((data = cs.ReadByte()) != -1)
				fsOut.WriteByte((byte) data);

			fsOut.Close();
			cs.Close();
			fsCrypt.Close();
		}
	}

	// public void instanceButtonToLoad() {
	//     SingletonLoadUi.Instance.gameObject.SetActive(true);
	// }
	//
	// public void openSave() {
	//     SingletonSaveUi.Instance.gameObject.SetActive(true);
	//     Time.timeScale = 0f;
	// }

	public void SetSlot(int i) {
		slot = i;
	}

	public IEnumerator loadSceneAsync(string scene) {
		operation = SceneManager.LoadSceneAsync(scene);
		operation.allowSceneActivation = false;
		while (!operation.isDone) {
			Debug.Log("LOAD SCENE: Progress: " + operation.progress);
			Debug.Log(operation.allowSceneActivation);
			if (operation.progress >= 0.9f) {
				operation.allowSceneActivation = true;
			}
			yield return null;
		}
		operation = null;
	}
}