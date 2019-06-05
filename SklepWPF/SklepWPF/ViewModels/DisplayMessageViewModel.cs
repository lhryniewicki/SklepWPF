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

        public string MessageContent { get; set; }

        public string AuthorFullName { get; set; }

        public string Title { get; set; }

        public bool CanReply { get; set; }

        public DisplayMessageViewModel(Message message)
        {
            Name = "DisplayMessage";
            _db = MyDbContext.Create();
            messageId = message.Id;
            var user = _db.Users.Where(n => n.Name == RunTimeInfo.Instance.Username).SingleOrDefault();
            if (user != null)
                userId = user.Id;
            if(message.AuthorId != userId)
                CanReply = true;
            else
                CanReply = false;
            MessageContent = message.Content;
            var author = _db.Users.Find(message.AuthorId);
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
            ApplicationViewModel.Instance.CurrentPageViewModel = new CreateMessageViewModel(messageId);
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
            var msg = _db.Messages.Include(rm => rm.Receivers).SingleOrDefault(m => m.Id == messageId);
            if(msg.AuthorId == userId)
            {
                if(msg.Receivers.Count == 0)
                {
                    _db.Messages.Remove(msg);
                }
                else
                {
                    msg.AuthorId = null;
                }
            }
            else
            {
                if (msg.Receivers.Count == 1 && msg.AuthorId == null)
                {
                    _db.Messages.Remove(msg);
                }
                else
                {
                    msg.Receivers.Remove(_db.Users.Find(userId));
                }
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
