using Acr.UserDialogs;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace RESTSampleApp
{
    public class MainPageViewModel : INotifyPropertyChanged
    {
        public Command CallRESTCommand
        {
            get
            {
                return new Command(async () =>
                {
                    try
                    {
                        UserDialogs.Instance.ShowLoading("Load Data...");
                        await this.CallRESTService();
                        UserDialogs.Instance.HideLoading();
                    }
                    catch (Exception ex)
                    {
                        Debug.WriteLine(ex.Message);
                        throw;
                    }
                }
                );
            }
        }

        private ObservableCollection<ToDoItem> toDoItemList;

        public event PropertyChangedEventHandler PropertyChanged;

        public ObservableCollection<ToDoItem> ToDoItemList
        {
            get
            {
                return this.toDoItemList;
            }
            set
            {
                this.toDoItemList = value;
                PropertyChanged(this, new PropertyChangedEventArgs("ToDoItemList"));
            }
        }

        private async Task<bool> CallRESTService()
        {
            var uri = new Uri("https://jsonplaceholder.typicode.com/todos");

            HttpClient httpClient = new HttpClient();
            HttpResponseMessage httpResponseMessage = await httpClient.GetAsync(uri);

            if (httpResponseMessage.IsSuccessStatusCode)
            {
                var json = await httpResponseMessage.Content.ReadAsStringAsync();
                if (!String.IsNullOrEmpty(json))
                {
                    List<ToDoItem> toDoItems = JsonConvert.DeserializeObject<List<ToDoItem>>(json);
                    this.ToDoItemList = new ObservableCollection<ToDoItem>(toDoItems); 
                }
            }

            return false;
        }
    }
}
