using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BoatClub.model
{
    class Member
    {
        string _memberId;
        string _memberName;
        string _memberSocSecNo;

        MemberDAL memberDAL;

        private Boat boat;

        public Member(string id, string memberName, string memberSocSecNo)
        {
            memberDAL = new MemberDAL();

            if (id == "0")
            {
                //Sets a unique member id, for a new member
                MemberID memberId = new MemberID();
                this._memberId = memberId.generateMemberId();
            }
            else
            {
                this._memberId = id;
            }
            this._memberName = memberName;
            this._memberSocSecNo = memberSocSecNo;
        }

        public string MemberName { get { return _memberName; } }
        public string MemberSocSecNo { get { return _memberSocSecNo; } }
        public string MemberID { get { return _memberId; } }

        public void addBoatToMember(string boatId, string boatType, string boatLength)
        {
            boat = new Boat(boatId, boatType, boatLength, MemberID);
            memberDAL.saveBoat(MemberID, boat);
        }

        public List<Boat> getBoatsByMember(string memberId) {
            List<Boat> memberBoats = memberDAL.getBoatsByMemberId(memberId);
            return memberBoats;
        }
    }
}
