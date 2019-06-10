using SklepWPF.Data;
using SklepWPF.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Input;
using System.Data.Entity;
using System.ComponentModel.DataAnnotations;

namespace SklepWPF.ViewModels
{
    class CreateMessageViewModel : ObservableObject, IPageViewModel
    {
        public string Name { get; set; }

        private readonly MyDbContext _db;

        private readonly int? messageId;

        private readonly bool wasSent;

        private string title;

        private string messageContent;

        [Required(ErrorMessage = "Pole nie może być puste")]
        public string MessageContent
        {
            get
            {
                return messageContent;
            }
            set
            {
                if (messageContent != value)
                    messageContent = value;
                OnPropertyChanged("MessageContent");
                ValidateProperty(value, "MessageContent");
            }
        }

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

        [Required(ErrorMessage = "Pole nie może być puste")]
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
                ValidateProperty(value, "Title");
            }
        }

        public CreateMessageViewModel(bool wasSent)
        {
            Name = "CreateMessage";
            _db = MyDbContext.Create();
            this.wasSent = wasSent;
            ReplyingMessageContent = "";
        }

        public CreateMessageViewModel(int messageId, bool wasSent)
        {
            Name = "CreateMessage";
            this.messageId = messageId;
            this.wasSent = wasSent;
            _db = MyDbContext.Create();
            var message = _db.Messages.Find(messageId);
            ReplyingMessageContent = message.Content;
            if((message.Title.Substring(0,3) != "Re:"))
            {
                Title = $"Re: {message.Title}";
            }
        }

        private void ValidateProperty<T>(T value, string name)
        {
            Validator.ValidateProperty(value, new ValidationContext(this, null, null)
            {
                MemberName = name
            });
        }

        public ICommand SendMessageCommand
        {
            get
            {
                return new RelayCommand(p => SendMessage(), p => IsFormValid());
            }
        }

        private bool IsFormValid()
        {
            if(String.IsNullOrEmpty(messageContent) || String.IsNullOrEmpty(title))
            {
                return false;
            }

            return true;
        }

        private void SendMessage()
        {
            Message message;
            var newContent = new StringBuilder();
            var user = _db.Users.SingleOrDefault(un => un.Nickname == RunTimeInfo.Instance.Username);
            if (messageId == null)
            {
                message = new Message();
                message.AuthorId = user.Id;
                message.AuthorFullName = $"{user.Name} {user.Surname}";
                message.Senders = new List<User> { user };
                message.Created = DateTime.Now;
                message.LastModified = message.Created;
                newContent.Append(MessageContent)
                    .AppendLine().AppendLine()
                    .Append(message.AuthorFullName + " ").Append(message.LastModified.ToLongDateString() + " ").Append(message.LastModified.ToLongTimeString())
                    .AppendLine().AppendLine();
                _db.Messages.Add(message);
            }
            else
            {
                message = _db.Messages.Include(r => r.Receivers).Include(s => s.Senders).SingleOrDefault(m => m.Id == messageId);
                message.LastModified = DateTime.Now;
                if(!message.Senders.Contains(user))
                {
                    message.Senders.Add(user);
                }
                newContent.Append(ReplyingMessageContent).Append(MessageContent)
                    .AppendLine().AppendLine()
                    .Append(user.Name + " " + user.Surname + " ").Append(message.LastModified.ToLongDateString() + " ").Append(message.LastModified.ToLongTimeString())
                    .AppendLine().AppendLine();
            }

            message.Content = newContent.ToString();
            message.Title = Title;

            if (user.IsAdmin)
            {
                var receiver = _db.Users.Find(message.AuthorId);
                if (!message.Receivers.Contains(receiver))
                    message.Receivers.Add(receiver);
                message.ClientSeen = false;
            }
            else
            {
                var admins = _db.Users.Include(rm => rm.ReceivedMessages).Where(u => u.IsAdmin == true);
                foreach (User u in admins)
                {
                    if(!u.ReceivedMessages.Contains(message))
                        u.ReceivedMessages.Add(message);
                }
                message.AdminSeen = false;
                message.ClientSeen = true;
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
            if(messageId != null)
                ApplicationViewModel.Instance.CurrentPageViewModel = new DisplayMessageViewModel(_db.Messages.Find(messageId), wasSent);
            else
                ApplicationViewModel.Instance.CurrentPageViewModel = ApplicationViewModel.Instance.PageViewModels.SingleOrDefault(n => n.Name == "MessageBox");
        }
    }
}
