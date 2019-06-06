using SklepWPF.Data;
using SklepWPF.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Input;
using System.Data.Entity;

namespace SklepWPF.ViewModels
{
    class DisplayMessageViewModel : ObservableObject, IPageViewModel
    {
        public string Name { get; set; }

        private readonly MyDbContext _db;

        private readonly int userId;

        private readonly int messageId;

        private readonly bool wasSent;

        public string MessageContent { get; set; }

        public string AuthorFullName { get; set; }

        public string Title { get; set; }

        public DisplayMessageViewModel(Message message, bool wasSent)
        {
            Name = "DisplayMessage";
            _db = MyDbContext.Create();
            this.wasSent = wasSent;
            messageId = message.Id;
            var user = _db.Users.Where(n => n.Name == RunTimeInfo.Instance.Username).SingleOrDefault();
            if (user != null)
            {
                userId = user.Id;
                if(user.IsAdmin)
                {
                    _db.Messages.Find(messageId).AdminSeen = true;
                }
                else
                {
                    _db.Messages.Find(messageId).ClientSeen = true;
                }
                _db.SaveChanges();
            }
            MessageContent = message.Content;
            AuthorFullName = message.AuthorFullName;
            Title = message.Title;
        }

        public ICommand ReplyCommand
        {
            get
            {
                return new RelayCommand(p => Reply());
            }
        }

        private void Reply()
        {
            ApplicationViewModel.Instance.CurrentPageViewModel = new CreateMessageViewModel(messageId, wasSent);
        }

        public ICommand DeleteMessageCommand
        {
            get
            {
                return new RelayCommand(p => DeleteMessage());
            }
        }

        private void DeleteMessage()
        {
            var msg = _db.Messages.Include(rm => rm.Receivers).Include(sm => sm.Senders).SingleOrDefault(m => m.Id == messageId);

            if(wasSent)
            {
                msg.Senders.Remove(_db.Users.Find(userId));
            }
            else
            {
                msg.Receivers.Remove(_db.Users.Find(userId));
            }

            if (msg.Senders.Count + msg.Receivers.Count == 0)
            {
                _db.Messages.Remove(msg);
            }

            _db.SaveChanges();

            ApplicationViewModel.Instance.CurrentPageViewModel = ApplicationViewModel.Instance.PageViewModels.SingleOrDefault(n => n.Name == "MessageBox");
        }

        public ICommand BackCommand
        {
            get
            {
                return new RelayCommand(p => Back());
            }
        }

        private void Back()
        {
            ApplicationViewModel.Instance.CurrentPageViewModel = ApplicationViewModel.Instance.PageViewModels.SingleOrDefault(n => n.Name == "MessageBox");
        }
    }
}
