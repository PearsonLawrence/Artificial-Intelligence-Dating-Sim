using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class MenuManager : MonoBehaviour {

    public GameObject Menu1, Menu2, Menu3;
    public InputField FemaleNumber, MaleNumber;

    public static int MaleNumberAmount;
    public static int FemaleNumberAmount;

    public int MaleSpawn, FemaleSpawn;

    public float FadeTime;
    public bool In, Out;
    public Image Black;
	// Use this for initialization
	void Start () {
        MaleSpawn = MaleNumberAmount;
        FemaleSpawn =  FemaleNumberAmount;
        In = true;
    }
	
	// Update is called once per frame
	void Update () {
        MaleSpawn =  MaleNumberAmount;
        FemaleSpawn =  FemaleNumberAmount;

        if(In)
        {
            Black.color -= new Color(0,0,0, Time.deltaTime * FadeTime);
            Black.color = new Color(Black.color.r, Black.color.g, Black.color.b, Mathf.Clamp(Black.color.a, 0, 255));
            if(Black.color.a <= 0)
            {
                Black.gameObject.SetActive(false);
            }
        }
        else
        {

            Black.color += new Color(0, 0, 0, Time.deltaTime * FadeTime);
            Black.color = new Color(Black.color.r, Black.color.g, Black.color.b, Mathf.Clamp(Black.color.a, 0, 255));
            if(Black.color.a >= 1 && load)
            {
                SceneManager.LoadScene("Test");
            }
        }
    }
    
    public void assignFemale()
    {
        FemaleNumberAmount = int.Parse(FemaleNumber.text);
    }

    public void assignMale()
    {

        MaleNumberAmount = int.Parse(MaleNumber.text);
    }
    bool load;
    public void OnPlayClick()
    {
        load = true;
        In = false;

        Black.gameObject.SetActive(true);
    }

    public void OnQuitClick()
    {
        Application.Quit();
    }

    public void OnEditClick()
    {
        Menu1.SetActive(false);
        Menu2.SetActive(true);
    }
    public void OnBack()
    {
        Menu1.SetActive(true);
        Menu2.SetActive(false);
    }
}
