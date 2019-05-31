using SklepWPF.Data;
using SklepWPF.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Input;

namespace SklepWPF.ViewModels
{
	class ClientPanelViewModel : IPageViewModel
	{
		public string Name { get; set; }

        private readonly MyDbContext _db;

        public User User { get; set; }

        public ClientPanelViewModel()
        {
            _db = MyDbContext.Create();
            User = _db.Users.Where(n => n.Name == RunTimeInfo.Instance.Username).FirstOrDefault();
        }

        public ICommand SaveUserData
        {
            get
            {
                return new RelayCommand(p => Save(),
                    p => IsFormValid());
            }
        }

        private void Save()
        {

        }

        private bool IsFormValid()
        {
            return false;
        }
    }
}
