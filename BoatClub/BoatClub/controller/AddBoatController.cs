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
        private AddBoatView addBoatView;

        private Member member;

        public AddBoatController()
        {
            this.addBoatView = new AddBoatView();
            addBoatView.showAddBoatMenu();
            addBoatView.showMemberList();
            member = addBoatView.getSelectedMember();
            if (member != null)
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
            addBoatView.addBoat(member);

            //Save boat and go back to main menu
            //memberDAL.saveBoat(newBoat, choice);
            StartController startController = new StartController();
        }
    }
}
