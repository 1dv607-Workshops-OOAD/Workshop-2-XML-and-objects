using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BoatClub.model;
using BoatClub.view;

namespace BoatClub.controller
{
    public class StartController
    {
        public StartController()
        {
            executeMenuChoice();
        }

        public void executeMenuChoice()
        {
            //StartView handles the main menu
            StartView startView = new StartView();
            startView.showStartMenu();
            
            StartView.MenuChoice menuChoice = startView.GetMenuChoice();
            if (menuChoice == StartView.MenuChoice.AddMember)
            {
                AddMemberController addMemberController = new AddMemberController();
            }
            if (menuChoice == StartView.MenuChoice.VerboseListMembers || 
                menuChoice == StartView.MenuChoice.CompactListMembers)
            {
                //Handles both types of lists, depending on user choice
                ListController listController = new ListController(menuChoice);
            }
            if (menuChoice == StartView.MenuChoice.AddBoat)
            {
                AddBoatController addBoatController = new AddBoatController();
            }
            //Takes user back to main menu
            if(menuChoice == StartView.MenuChoice.None){
                startView = new StartView();
                executeMenuChoice();
            }
        }
    }
}