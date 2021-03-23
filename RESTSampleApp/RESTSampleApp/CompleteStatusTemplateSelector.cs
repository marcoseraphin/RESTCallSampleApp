using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace RESTSampleApp
{
    public class CompleteStatusTemplateSelector : DataTemplateSelector
    {
        public DataTemplate CompleteTemplate { get; set; }
        public DataTemplate UnCompleteTemplate { get; set; }
        public DataTemplate UserIDTemplate { get; set; }

        public CompleteStatusTemplateSelector()
        {
            this.CompleteTemplate = new DataTemplate(typeof(ViewCellCompleted));
            this.UnCompleteTemplate = new DataTemplate(typeof(ViewCellUncompleted));
            this.UserIDTemplate = new DataTemplate(typeof(ViewCellUserID));
        }

        protected override DataTemplate OnSelectTemplate(object item, BindableObject container)
        {
            ToDoItem toDoItem = item as ToDoItem;

            if (toDoItem.Completed)
            {
                if (toDoItem.UserId == 3)
                {
                    return this.UserIDTemplate;
                }

                return this.CompleteTemplate;
            }

            return this.UnCompleteTemplate;
        }
    }
}
