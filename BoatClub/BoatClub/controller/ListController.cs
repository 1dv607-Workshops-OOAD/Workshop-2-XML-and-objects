using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BoatClub.helper;
using BoatClub.view;
using BoatClub.model;

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
                Member member = listView.getSelectedMember();

                if (member == null)
                {
                    StartController startController = new StartController();
                }

                EditMemberController editMemberController = new EditMemberController(member);
            }
        }
    }
}
