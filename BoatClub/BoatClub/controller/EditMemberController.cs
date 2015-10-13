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
    class EditMemberController
    {
        private EditMemberView editMemberView;
        private Member member;

        public EditMemberController(Member member)
        {
            this.member = member;
            this.editMemberView = new EditMemberView(member);
            showMemberView();
            executeMenuChoice(editMemberView.getMenuChoice());
        }

        public void showMemberView()
        {
            editMemberView.showEditMemberMenu();
            member = editMemberView.showSelectedMemberWithBoats(member.MemberID);
        }

        public void executeMenuChoice(Helper.MenuChoice menuChoice)
        {
            MemberDAL memberDAL = new MemberDAL();

            if (menuChoice == Helper.MenuChoice.Boats)
            {
                EditBoatController editBoatcontroller = new EditBoatController(member);
            }
            else { 
                if (menuChoice == Helper.MenuChoice.Delete)
                {
                    memberDAL.deleteMemberById(member.MemberID);
                }
                if (menuChoice == Helper.MenuChoice.Edit)
                {
                    editMemberView.showSelectedMemberWithoutBoats();
                    
                    //editMemberView.editMember returns a member object
                    memberDAL.updateMemberById(editMemberView.editMember());
                }
            
                StartController startController = new StartController();
            }
        }
    }
}
