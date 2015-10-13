using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BoatClub.helper;
using BoatClub.view;

namespace BoatClub.controller
{
    class ListController
    {
        public ListController(StartView.MenuChoice menuChoice)
        {
            executeMenuChoice(menuChoice);
        }

        public void executeMenuChoice(StartView.MenuChoice menuChoice)
        {
            //Handles user interface for both types of lists
            ListView listView = new ListView();

            if (menuChoice == StartView.MenuChoice.CompactListMembers)
            {
                listView.showCompactList();
                Helper.MenuChoice choice = listView.goToStartMenu();
                if (choice == Helper.MenuChoice.Back || choice == Helper.MenuChoice.None)
                {
                    StartController startController = new StartController();
                }
            }
            else
            {
                listView.showVerboseList();
                string choice = listView.getChoice();

                if (choice.ToUpper() == "S")
                {
                    StartController startController = new StartController();
                }

                EditMemberController editMemberController = new EditMemberController(choice);
            }
        }
    }
}
