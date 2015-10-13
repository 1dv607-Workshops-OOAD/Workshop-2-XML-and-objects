using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BoatClub.model
{
    class Member
    {
        int _memberId;
        string _memberName;
        string _memberSocSecNo;

        public Member(int id, string memberName, string memberSocSecNo)
        {
            if (id == 0)
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
        public int MemberID { get { return _memberId; } }
    }
}
