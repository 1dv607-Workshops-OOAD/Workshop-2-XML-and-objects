using BoatClub.helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Linq;
using System.Xml.Serialization;
using System.Xml.XmlConfiguration;

namespace BoatClub.model
{
    class MemberDAL
    {
        //Path to XML file
        private string path = "../../data/Members.xml";

        //Elements and attributes strings for XML files
        private string XMLElementMembers = "Members";
        private string XMLElementMember = "Member";
        private string XMLElementBoat = "Boat";
        private string XMLAttributeMemberId = "memberId";
        private string XMLAttributeName = "name";
        private string XMLAttributeSocialSecNo = "socialSecNo";
        private string XMLAttributeBoatId = "boatId";
        private string XMLAttributeBoatType = "boatType";
        private string XMLAttributeBoatLength = "boatLength";

        public List<Member> getAllMembers() {

            List<Member> members = new List<Member>();

            using (XmlTextReader reader = new XmlTextReader(path))
            {
                while (reader.Read())
                {
                    if ((reader.NodeType == XmlNodeType.Element) && (reader.Name == XMLElementMember))
                    {
                        string id = reader.GetAttribute(XMLAttributeMemberId);
                        string name = reader.GetAttribute(XMLAttributeName);
                        string socialSecNo = reader.GetAttribute(XMLAttributeSocialSecNo);
                        Member member = new Member(id, name, socialSecNo);

                        members.Add(member);
                    }
                }
            }
            return members;
        }

        //Returns selected member
        public Member getMemberById(string memberId)
        {
            List<Member> members = getAllMembers();
            Member selectedMember;

            foreach (Member member in members) { 
                if(member.MemberID == memberId){
                    selectedMember = member;
                    return selectedMember;
                }
            }

            return null;
        }

        public void saveMember(Member newMember)
        {
            XDocument doc = XDocument.Load(path);
            XElement memberRoot = new XElement(XMLElementMember);
            memberRoot.Add(new XAttribute(XMLAttributeMemberId, newMember.MemberID.ToString()));
            memberRoot.Add(new XAttribute(XMLAttributeName, newMember.MemberName));
            memberRoot.Add(new XAttribute(XMLAttributeSocialSecNo, newMember.MemberSocSecNo));
            doc.Element(XMLElementMembers).Add(memberRoot);
            doc.Save(path);
        }

        public void deleteMemberById(string memberId)
        {
            XDocument doc = XDocument.Load(path);
            doc.Root.Elements(XMLElementMember)
                .Where(e => e.Attribute(XMLAttributeMemberId).Value.Equals(memberId))
                .Select(e => e).Single()
                .Remove();
            doc.Save(path);
        }

        public void updateMemberById(Member editedMember)
        {
            XDocument doc = XDocument.Load(path);
            var member = doc.Descendants(XMLElementMember)
                .Where(arg => arg.Attribute(XMLAttributeMemberId).Value == editedMember.MemberID.ToString())
                .Single();
            member.Attribute(XMLAttributeName).Value = editedMember.MemberName;
            member.Attribute(XMLAttributeSocialSecNo).Value = editedMember.MemberSocSecNo;
            doc.Save(path);
        }

        //Returns a list of boats for one member
        public List<Boat> getBoatsByMemberId(string memberId)
        {
            List<Boat> boats = new List<Boat>();
            Boat boat;
            using (XmlTextReader reader = new XmlTextReader(path))
            {
                XDocument doc = XDocument.Load(path);
                foreach (var item in doc.Descendants(XMLElementMember).Elements(XMLElementBoat)
                        .Where(e => e.Parent.Name == XMLElementMember &&
                        e.Parent.Attribute(XMLAttributeMemberId).Value == memberId))
                {
                    string boatId = item.Attribute(XMLAttributeBoatId).Value;
                    string boatType = item.Attribute(XMLAttributeBoatType).Value;
                    string boatLength = item.Attribute(XMLAttributeBoatLength).Value;
                    boat = new Boat(int.Parse(boatId), boatType, boatLength, memberId);
                    boats.Add(boat);
                }
            }
            return boats;
        }

        public void saveBoat(string memberId, Boat newBoat)
        {
            XDocument doc = XDocument.Load(path);
            doc.Element(XMLElementMembers).Elements(XMLElementMember)
            .First(c => (string)c.Attribute(XMLAttributeMemberId) == memberId)
            .Add(
                new XElement(
                XMLElementBoat, new XAttribute(XMLAttributeBoatId, newBoat.BoatId),
                                new XAttribute(XMLAttributeBoatType, newBoat.BoatType),
                                new XAttribute(XMLAttributeBoatLength, newBoat.BoatLength)
                )
            );
            doc.Save(path);
        }

        public void deleteBoatById(string selectedBoatId, string memberId)
        {
            XDocument doc = XDocument.Load(path);
            doc.Descendants(XMLElementBoat).Where(e => e.Attribute(XMLAttributeBoatId).Value.Equals(selectedBoatId))
                .Where(e => e.Parent.Attribute(XMLAttributeMemberId).Value.Equals(memberId)).Single()
                .Remove();
            doc.Save(path);
        }

        public void updateBoatById(Boat editedBoat, string memberId)
        {
            XDocument doc = XDocument.Load(path);
            var member = doc.Descendants(XMLElementMember)
                .Where(arg => arg.Attribute(XMLAttributeMemberId).Value == memberId)
                .Single();
            member.Element(XMLElementBoat).Attribute(XMLAttributeBoatType).Value = editedBoat.BoatType;
            member.Element(XMLElementBoat).Attribute(XMLAttributeBoatLength).Value = editedBoat.BoatLength;
            doc.Save(path);
        }

        //Returns one boat
        public Boat getBoatById(string selectedBoatId, string memberId)
        {
            List<Boat> boats = getBoatsByMemberId(memberId);

            foreach (Boat boat in boats) {
                if(boat.BoatId.ToString() == selectedBoatId){
                    Boat selectedBoat = new Boat(boat.BoatId, boat.BoatType, boat.BoatLength, memberId);
                    return selectedBoat;
                }
            }
            return null;
        }
    }
}
