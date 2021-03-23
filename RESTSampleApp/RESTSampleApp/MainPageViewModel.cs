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
using Xamarin.Essentials;
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
                        var current = Connectivity.NetworkAccess;

                        if (current == NetworkAccess.Internet)
                        {
                            UserDialogs.Instance.ShowLoading("Load Data...");
                            await this.CallRESTService();
                            UserDialogs.Instance.HideLoading();
                        }
                        else
                        {
                            string json = Preferences.Get("Cache", "");
                            if (json != String.Empty)
                            {
                                List<ToDoItem> toDoItems = JsonConvert.DeserializeObject<List<ToDoItem>>(json);
                                this.ToDoItemList = new ObservableCollection<ToDoItem>(toDoItems);
                            }
                            else
                            {
                              await UserDialogs.Instance.AlertAsync("Noch keine lokalen Daten vorhanden");
                            }
                        }
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

                    Preferences.Set("Cache", json);
                }
            }

            return false;
        }
    }
}
