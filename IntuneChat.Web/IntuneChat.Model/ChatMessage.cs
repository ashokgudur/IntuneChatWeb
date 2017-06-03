using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntuneChat.Model
{
    public enum CommentType
    {
        Contact,
        Account,
        Entry,
    }

    public class ChatMessage
    {
        public string ByEmail { get; set; }
        public string ByName { get; set; }
        public string ToEmail { get; set; }
        public string ToName { get; set; }
        public string Text { get; set; }
        public int ByUserId { get; set; }
        public int ToUserId { get; set; }
        public int AccountId { get; set; }
        public int EntryId { get; set; }
        public DateTime DateTimeStamp { get; set; }

        public CommentType CommentType
        {
            get
            {
                if (EntryId > 0)
                    return CommentType.Entry;
                else if (AccountId > 0)
                    return CommentType.Account;
                else
                    return CommentType.Contact;
            }
        }

        public string GetCommentKey()
        {
            if (CommentType == CommentType.Entry)
                return composeKey(CommentType.Entry, EntryId);
            else if (CommentType == CommentType.Account)
                return composeKey(CommentType.Account, AccountId);
            else
                return composeKey(CommentType.Contact, ByUserId);
        }

        private string composeKey(CommentType type, int id)
        {
            return string.Format("{0}-{1}", type, id);
        }
    }
}
