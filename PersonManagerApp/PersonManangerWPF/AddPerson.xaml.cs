using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using DataLayerLogic;
using PersonEntities;
namespace PersonManangerWPF
{
    /// <summary>
    /// Interaction logic for AddPerson.xaml
    /// </summary>
    public partial class AddPerson : Page
    {
        IPersonManager pm =  DLLFacade.CreateManager(AccessType.Memory);
        public AddPerson()
        {
            InitializeComponent();
        }

        private void AddPersonBTN_Click(object sender, RoutedEventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(TxtName.Text) && !string.IsNullOrWhiteSpace(TxtEmail.Text))
            {
               
                Person passedPerson = new Person
                {
                    Name = TxtName.Text,
                    Email = TxtEmail.Text
                };
                pm.AddPerson(passedPerson);
              // this.NavigationService.Navigate(new StartUp(passedPerson));

            }
            return;
        }

        private void Delete_Click(object sender, RoutedEventArgs e)
        {
            this.NavigationService.Navigate(new StartUp());
        }
    }
}
