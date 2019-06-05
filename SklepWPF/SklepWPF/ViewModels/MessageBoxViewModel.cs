﻿using SklepWPF.Data;
using SklepWPF.Enums;
using SklepWPF.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.Entity;
using System.Linq;
using System.Windows.Input;

namespace SklepWPF.ViewModels
{
    class MessageBoxViewModel : ObservableObject, IPageViewModel
    {
        public string Name { get; set; }

        private readonly MyDbContext _db;

        private readonly int userId;

        public ICollection<Message> Messages { get; set; }

        public MessageDisplay DisplayedMessages { get; set; }

        private readonly int messagePageSize;

        private bool isNextMsgPageValid;

        private int messagePage;

        public string MessageSearchQuery { get; set; }

        public int MessagePage
        {
            get
            {
                return messagePage;
            }
            set
            {
                if (messagePage != value)
                    messagePage = value;
                OnPropertyChanged("MessagePage");
            }
        }

        public MessageBoxViewModel()
        {
            _db = MyDbContext.Create();
            var user = _db.Users.Where(n => n.Name == RunTimeInfo.Instance.Username).SingleOrDefault();
            if(user != null)
                userId = user.Id;
            messagePage = 1;
            messagePageSize = 14;
            isNextMsgPageValid = false;
            Messages = new ObservableCollection<Message>();
            DisplayedMessages = MessageDisplay.Received;
            LoadMessages();
        }

        public ICommand NextPageCommand
        {
            get
            {
                return new RelayCommand(p => NextPage(),
                    p => IsValidNext());
            }
        }

        private bool IsValidNext()
        {
            return isNextMsgPageValid;
        }

        private void NextPage()
        {
            MessagePage++;
            LoadMessages();
        }

        public ICommand PreviousPageCommand
        {
            get
            {
                return new RelayCommand(p => PreviousPage(),
                    p => IsValidPrevious());
            }
        }

        private bool IsValidPrevious()
        {
            return (MessagePage - 1) > 0;
        }

        private void PreviousPage()
        {
            MessagePage--;
            LoadMessages();
        }

        public ICommand ChangeMessagesCommand
        {
            get
            {
                return new RelayCommand(p => ChangeMessages((int)p));
            }
        }

        private void ChangeMessages(int display)
        {
            DisplayedMessages = (MessageDisplay)display;
            MessagePage = 1;
            LoadMessages();
        }

        public ICommand SearchMessagesCommand
        {
            get
            {
                return new RelayCommand(p => SearchMessages());
            }
        }

        private void SearchMessages()
        {
            MessagePage = 1;
            LoadMessages();
        }

        public ICommand DeleteSelectedMessagesCommand
        {
            get
            {
                return new RelayCommand(p => DeleteSelectedMessages(((System.Collections.IList)p).Cast<Message>().ToList()));
            }
        }

        private void DeleteSelectedMessages(ICollection<Message> selectedMessages)
        {
            foreach (Message m in selectedMessages)
            {
                var _message = _db.Messages.Include(msg => msg.Receivers).SingleOrDefault(msg => msg.Id == m.Id);

                if (DisplayedMessages == MessageDisplay.Sent)
                {
                    if (_message.Receivers.Count == 0)
                    {
                        _db.Messages.Remove(m);
                    }
                    else
                    {
                        _message.Author = null;
                    }
                }
                else
                {
                    if (_message.Receivers.Count == 1 && m.AuthorId == null)
                    {
                        _db.Messages.Remove(m);
                    }
                    else
                    {
                        _message.Receivers.Remove(_db.Users.Find(userId));
                    }
                }

                Messages.Remove(m);
            }

            _db.SaveChanges();

            LoadMessages();
        }

        public ICommand ClearMessageBoxCommand
        {
            get
            {
                return new RelayCommand(p => ClearMessageBox());
            }
        }

        private void ClearMessageBox()
        {
            ICollection<Message> messages;
            List<Message> dbRemovedMessages = new List<Message>();

            var user = _db.Users.SingleOrDefault(n => n.Name == RunTimeInfo.Instance.Username);

            if (DisplayedMessages == MessageDisplay.Sent)
            {
                messages = _db.Messages.Where(a => a.AuthorId == user.Id).ToList();
            }
            else
            {
                messages = _db.Messages.Include(r => r.Receivers).Where(r => r.Receivers.Any(u => u.Id == userId)).ToList();
            }

            if (messages != null)
            {
                foreach (Message m in messages)
                {
                    if (m.Receivers.Count == 0 || (m.AuthorId == null && m.Receivers.Count == 1))
                    {
                        dbRemovedMessages.Add(m);
                    }
                    else if (DisplayedMessages == MessageDisplay.Sent)
                    {
                        m.AuthorId = null;
                    }
                    else
                    {
                        m.Receivers.Remove(user);
                    }
                }

                messages.Clear();
                _db.Messages.RemoveRange(dbRemovedMessages);
                _db.SaveChanges();

                MessagePage = 1;
                Messages.Clear();
                isNextMsgPageValid = false;
            }
        }

        private void LoadMessages()
        {
            IQueryable<Message> messages;

            if (DisplayedMessages == MessageDisplay.Sent)
            {
                messages = _db.Messages.Where(m => m.AuthorId == userId);
            }
            else
            {
                messages = _db.Messages.Where(m => m.Receivers.Any(u => u.Id == userId));
            }

            if (!String.IsNullOrEmpty(MessageSearchQuery))
            {
                messages = messages.Where(m => m.Title.Contains(MessageSearchQuery));
            }

            var _messages = messages.OrderBy(d => d.Created).ThenBy(s => s.Seen).Skip((messagePage - 1) * messagePageSize)
                    .Take(messagePageSize + 1).ToList();

            if (_messages.Count == messagePageSize + 1)
            {
                isNextMsgPageValid = true;
                _messages.RemoveAt(_messages.Count - 1);
            }
            else
            {
                isNextMsgPageValid = false;
            }

            Messages.Clear();
            for (int i = 0; i < _messages.Count; i++)
            {
                Messages.Add(_messages[i]);
            }
        }

        public ICommand DisplayMessageCommand
        {
            get
            {
                return new RelayCommand(p => DisplayMessage((Message)p));
            }
        }

        private void DisplayMessage(Message message)
        {
            if(message.AuthorId != userId)
            {
                message.Seen = true;
                _db.SaveChanges();
            }
            ApplicationViewModel.Instance.CurrentPageViewModel = new DisplayMessageViewModel(message);
        }

        public ICommand CreateMessageCommand
        {
            get
            {
                return new RelayCommand(p => CreateMessage());
            }
        }
        
        private void CreateMessage()
        {
            ApplicationViewModel.Instance.CurrentPageViewModel = new CreateMessageViewModel();
        }
    }
}
