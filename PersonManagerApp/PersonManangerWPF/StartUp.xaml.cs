using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Linq;
using System.Windows;
using System.Windows.Controls;
using DataLayerLogic;
using PersonEntities;

[assembly: log4net.Config.XmlConfigurator(ConfigFileExtension = ".config", ConfigFile = "App.config", Watch = true)]

namespace PersonManangerWPF
{
    /// <summary>
    /// Interaction logic for StartUp.xaml
    /// </summary>
    public partial class StartUp : Page
    {
 
        private IPersonManager pm;
        private int SelectedPerson;
        IEnumerable<Person> people;
        public StartUp()
        {
            InitializeComponent();
            pm = DLLFacade.CreateManager(AccessType.Memory);
            try
            {
               people= pm.GetPersons();
                LstPerson.ItemsSource = people;

            }
            catch (Exception ex)
            {

                MessageBox.Show($"{ex.Message}\n{ex.StackTrace}", "Error!!!444", MessageBoxButton.OK, MessageBoxImage.Error, MessageBoxResult.OK);
            }
            Options.ItemsSource = Enum.GetValues(typeof(AccessType)).Cast<AccessType>();


            var textchanges = Observable.FromEventPattern<TextChangedEventHandler, TextChangedEventArgs>(
                h => txtUserEntry.TextChanged += h,
                h => txtUserEntry.TextChanged -= h
                ).Select(x => ((TextBox)x.Sender).Text);
            
            textchanges
                .Throttle(TimeSpan.FromMilliseconds(300)) // result on threadpool
                .Select(Lookfor)
                .Switch()
                .ObserveOnDispatcher() // send back to dispatcher
                .Subscribe(OnSearchResult);

            Lookfor("").Subscribe(OnSearchResult);
        }

        private void OnSearchResult(List<Person> list)
        {
            LstPerson.ItemsSource = list;

        }
        public IObservable<List<Person>> Lookfor(string filter)
        {
            var list = pm.GetPersons();
            var filteredList = list.Where(x => x.Name.ToLower().Contains(filter.ToLower())).ToList();
            return Observable.Return(filteredList);
        }
        private void LstPerson_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (LstPerson.SelectedItem is Person person)
            {
                Upadate.IsEnabled = true;
                Delete.IsEnabled = true;
                SelectedPerson = person.Id;
                TxtName.Text = person.Name;
                TxtDateOfBirth.SelectedDate = person.DateOfBirth;
                TxtEmail.Text = person.Email;
            }
        }

        private void Update_Click(object sender, RoutedEventArgs e)
        {
            if (LstPerson.SelectedItem is Person person)
            {
                person.Id = SelectedPerson;
                person.Name = TxtName.Text;
                person.DateOfBirth = TxtDateOfBirth.SelectedDate ?? DateTime.MinValue;
                person.Email = TxtEmail.Text;
                try
                {
                    pm.UpdatePerson(person);
                }
                catch (Exception ex)
                {

                    MessageBox.Show($"{ex.Message}\n{ex.StackTrace}", "Error!!!444", MessageBoxButton.OK, MessageBoxImage.Error, MessageBoxResult.OK);
                }
            }
            ClearPanel();
            LstPerson.ItemsSource = pm.GetPersons();
        }

        private void Delete_Click(object sender, RoutedEventArgs e)
        {

            if (!(LstPerson.SelectedItem is Person person))
            {
                return;
            }
            try
            {
                pm.DeletePerson(person);
            }
            catch (Exception ex)
            {

                MessageBox.Show($"{ex.Message}\n{ex.StackTrace}", "Error!!!444", MessageBoxButton.OK, MessageBoxImage.Error, MessageBoxResult.OK);

            }
            ClearPanel();
            LstPerson.ItemsSource = pm.GetPersons();
        }



        private void AddPerson_Click(object sender, RoutedEventArgs e)
        {
            //NavigationService.Navigate(new AddPerson());
            if (!string.IsNullOrWhiteSpace(TxtName.Text) && !string.IsNullOrWhiteSpace(TxtEmail.Text) && !string.IsNullOrWhiteSpace(TxtDateOfBirth.Text))
            {
                var Person = new Person
                {
                    Name = TxtName.Text,
                    DateOfBirth = TxtDateOfBirth.SelectedDate ?? DateTime.MinValue,
                    Email = TxtEmail.Text
                };

                try
                {
                    pm.AddPerson(Person);
                }
                catch (Exception ex)
                {

                    MessageBox.Show($"{ex.Message}\n{ex.StackTrace}", "Error!!!444", MessageBoxButton.OK, MessageBoxImage.Error, MessageBoxResult.OK);

                }
                LstPerson.ItemsSource = pm.GetPersons();
                ClearPanel();
            }
            return;
        }
        //protected void NavigationService_LoadCompleted(object sender, NavigationEventArgs e)
        //{
        //    if (e.ExtraData is Person)
        //    {
        //        pm.AddPerson((Person)e.ExtraData);
        //        LstPerson.ItemsSource = pm.GetPersons();
        //    }
        //}


        private void Options_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            pm = DLLFacade.CreateManager((AccessType)Enum.Parse(typeof(AccessType), Options.SelectedItem.ToString()));
            LstPerson.ItemsSource = pm.GetPersons();
        }
        private void ClearPanel()
        {
            foreach (var item in LogicalTreeHelper.GetChildren(Display))
            {
                if (item is WrapPanel)
                {
                    foreach (var itemsitem in LogicalTreeHelper.GetChildren((WrapPanel)item))
                    {
                        if (itemsitem is TextBox)
                        {
                            ((TextBox)itemsitem).Text = string.Empty;
                        }
                        if (itemsitem is DatePicker)
                        {
                            ((DatePicker)itemsitem).SelectedDate = DateTime.MinValue;
                        }
                    }
                }
            }
        }

        private void Search_Click(object sender, RoutedEventArgs e)
        {
            try
            {

                LstPerson.ItemsSource = pm.SearchResult(TxtName.Text, TxtDateOfBirth.SelectedDate, TxtEmail.Text);
            }
            catch (Exception ex)
            {

                MessageBox.Show($"{ex.Message}\n{ex.StackTrace}", "Error!!!444", MessageBoxButton.OK, MessageBoxImage.Error, MessageBoxResult.OK);

            }
            ClearPanel();
        }
    }
}
