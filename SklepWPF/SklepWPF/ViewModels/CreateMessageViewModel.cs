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
    class CreateMessageViewModel : ObservableObject, IPageViewModel
    {
        public string Name { get; set; }

        private readonly MyDbContext _db;

        private readonly int? messageId;

        private string title;

        public string MessageContent { get; set; }

        private string replyingMessageContent;

        public string ReplyingMessageContent
        {
            get
            {
                return replyingMessageContent;
            }
            set
            {
                if (replyingMessageContent != value)
                    replyingMessageContent = value;
                OnPropertyChanged("ReplyingMessageContent");
            }
        }

        public string Title
        {
            get
            {
                return title;
            }
            set
            {
                if (title != value)
                    title = value;
                OnPropertyChanged("Title");
            }
        }

        public CreateMessageViewModel()
        {
            Name = "CreateMessage";
            _db = MyDbContext.Create();
            ReplyingMessageContent = "";
            Title = "";
        }

        public CreateMessageViewModel(int messageId)
        {
            Name = "CreateMessage";
            this.messageId = messageId;
            _db = MyDbContext.Create();
            var message = _db.Messages.Find(messageId);
            ReplyingMessageContent = message.Content;
            if((message.Title.Substring(0,3) != "Re:"))
            {
                Title = $"Re: {message.Title}";
            }
            else
            {
                Title = message.Title;
            }
        }

        public ICommand SendMessageCommand
        {
            get
            {
                return new RelayCommand(p => SendMessage());
            }
        }

        private void SendMessage()
        {
            Message message = new Message();
            message.Content = MessageContent;
            message.Created = DateTime.Now;
            message.Title = Title;
            var user = _db.Users.SingleOrDefault(un => un.Nickname == RunTimeInfo.Instance.Username);
            message.Author = user;
            message.AuthorFullName = $"{user.Name} {user.Surname}";

            if (user.IsAdmin)
            {
                var receiverId = _db.Messages.Find(messageId).AuthorId;
                message.Receivers = new List<User> { _db.Users.Find(receiverId) };
            }
            else
            {
                var admins = _db.Users.Include(rm => rm.ReceivedMessages).Where(u => u.IsAdmin == true);
                foreach (User u in admins)
                {
                    u.ReceivedMessages.Add(message);
                }
            }

            _db.Messages.Add(message);
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
            if(messageId != null)
                ApplicationViewModel.Instance.CurrentPageViewModel = new DisplayMessageViewModel(_db.Messages.Find(messageId));
            else
                ApplicationViewModel.Instance.CurrentPageViewModel = ApplicationViewModel.Instance.PageViewModels.SingleOrDefault(n => n.Name == "MessageBox");
        }
    }
}
