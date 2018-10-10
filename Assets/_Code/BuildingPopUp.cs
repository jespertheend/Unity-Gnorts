﻿using System.Collections.Generic;                                                               //We need this for 'List<string>'
using UnityEngine;
using UnityEngine.UI;                                                                           //We need this to interact with the UI

public class BuildingPopUp : MonoBehaviour {

    public GameObject SelectedBuilding;                                                         //The selected building
    byte SelectedBuildingSpecial;                                                               //The special tag of the building
    public Dropdown DropDownMenu;                                                               //This needs to be set to the Dropdown menu itzelf
    
    void Start()                                                                        //Triggered on star
    {
        DropDownMenu.onValueChanged.AddListener(delegate {InputDropdown(DropDownMenu);});       //Create a listner for this Dropdown menu
    }
    public void SelectBuilding(GameObject Building, byte ClickSpecial)                  //If a building is being selected
    {
        SelectedBuildingSpecial = ClickSpecial;                                                 //Remember the ClickSpecial of this building
        SelectedBuilding = Building;                                                            //Remember this building
        DropDownMenu.ClearOptions();                                                            //Clear the old options of the Dropdown menu
        if (SelectedBuildingSpecial > 0 )                                                       //If this building has a special
            ChangeOption(Building, SelectedBuildingSpecial, true, 255);                         //Do the special code
        else
            gameObject.SetActive(false);                                                        //This object doesn't have a special drop down menu, so hide it
    }
    void InputDropdown(Dropdown change)                                                 //Triggered when the dropdown menu changes
    {
        ChangeOption(SelectedBuilding, SelectedBuildingSpecial, true, System.Convert.ToByte(change.value));
    }
    public void ChangeOption(GameObject Building, byte ClickSpecial, bool PopUp, byte ToOption) //ToOption=255 = do not change option
    {
        if (ClickSpecial == 1)                                                                  //If it's a Gate
        {
            GameObject Gate = Building.transform.Find("Gate").gameObject;                       //Get the gate
            if (ToOption < 255)                                                                 //If an (valid) option has been given
            {
                DropDownMenu.value = ToOption;                                                  //Set the gate to the selected option
                if (ToOption == 0)                                                              //If value has been changed to 0 (Close Gate)
                    Gate.SetActive(false);                                                      //Hide Gate
                else
                    Gate.SetActive(true);                                                       //Show Gate
            }
            else
            {
                if (Gate.activeSelf)                                                            //If the gate is active
                    ToOption = 1;                                                               //Set the value to 1 (Open)
                else
                    ToOption = 0;                                                               //Set the value to 0 (Closed)
            }
            if (PopUp)                                                                          //If we need a PopUp window
            {
                DropDownMenu.ClearOptions();                                                    //Clear the old options of the Dropdown menu
                DropDownMenu.AddOptions(new List<string> { "Open", "close" });                  //Add the options to the dropdown menu
            }
            Building.GetComponent<BuildingOption>().SelectedOption = ToOption;                  //Update the Building with this info
        }
        else if(ClickSpecial == 2)                                                              //If it's a XXXX
        {
            if (ToOption < 255)                                                                 //If an (valid) option has been given
            {
                DropDownMenu.value = ToOption;                                                  //Set the gate to the selected option
                //Execute something
            }
            else
            {
                //Get the proper values (the building is just placed or something)
            }
            if (PopUp)                                                                          //If we need a PopUp window
            {
                DropDownMenu.ClearOptions();                                                    //Clear the old options of the Dropdown menu
                DropDownMenu.AddOptions(new List<string> { "Option 1", "Option 2" });           //Add the options to the dropdown menu
            }
            SelectedBuilding.GetComponent<BuildingOption>().SelectedOption = ToOption;          //Update the Building with this info
        }
    }
}