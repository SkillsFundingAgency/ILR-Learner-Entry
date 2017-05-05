using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Xml;

namespace ILR.Renormalised
{
    public class LearnerContact:ILR._base
    {
        private ILR.Learner learner;

        public string AddLine1
        {
            get
            {
                ILR.PostAdd postAdd = GetPostAdd();
                if (postAdd == null)
                    return null;
                else
                    return postAdd.AddLine1;
            }
            set
            {
                var existing = (from ILR.LearnerContact learnerContact in this.learner.LearnerContactList where learnerContact.LocType == 1 select learnerContact).FirstOrDefault();
                if (existing != null)
                {
                    if (((ILR.LearnerContact)existing).PostAdd == null)
                        ((ILR.LearnerContact)existing).CreatePostAdd();
                    ((ILR.LearnerContact)existing).PostAdd.AddLine1 = value;
                }
                else
                {
                    ILR.LearnerContact newLearnerContact = this.learner.CreateLearnerContact();
                    newLearnerContact.LocType = 1;
                    newLearnerContact.ContType = 2;
                    newLearnerContact.CreatePostAdd();
                    newLearnerContact.PostAdd.AddLine1 = value;
                }
                OnPropertyChanged("AddLine1");
            }
        }
        public string AddLine2
        {
            get
            {
                ILR.PostAdd postAdd = GetPostAdd();
                if (postAdd == null)
                    return null;
                else
                    return postAdd.AddLine2;
            }
            set
            {
                var existing = (from ILR.LearnerContact learnerContact in this.learner.LearnerContactList where learnerContact.LocType == 1 select learnerContact).FirstOrDefault();
                if (existing != null)
                {
                    if (((ILR.LearnerContact)existing).PostAdd == null)
                        ((ILR.LearnerContact)existing).CreatePostAdd();
                    ((ILR.LearnerContact)existing).PostAdd.AddLine2 = value;
                }
                else
                {
                    ILR.LearnerContact newLearnerContact = this.learner.CreateLearnerContact();
                    newLearnerContact.LocType = 1;
                    newLearnerContact.ContType = 2;
                    newLearnerContact.CreatePostAdd();
                    newLearnerContact.PostAdd.AddLine2 = value;
                }
                OnPropertyChanged("AddLine2");
            }
        }
        public string AddLine3
        {
            get
            {
                ILR.PostAdd postAdd = GetPostAdd();
                if (postAdd == null)
                    return null;
                else
                    return postAdd.AddLine3;
            }
            set
            {
                var existing = (from ILR.LearnerContact learnerContact in this.learner.LearnerContactList where learnerContact.LocType == 1 select learnerContact).FirstOrDefault();
                if (existing != null)
                {
                    if (((ILR.LearnerContact)existing).PostAdd == null)
                        ((ILR.LearnerContact)existing).CreatePostAdd();
                    ((ILR.LearnerContact)existing).PostAdd.AddLine3 = value;
                }
                else
                {
                    ILR.LearnerContact newLearnerContact = this.learner.CreateLearnerContact();
                    newLearnerContact.LocType = 1;
                    newLearnerContact.ContType = 2;
                    newLearnerContact.CreatePostAdd();
                    newLearnerContact.PostAdd.AddLine3 = value;
                }
            OnPropertyChanged("AddLine3");
            }
        }
        public string AddLine4
        {
            get
            {
                ILR.PostAdd postAdd = GetPostAdd();
                if (postAdd == null)
                    return null;
                else
                    return postAdd.AddLine4;
            }
            set
            {
                var existing = (from ILR.LearnerContact learnerContact in this.learner.LearnerContactList where learnerContact.LocType == 1 select learnerContact).FirstOrDefault();
                if (existing != null)
                {
                    if (((ILR.LearnerContact)existing).PostAdd == null)
                        ((ILR.LearnerContact)existing).CreatePostAdd();
                    ((ILR.LearnerContact)existing).PostAdd.AddLine4 = value;
                }
                else
                {
                    ILR.LearnerContact newLearnerContact = this.learner.CreateLearnerContact();
                    newLearnerContact.LocType = 1;
                    newLearnerContact.ContType = 2;
                    newLearnerContact.CreatePostAdd();
                    newLearnerContact.PostAdd.AddLine4 = value;
                    OnPropertyChanged("AddLine4");
                }
            }
        }
        public string PostCode
        {
            get
            {
                return (from ILR.LearnerContact learnerContact in this.learner.LearnerContactList where learnerContact.LocType == 2 && learnerContact.ContType == 2 select learnerContact.PostCode).FirstOrDefault();
            }
            set
            {
                var existing = (from ILR.LearnerContact learnerContact in this.learner.LearnerContactList where learnerContact.LocType == 2 && learnerContact.ContType == 2 select learnerContact).FirstOrDefault();
                if (existing != null)
                    ((ILR.LearnerContact)existing).PostCode = value;
                else
                {
                    ILR.LearnerContact newLearnerContact = this.learner.CreateLearnerContact();
                    newLearnerContact.LocType = 2;
                    newLearnerContact.ContType = 2;
                    newLearnerContact.PostCode = value;
                }
                OnPropertyChanged("PostCode");
            }
        }
        public string TelNumber
        {
            get
            {
                return (from ILR.LearnerContact learnerContact in this.learner.LearnerContactList where learnerContact.LocType == 3 select learnerContact.TelNumber).FirstOrDefault();
            }
            set
            {
                var existing = (from ILR.LearnerContact learnerContact in this.learner.LearnerContactList where learnerContact.LocType == 3 select learnerContact).FirstOrDefault();
                if (existing != null)
                    ((ILR.LearnerContact)existing).TelNumber = value;
                else
                {
                    ILR.LearnerContact newLearnerContact = this.learner.CreateLearnerContact();
                    newLearnerContact.LocType = 3;
                    newLearnerContact.ContType = 2;
                    newLearnerContact.TelNumber = value;
                }
                OnPropertyChanged("TelNumber");
            }
        }
        public string Email
        {
            get
            {
                return (from ILR.LearnerContact learnerContact in this.learner.LearnerContactList where learnerContact.LocType == 4 select learnerContact.Email).FirstOrDefault();
            }
            set
            {
                var existing = (from ILR.LearnerContact learnerContact in this.learner.LearnerContactList where learnerContact.LocType == 4 select learnerContact).FirstOrDefault();
                if (existing != null)
                    ((ILR.LearnerContact)existing).Email = value;
                else
                {
                    ILR.LearnerContact newLearnerContact=this.learner.CreateLearnerContact();
                    newLearnerContact.LocType = 4;
                    newLearnerContact.ContType = 2;
                    newLearnerContact.Email = value;
                }
                OnPropertyChanged("Email");
            }
        }
        public string PriorPostCode
        {
            get
            {
                return (from ILR.LearnerContact learnerContact in this.learner.LearnerContactList where learnerContact.LocType == 2 && learnerContact.ContType == 1 select learnerContact.PostCode).FirstOrDefault();
            }
            set
            {
                var existing = (from ILR.LearnerContact learnerContact in this.learner.LearnerContactList where learnerContact.LocType == 2 && learnerContact.ContType == 1 select learnerContact).FirstOrDefault();
                if (existing != null)
                    ((ILR.LearnerContact)existing).PostCode = value;
                else
                {
                    ILR.LearnerContact newLearnerContact = this.learner.CreateLearnerContact();
                    newLearnerContact.LocType = 2;
                    newLearnerContact.ContType = 1;
                    newLearnerContact.PostCode = value;
                }
                OnPropertyChanged("PriorPostCode");
            }
        }

        internal LearnerContact(ILR.Learner Learner)
        {
            this.learner = Learner;
        }

        private ILR.PostAdd GetPostAdd()
        {
            return (from ILR.LearnerContact learnerContact in this.learner.LearnerContactList where learnerContact.LocType == 1 select learnerContact.PostAdd).FirstOrDefault();
        }
    }
}
