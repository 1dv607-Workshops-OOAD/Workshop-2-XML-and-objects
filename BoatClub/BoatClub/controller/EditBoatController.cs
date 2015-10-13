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
    class EditBoatController
    {
        private EditBoatView editBoatView;
        private Member member;
        private string selectedBoatId;

        public EditBoatController(Member member)
        {
            Console.WriteLine("EditBoatController konstruktor" + member.MemberName);
            this.member = member;
            this.editBoatView = new EditBoatView(this.member);
            
            //If a member has boats, show member´s boats
            editBoatView.showMemberBoatsMenu();

            showSelectedBoat();
            executeMenuChoice();
        }

        public void showSelectedBoat()
        {
            selectedBoatId = editBoatView.getSelectedBoat();
            if(selectedBoatId.ToUpper() == "S"){
                StartController startController = new StartController();
            }
            editBoatView.showEditBoatMenu(selectedBoatId, member.MemberID);
        }

        public void executeMenuChoice()
        {
            MemberDAL memberDAL = new MemberDAL();

            Helper.MenuChoice menuChoice = editBoatView.getEditBoatMenuChoice();

            if (menuChoice == Helper.MenuChoice.Delete)
            {
                memberDAL.deleteBoatById(selectedBoatId, member.MemberID);
            }
            if (menuChoice == Helper.MenuChoice.Edit)
            {
                memberDAL.updateBoatById(editBoatView.editBoat(selectedBoatId), member.MemberID);
            }
            StartController startController = new StartController();
        }
    }
}
