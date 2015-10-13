using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BoatClub.model;
using BoatClub.view;

namespace BoatClub.controller
{
    class AddBoatController
    {
        private AddBoatView boatView;
        
        private string choice;

        public AddBoatController()
        {
            this.boatView = new AddBoatView();
            boatView.showAddBoatMenu();
            boatView.showMemberList();
            choice = boatView.getChoice();
            if (boatView.doesMemberExist())
            {
                saveBoat();
            }
            else {
                StartController startController = new StartController();
            }
        }

        public void saveBoat()
        {
            MemberDAL memberDAL = new MemberDAL();
            Boat newBoat = boatView.addBoat(choice);

            //Save boat and go back to main menu
            memberDAL.saveBoat(newBoat, choice);
            StartController startController = new StartController();
        }
    }
}
