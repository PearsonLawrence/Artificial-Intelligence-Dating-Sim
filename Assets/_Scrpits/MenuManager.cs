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

	// Use this for initialization
	void Start () {
        MaleSpawn = MaleNumberAmount;
        FemaleSpawn =  FemaleNumberAmount;
    }
	
	// Update is called once per frame
	void Update () {
        MaleSpawn =  MaleNumberAmount;
        FemaleSpawn =  FemaleNumberAmount;

    }
    
    public void assignFemale()
    {
        FemaleNumberAmount = int.Parse(FemaleNumber.text);
    }

    public void assignMale()
    {

        MaleNumberAmount = int.Parse(MaleNumber.text);
    }

    public void OnPlayClick()
    {
        SceneManager.LoadScene("Test");
    }

    public void OnQuitClick()
    {

    }

    public void OnEditClick()
    {

    }
}
