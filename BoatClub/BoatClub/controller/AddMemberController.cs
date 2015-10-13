using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BoatClub.helper;
using BoatClub.model;
using BoatClub.view;

namespace BoatClub.controller
{
    class AddMemberController
    {
        public AddMemberController()
        {
            addNewMember();
        }

        public void addNewMember()
        {
            AddMemberView addMemberView  = new AddMemberView();
            MemberDAL memberDAL = new MemberDAL();

            //Save member and go back to main menu
            memberDAL.saveMember(addMemberView.showAddMemberView());
            StartController startController = new StartController();
        }
    }
}
