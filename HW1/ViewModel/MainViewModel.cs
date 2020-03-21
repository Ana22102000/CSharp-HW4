using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;
using  CSharpHomework.Model;
using  CSharpHomework.Tools;
using CSharpHomework.Tools.Managers;
using CSharpHomework.Tools.Navigation;

namespace CSharpHomework.ViewModel
{
    internal class MainViewModel:BaseViewModel
    {

        #region Fields

        private static DateViewModel _dateViewModel;

        private object m_selectedResult;
        private ObservableCollection<Person> _users;

        private DateTime _fBirthday = DateTime.Now;
        private bool _isChecked = false;


        #region Commands
        private RelayCommand<object> _addCommand;
        private ICommand _deleteCommand;
        private ICommand _editCommand;
        private ICommand _filterCommand;


        #endregion

        #endregion


        #region Properties

        public static DateViewModel DVM
        {
            get { return _dateViewModel; }
            set { _dateViewModel = value; }
        }

        public object SelectedResult
        {
            get { return m_selectedResult; }
            set
            {
                m_selectedResult = value;
                OnPropertyChanged("SelectedResult");
            }
        }
        public ObservableCollection<Person> Users
        {
            get => _users;
            private set
            {
                _users = value;
                OnPropertyChanged();
            }
        }

        public string FName { get; set; }
        public string FSurname { get; set; }

        public string FEmail { get; set; }

        public DateTime FBirthday
        {
            get => _fBirthday;
            set => _fBirthday = value;
        }

        public string FIsAdult { get; set; }
        public string FSunSign { get; set; }

        public string FChineseSign { get; set; }
        public string FIsBirthday { get; set; }

        public bool IsChecked
        {
            get => _isChecked;
            set => _isChecked = value;
        }
        #region Commands
        public RelayCommand<Object> AddCommand
        {
            get
            {
                return _addCommand ?? (_addCommand = new RelayCommand<object>(o =>
                {
                    if (DVM != null)
                    {
                        DVM.SetFields(null);
                    }
                   
                    
                    NavigationManager.Instance.Navigate(ViewType.AddPerson);

                }));
            }
        }
        public ICommand DeleteCommand
        {
            get
            {
                if (_deleteCommand == null)
                {
                    _deleteCommand = new RelayCommand<object>(
                        Delete /*o => CanDelete()*/);
                }
                return _deleteCommand;
            }
        }
        public ICommand EditCommand
        {
            get
            {
                if (_editCommand == null)
                {
                    _editCommand = new RelayCommand<object>(
                        Edit);
                }
                return _editCommand;
            }
        }

        public ICommand FilterCommand
        {

            get
            {
                if (_filterCommand == null)
                {
                    _filterCommand = new RelayCommand<object>(
                        Filter);
                }
                return _filterCommand;
            }
        }

        #endregion

        #endregion







        private void Delete(object result)
        {
            StationManager.DataStorage.RemoveUser((Person)result);
            

            _users.Remove((Person)result);
        }

        private void Edit(object result)
        {
            StationManager.DataStorage.RemoveUser((Person) result);
            _users.Remove((Person) result);
            if (DVM != null)
            {
                DVM.SetFields((Person) result);
            }
            else
            {
                DateViewModel.DefPerson = (Person) result;
            }

            //DateViewModel.SetFields((Person) result);
            NavigationManager.Instance.Navigate(ViewType.AddPerson);



        }


        private void Filter(object result)
        {
            Users = new ObservableCollection<Person>(StationManager.DataStorage.UsersList);


            if(FName?.Equals("") == false)
                Users = new ObservableCollection<Person>(from item in _users
                    where item.Name.StartsWith(FName)
                    select item);
            

            if (FSurname?.Equals("") == false)//todo in 1 function
                Users = new ObservableCollection<Person>(from item in _users
                    where item.Surname.StartsWith(FSurname)
                    select item);

            if (FEmail?.Equals("") == false)//todo in 1 function
                Users = new ObservableCollection<Person>(from item in _users
                    where item.Email.StartsWith(FEmail)
                    select item);

            if (FSunSign?.Equals("") == false)//todo in 1 function
                Users = new ObservableCollection<Person>(from item in _users
                    where item.SunSign.StartsWith(FSunSign)
                    select item);

            if (FChineseSign?.Equals("") == false)//todo in 1 function
                Users = new ObservableCollection<Person>(from item in _users
                    where item.ChineseSign.StartsWith(FChineseSign)
                    select item);

            if (FIsAdult?.Equals("") == false) //todo in 1 function
                Users = new ObservableCollection<Person>(from item in _users
                    where item.IsAdult == Boolean.Parse(FIsAdult.ToLower())
                    select item);

            if (FIsBirthday?.Equals("") == false) //todo in 1 function
                Users = new ObservableCollection<Person>(from item in _users
                    where item.IsBirthday == Boolean.Parse(FIsBirthday.ToLower())
                    select item);

            if(IsChecked)
                if (FBirthday!=null) //todo in 1 function
                    Users = new ObservableCollection<Person>(from item in _users
                        where item.Birthday.ToShortDateString().Equals(FBirthday.ToShortDateString()) 
                        select item);

        }

        public void Sort(string sortField, bool sortAscending)
        {
            LoaderManager.Instance.ShowLoader();

            if (sortAscending)
                Users =new ObservableCollection<Person>(from item in _users
                    orderby typeof(Person).GetProperty(sortField).GetValue(item)
                    select item);
            else
            {
                Users = new ObservableCollection<Person>(from item in _users
                    orderby typeof(Person).GetProperty(sortField).GetValue(item) descending 
                    select item);
            }

            LoaderManager.Instance.HideLoader();

        }

        public void AddUser(Person person)
        {
            _users.Add(person);
        }

        public MainViewModel()
        {

            _users = new ObservableCollection<Person>(StationManager.DataStorage.UsersList);
        }


    }
}
