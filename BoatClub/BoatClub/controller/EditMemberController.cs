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
        private string selectedMember;
        private EditMemberView editMemberView;
        Member member;

        public EditMemberController(string selectedMember)
        {
            this.selectedMember = selectedMember;
            this.editMemberView = new EditMemberView();
            showMemberView();
            executeMenuChoice(editMemberView.getMenuChoice());
        }

        public void showMemberView()
        {
            editMemberView.showEditMemberMenu();
            member = editMemberView.showSelectedMemberWithBoats(selectedMember);
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
                    memberDAL.deleteMemberById(selectedMember);
                }
                if (menuChoice == Helper.MenuChoice.Edit)
                {
                    editMemberView.showSelectedMemberWithoutBoats(selectedMember);
                    
                    //editMemberView.editMember returns a member object
                    memberDAL.updateMemberById(editMemberView.editMember(selectedMember));
                }
            
                StartController startController = new StartController();
            }
        }
    }
}
