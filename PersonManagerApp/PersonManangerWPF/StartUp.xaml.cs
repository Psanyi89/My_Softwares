using DataLayerLogic;
using PersonEntities;
using System;
using System.Windows;
using System.Windows.Controls;


namespace PersonManangerWPF
{
    /// <summary>
    /// Interaction logic for StartUp.xaml
    /// </summary>
    public partial class StartUp : Page
    {
        private IPersonManager pm;
        public StartUp()
        {
            InitializeComponent();
            pm = new DLLFacade().GetPersonManagerMemory();
            LstPerson.ItemsSource = pm.GetPersons();
            Options.ItemsSource = new DataAccess().AccesPoint;

        }
        private void LstPerson_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (LstPerson.SelectedItem is Person person)
            {
                Upadate.IsEnabled = true;
                Delete.IsEnabled = true;
                TxtName.Text = person.Name;
                TxtDateOfBirth.Text = person.DateOfBirth.ToShortDateString();
                TxtEmail.Text = person.Email;
            }
        }

        private void Update_Click(object sender, RoutedEventArgs e)
        {
            if (LstPerson.SelectedItem is Person person)
            {
                person.Name = TxtName.Text;
                person.DateOfBirth = Convert.ToDateTime(TxtDateOfBirth.Text);
                person.Email = TxtEmail.Text;
                pm.UpdatePerson(person);
                ClearPanel();
            }
            LstPerson.ItemsSource = pm.GetPersons();
        }

        private void Delete_Click(object sender, RoutedEventArgs e)
        {

            if (!(LstPerson.SelectedItem is Person person))
            {
                return;
            }
            pm.DeletePerson(person);
            ClearPanel();
            LstPerson.ItemsSource = pm.GetPersons();
        }



        private void AddPerson_Click(object sender, RoutedEventArgs e)
        {
            //NavigationService.Navigate(new AddPerson());
            if (!string.IsNullOrWhiteSpace(TxtName.Text) && !string.IsNullOrWhiteSpace(TxtEmail.Text) && !string.IsNullOrWhiteSpace(TxtDateOfBirth.Text))
            {
                Person Person = new Person
                {
                    Name = TxtName.Text,
                    DateOfBirth = Convert.ToDateTime(TxtDateOfBirth.Text),
                    Email = TxtEmail.Text
                };
                pm.AddPerson(Person);
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
            if (Options.SelectedIndex == 0)
            {
                pm = new DLLFacade().GetPersonManagerMemory();

            }
            else if (Options.SelectedIndex == 1)
            {
                pm = new DLLFacade().GetPersonManagerTxt();
            }
            else if (Options.SelectedIndex == 2)
            {
                pm = new DLLFacade().GetPersonManagerCSV();
            }
            else if (Options.SelectedIndex == 3)
            {
                pm = new DLLFacade().GetPersonManagerXml();
            }
            else if (Options.SelectedIndex == 4)
            {
                pm = new DLLFacade().GetPersonManagerToml();
            }
            LstPerson.ItemsSource = pm.GetPersons();
        }
        private void ClearPanel()
        {
            foreach (object item in LogicalTreeHelper.GetChildren(Display))
            {
                if (item is WrapPanel)
                {
                    foreach (object itemsitem in LogicalTreeHelper.GetChildren((WrapPanel)item))
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
            LstPerson.ItemsSource = pm.SearchResult(TxtName.Text,TxtDateOfBirth.SelectedDate,TxtEmail.Text);
            ClearPanel();
        }
    }
}
