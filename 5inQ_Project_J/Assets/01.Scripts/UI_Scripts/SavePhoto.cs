using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;

public class SavePhoto : MonoBehaviour
{
    public GameObject save_UI;
    string[] files = null;
    int whichScreenShotIsShown = 0;
   

    private void Start()
    {
        files = Directory.GetFiles(Application.persistentDataPath + "/", "*.png");
        
        if (files.Length > 0)
        {
            GetPictureAndShowIt();
        }
    }

    private void GetPictureAndShowIt()
    {
        string pathToFile = files[whichScreenShotIsShown];
        Texture2D texture = GetScreenImage(pathToFile);
        Sprite sp = Sprite.Create(texture, new Rect(0, 0, texture.width, texture.height), new Vector2(0.5f, 0.5f));
        save_UI.GetComponent<Image>().sprite = sp;
    }

    private Texture2D GetScreenImage(string filePath)
    {
        Texture2D texture = null;
        byte[] fileBytes;
        if (File.Exists(filePath))
        {
            fileBytes = File.ReadAllBytes(filePath);
            texture = new Texture2D(2, 2, TextureFormat.ARGB32, false);
            texture.LoadImage(fileBytes);
        } 
        return texture;
    }
}
